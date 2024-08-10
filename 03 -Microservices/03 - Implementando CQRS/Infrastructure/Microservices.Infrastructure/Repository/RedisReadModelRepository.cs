using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Infrastructure.Crosscutting;
using Microservices.Infrastructure.Crosscutting.Util;
using Microservices.Infrastructure.MessageBus;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Microservices.Infrastructure.Repository
{
    public class RedisReadModelRepository<T>
        : IReadModelRepository<T> where T : ReadObject
    {
        private readonly IDatabase _database;
        private readonly IMessageBus _bus;

        public RedisReadModelRepository(IDatabase database, IMessageBus bus)
        {
            _database = database;
            _bus = bus;
        }

        //public IEnumerable<T> GetAll()
        //{
        //    var get = new RedisValue[] { InstanceName() + "*" };
        //    var result = _database.SortAsync(SetName(), sortType: SortType.Alphabetic, by: "nosort", get: get).Result;

        //    var readObjects = result.Select(v => JsonConvert.DeserializeObject<T>(v)).AsEnumerable();
        //    return readObjects;
        //}

        public IEnumerable<T> GetAll()
        {
            var setKey = InstanceName() + "Set";
            var ids = _database.SetMembersAsync(setKey).Result; // Obtener todos los IDs del set
            var resultObjects = new List<T>();

            foreach (var id in ids)
            {
                var hashKey = InstanceName() + id; // Construye la clave del hash para cada ID
                var hashEntries = _database.HashGetAllAsync(hashKey).Result; // Obtiene el hash

                if (hashEntries.Length == 0) continue;
                                
                resultObjects.Add(GetObject(hashEntries));
            }

            return resultObjects;
        }


        public T Get(Guid id)
        {
            var key = Key(id);
            var hashEntries = _database.HashGetAll(key);

            if (hashEntries.Length == 0)
                return default(T);

            return GetObject(hashEntries);
        }


        public void Update(T t, Event e = null)
        {
            var key = Key(t.Id);
            var hashEntries = t.GetType().GetProperties().Select(prop =>
            {
                object value = prop.GetValue(t);
                string stringValue = SerializeValue(value, prop.PropertyType);
                return new HashEntry(prop.Name, stringValue);
            }).ToArray();
                        
            _database.HashSet(key, hashEntries);
                        
            if (e != null)
            {
                _bus.Publish(e);
            }
        }

        public void Insert(T t, Event e = null)
        {
            var serialized = JsonConvert.SerializeObject(t);
            var key = Key(t.Id);
            var transaction = _database.CreateTransaction();
            //transaction.StringSetAsync(key, serialized);            
            var hashEntries = t.GetType().GetProperties().Select(prop =>
            {
                object value = prop.GetValue(t);
                string serializedValue = SerializeValue(value, prop.PropertyType);
                return new HashEntry(prop.Name, serializedValue);
            }).ToArray();
            transaction.SetAddAsync(SetName(), t.Id.ToString("N"));
            transaction.HashSetAsync(key, hashEntries);

            var committed = transaction.ExecuteAsync().Result;

            if (!committed)
            {
                throw new ApplicationException("transaction failed. Now what?");
            }

            if (e != null)
            {
                _bus.Publish(e);
            }
        }

        private string Key(Guid id)
        {
            return InstanceName() + id.ToString("N");
        }

        private string InstanceName()
        {
            var type = typeof(T);
            return string.Format("{0}:", type.FullName);
        }
        private string SetName()
        {
            return string.Format("{0}Set", InstanceName());
        }
        private string SerializeValue(object value, Type type)
        {
            if (value == null) return string.Empty;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.DateTime:
                    return JsonConvert.SerializeObject(value, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ"
                    });
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return value.ToString();
                case TypeCode.String:
                case TypeCode.Char:
                    return value.ToString();
                default:
                    if (type == typeof(Guid))
                    {
                        return value.ToString();
                    }
                    else if (IsComplexType(type))
                    {
                        return JsonConvert.SerializeObject(value);
                    }
                    else
                    {
                        return value.ToString();
                    }
            }
        }

        private bool IsComplexType(Type type)
        {
            return !type.IsPrimitive && type != typeof(string) && type != typeof(decimal) && !type.IsEnum;
        }


        private object DeserializeValue(string value, Type type)
        {
            if (string.IsNullOrEmpty(value)) return null;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Decimal:
                    return decimal.Parse(value, CultureInfo.InvariantCulture);
                case TypeCode.Int32:
                    return int.Parse(value, CultureInfo.InvariantCulture);
                case TypeCode.DateTime:
                    return DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
                case TypeCode.String:
                    return value;
                case TypeCode.Boolean:
                    return bool.Parse(value);
                case TypeCode.Double:
                    return double.Parse(value, CultureInfo.InvariantCulture);
                default:
                    if (type == typeof(Guid))
                    {
                        return Guid.Parse(value);
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject(value, type);
                    }
            }
        }

        private T GetObject(HashEntry[] hashEntries)
        {
            var resultObject = Activator.CreateInstance<T>();
            foreach (var entry in hashEntries)
            {
                var property = resultObject.GetType().GetProperty(entry.Name);
                if (property != null && entry.Value.HasValue)
                {
                    var value = DeserializeValue(entry.Value, property.PropertyType);
                    property.SetValue(resultObject, value);
                }
            }

            return resultObject;
        }

    }
}
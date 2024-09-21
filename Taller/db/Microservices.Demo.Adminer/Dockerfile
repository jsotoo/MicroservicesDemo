FROM adminer:4.8.1

USER root

# Instalar dependencias y la extensión MongoDB para PHP
RUN apt-get update && apt-get install -y \
    php-pear \
    php-dev \
    && pecl install mongodb \
    && mkdir -p /usr/local/etc/php/conf.d \
    && echo "extension=mongodb.so" > /usr/local/etc/php/conf.d/mongodb.ini \
    && apt-get clean && rm -rf /var/lib/apt/lists/*

# Cambiar el usuario de nuevo a www-data
USER www-data

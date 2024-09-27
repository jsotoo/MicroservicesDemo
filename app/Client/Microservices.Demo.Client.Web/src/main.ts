import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { ApmService } from '@elastic/apm-rum-angular';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

//const apm = ApmService.init({
//  serviceName: "Microservices.Demo.Client.Web",
//  serverUrl: "http://microservices.demo.apm-server:8200",
//  environment: "production"  
//});

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));

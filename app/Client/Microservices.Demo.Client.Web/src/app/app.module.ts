import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { CanActivateGuard } from './guard/can-activate.guard';
import { HomeComponent } from './home/home.component';
import { ChatComponent } from './chat/chat.component';
import { LoginComponent } from './login/login.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { PoliciesComponent } from './policies/policies.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { SharedModule } from './shared/shared.module';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { ApmErrorHandler, ApmModule, ApmService } from '@elastic/apm-rum-angular';

const routes: Routes = [
  {
    path: '',
    canActivate: [CanActivateGuard],
    children: [
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'products', loadChildren: () => import('./products/products.module').then(m => m.ProductsModule) },
      { path: 'policies', loadChildren: () => import('./policies/policies.module').then(m => m.PoliciesModule) },
      { path: 'chat', component: ChatComponent }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  declarations: [
    AppComponent,
    SideNavComponent,
    PoliciesComponent,
    ChatComponent,
    HomeComponent,
    LoginComponent
  ],
  imports: [
    ApmModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    SharedModule,
    RouterModule.forRoot(routes, { useHash: true }),
    BrowserAnimationsModule
  ],
  providers: [
    ApmService,
    { provide: 'Window', useFactory: () => window },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: ErrorHandler,
      useClass: ApmErrorHandler
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(service: ApmService) {    
    const apm = service.init({
      serviceName: 'Microservices_Demo_Client_Web',
      serverUrl: 'http://localhost:8200',
      environment: 'production'
    });    
  }
}

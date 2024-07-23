import { Component } from '@angular/core';
import { IEnvironment } from '../../../environments/ienvironment.interface';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent {
  env: IEnvironment = environment;

  swaggerEndpoint: IIframeEndpoint = {
    url: `${this.env.emailApiBaseUrl}/${this.env.hangfireServeRoutePath}`,
    title: 'Hangfire Server',
  };

  swaggerEndpoints: IIframeEndpoint[] = [
    {
      url: `${this.env.authApiBaseUrl}/${this.env.swaggerRoutePath}`,
      title: 'Auth API',
    },
    {
      url: `${this.env.couponApiBaseUrl}/${this.env.swaggerRoutePath}`,
      title: 'Coupon API',
    },
    {
      url: `${this.env.productApiBaseUrl}/${this.env.swaggerRoutePath}`,
      title: 'Product API',
    },
    {
      url: `${this.env.orderApiBaseUrl}/${this.env.swaggerRoutePath}`,
      title: 'Order API',
    },
    {
      url: `${this.env.shoppingCartApiBaseUrl}/${this.env.swaggerRoutePath}`,
      title: 'ShoppingCart API',
    },
    {
      url: `${this.env.emailApiBaseUrl}/${this.env.swaggerRoutePath}`,
      title: 'Email API',
    },
  ];
}

interface IIframeEndpoint {
  url: string;
  title: string;
}

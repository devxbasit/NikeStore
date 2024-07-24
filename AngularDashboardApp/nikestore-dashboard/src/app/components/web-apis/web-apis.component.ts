import { Component } from '@angular/core';
import { IIframeEndpoint } from 'src/app/interfaces/IIframeEndpoint.interface';
import { environment } from 'src/environments/environment';
import { IEnvironment } from 'src/environments/ienvironment.interface';

@Component({
  selector: 'app-web-apis',
  templateUrl: './web-apis.component.html',
  styleUrls: ['./web-apis.component.css'],
})
export class WebApisComponent {
  env: IEnvironment = environment;

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

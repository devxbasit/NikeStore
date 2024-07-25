import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IEnvironment } from 'src/environments/ienvironment.interface';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css'],
})
export class AboutComponent {
  env: IEnvironment = environment;

  nikeStoreFeatures: string[] = [
    'Designed & implemented <strong>6 microservices</strong> responsible for authentication, order & email processing & shopping cart, product & coupon management.',
    'Integrated Stripe payment gateway with OrderAPI to handle customer order <strong>payments & refunds</strong>.',
    '<strong>Set up RabbitMQ</strong> as the messaging broker for asynchronous communication between microservices.',
    'Implemented ShoppingCartApi to manage customer <strong>cart items, coupons & discounts</strong>.',
    'Integrated EmailAPI <strong>to streamline background email processing</strong> using Hangfire, MailKit, & RabbitMQ; increased email <strong>throughput & reduced server load</strong>.',
    'Architected & integrated AuthAPI handling <strong>user registration, login, & token generation</strong> with JWT tokens & .NET Identity.',
    'Implemented Ocelot API gateway to provide a unified entry point, streamlining<strong> 25+ API endpoints </strong>into one cohesive access point.',
    'Configured <strong>middleware pipeline</strong> for functionalities like authentication, logging & global exceptional handling & CORS.',
    'Integrated all coupon codes & discount amounts with the Stripe payment gateway & database, ensuring seamless transactions.',
  ];
}

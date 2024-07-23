import { IEnvironment } from './ienvironment.interface';

export const environment: IEnvironment = {
  isProduction: true,

  authApiBaseUrl: 'http://nikestoreauthapi.runasp.net',
  couponApiBaseUrl: 'http://nikestorecouponapi.runasp.net',
  productApiBaseUrl: 'http://nikestoreproductapi.runasp.net',
  orderApiBaseUrl: 'http://nikestoreorderapi.runasp.net',
  shoppingCartApiBaseUrl: 'http://nikestoreshoppingcartapi.runasp.net',
  emailApiBaseUrl: 'http://nikestoreemailapi.runasp.net',

  nikeStoreWebAppBaseUrl: 'http://nikestoreweb.runasp.net',
  nikeStoreApiGatewayBaseUrl: 'http://nikestoregatewaysolution.runasp.net',

  rabbitMqManagerBaseUrl: 'https://chimpanzee.rmq.cloudamqp.com',

  swaggerRoutePath: 'swagger/index.html',
  hangfireServeRoutePath: 'hangfire',
};

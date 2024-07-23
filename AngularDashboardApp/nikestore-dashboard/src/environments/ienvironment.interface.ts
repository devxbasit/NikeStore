export interface IEnvironment {
  isProduction: boolean;
  authApiBaseUrl: string;
  couponApiBaseUrl: string;
  productApiBaseUrl: string;
  orderApiBaseUrl: string;
  shoppingCartApiBaseUrl: string;
  emailApiBaseUrl: string;
  nikeStoreWebAppBaseUrl: string;
  nikeStoreApiGatewayBaseUrl: string;
  rabbitMqManagerBaseUrl: string;
  swaggerRoutePath: string;
  hangfireServeRoutePath: string;
}

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

  LinkedInProfileUrl: 'https://www.linkedin.com/in/devxbasit/',
  professionalEmailId: 'basitshafi.dev@gmail.com',

  GithubProfileUrl: 'https://github.com/devxbasit',
  StackOverFlowProfileUrl:
    'https://stackoverflow.com/users/16456741/basit?tab=profile',
  NikeStoreGithubRepositoryUrl: 'https://github.com/devxbasit/NikeStore',

  swaggerRoutePath: 'swagger/index.html',
  hangfireServeRoutePath: 'hangfire',
  nikeStoreLoginRoutePath: 'auth/login',

  rabbitMqManagerBaseUrl: 'https://ostrich.lmq.cloudamqp.com/login',
  rabbitMqUsername: 'hcidvfyh',
  rabbitMqPassword: '85COkXI6Qx42VSwSJSABqrglJ_4MvCId',
};

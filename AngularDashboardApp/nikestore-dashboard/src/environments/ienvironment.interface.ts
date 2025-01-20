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

  LinkedInProfileUrl: string;
  professionalEmailId: string;
  GithubProfileUrl: string;
  StackOverFlowProfileUrl: string;
  NikeStoreGithubRepositoryUrl: string;

  swaggerRoutePath: string;
  hangfireServeRoutePath: string;
  nikeStoreLoginRoutePath: string;

  rabbitMqManagerBaseUrl: string;
  rabbitMqUsername: string;
  rabbitMqPassword: string;
}

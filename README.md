#### NikeStore

NikeStore is an **enterprise-grade microservice-based** web application built using clean architecture, Ocelot API gateway, RabbitMQ, Hangfire,
JWT tokens, .NET Identity, MailKit & Entity Framework Core

#### Overview view of the project:

Designed & implemented **6 microservices** responsible for authentication, order & email processing and shopping cart, product &
coupon management.

- **Integrated stripe payment gateway** with OrderAPI to handle customer order **payments & refunds**.
- **Set up RabbitMQ** as the messaging broker for asynchronous communication between microservices.
- Implemented ShoppingCartApi to manage customer **cart items, coupons & discounts**.
- Implemented EmailAPI to process & **send emails in the background** using Hangfire, MailKit & RabbitMQ.
- Implemented AuthAPI to hand **user registration, login & token generation** using JWT tokens & .NET Identity.
- Implemented Ocelot API gateway to provide a unified entry point.
- Configured **middleware pipeline** for functionalities like authentication, logging & global exceptional handling & CORS.
- **Synchronised** all the coupon codes & discount amounts **with the stripe payment** gateway & the database.
- Utilized EF Core for object-relational mapping & efficient database access.

#### NikeStore - App Flow

![nike-store microservice app flow](https://github.com/devxbasit/NikeStore/blob/master/ss/nikestore-app-flow.png)

#### Registration

![nike-store microservice app flow](https://github.com/devxbasit/NikeStore/blob/master/ss/register.png)

#### Login

![nike-store microservice app flow](https://github.com/devxbasit/NikeStore/blob/master/ss/login.png)

#### Products Home

![nike-store Products](https://github.com/devxbasit/NikeStore/blob/master/ss/ProductListHome.png)

#### Product Details

![nike-store Product](https://github.com/devxbasit/NikeStore/blob/master/ss/ProductDetails.png)

#### Product Details

![nike-store Product](https://github.com/devxbasit/NikeStore/blob/master/ss/ProductDetails2.png)

#### Customer Cart

![nike-store Cart](https://github.com/devxbasit/NikeStore/blob/master/ss/Cart.png)

#### Checkout

![nike-store Checkout](https://github.com/devxbasit/NikeStore/blob/master/ss/CheckoutSummary.png)

#### Stripe Checkout page

![nike-store Stripe Checkout](https://github.com/devxbasit/NikeStore/blob/master/ss/StripeCheckoutPage.png)

#### Stripe Transactions

![nike-store Stripe Checkout](https://github.com/devxbasit/NikeStore/blob/master/ss/StripeTransactions.png)

#### Stripe Transactions Details

![nike-store Stripe Checkout](https://github.com/devxbasit/NikeStore/blob/master/ss/StripeTransactionsDetail.png)

#### Orders Grid Admin

![nike-store Stripe Checkout](https://github.com/devxbasit/NikeStore/blob/master/ss/OrdersGridAdmin.png)

#### Products Grid

![nike-store Stripe Coupons](https://github.com/devxbasit/NikeStore/blob/master/ss/ProductsGrid.png)

#### Products Add, Update

<p style="text-align: center;">
  <img style="width: 350px; height: auto;" src="https://github.com/devxbasit/NikeStore/blob/master/ss/ProductAdd.png" hspace="10" >
  <img style="width: 350px; height: auto;" src="https://github.com/devxbasit/NikeStore/blob/master/ss/ProductUpdate.png" hspace="10" >
</p>

#### Products Delete

<p style="text-align: center;">
  <img style="width: 350px; height: auto;" src="https://github.com/devxbasit/NikeStore/blob/master/ss/CouponDeleteConfirmation.png" hspace="10" >
  <img style="width: 350px; height: auto;" src="https://github.com/devxbasit/NikeStore/blob/master/ss/CouponDeleteSucces.png" hspace="10" >
</p>

#### Coupons Grid

![nike-store Coupons Grid](https://github.com/devxbasit/NikeStore/blob/master/ss/CouponGrid.png)

#### Database Coupons in Sync with StripeAPI

![nike-store Stripe Coupons](https://github.com/devxbasit/NikeStore/blob/master/ss/StripeCouponsGrid.png)

#### Coupons Add, Update

<p>
  <img style="width: 350px; height: auto;" src="https://github.com/devxbasit/NikeStore/blob/master/ss/CouponAdd.png" hspace="10" >
  <img style="width: 350px; height: auto;" src="https://github.com/devxbasit/NikeStore/blob/master/ss/CouponUpdate.png" hspace="10" >
</p>

#### Coupons Delete

<p>
  <img style="width: 350px; height: auto;" src="https://github.com/devxbasit/NikeStore/blob/master/ss/CouponDeleteConfirmation.png" hspace="10" >
  <img style="width: 350px; height: auto;" src="https://github.com/devxbasit/NikeStore/blob/master/ss/CouponDeleteSucces.png" hspace="10" >
</p>

#### Hangfire Dashboard

![nike-store microservice app flow](https://github.com/devxbasit/NikeStore/blob/master/ss/hangfire-dashboard.png)

#### Hangfire Succeeded Jobs

![nike-store microservice app flow](https://github.com/devxbasit/NikeStore/blob/master/ss/hangfire-succeeded-jobs.png)

#### Hangfire Servers

![nike-store microservice app flow](https://github.com/devxbasit/NikeStore/blob/master/ss/hangfire-servers.png)

#### RabbitMQ Queues

![nike-store microservice app flow](https://github.com/devxbasit/NikeStore/blob/master/ss/rabbit-mq-queues.png)

#### Customer Registration Mail

![nike-store microservice app flow](https://github.com/devxbasit/NikeStore/blob/master/ss/registration-mail.png)

#### Order Confirmation Mail

![nike-store microservice app flow](https://github.com/devxbasit/NikeStore/blob/master/ss/order-confirmation-mail.png)

#### Email Cart Mail

![nike-store microservice app flow](https://github.com/devxbasit/NikeStore/blob/master/ss/email-cart-mail.png)

#### Microservices Architecture Screenshot - 1

<p>
  <img style="width: 350px; height: auto;" src="https://github.com/devxbasit/NikeStore/blob/master/ss/rider-rabbit-mq.png" hspace="10" >
  <img style="width: 350px; height: auto;" src="https://github.com/devxbasit/NikeStore/blob/master/ss/rider-frontend.png" hspace="10" >
</p>

<p>
  <img style="width: 350px; height: auto;"  src="https://github.com/devxbasit/NikeStore/blob/master/ss/sql.png" hspace="10" >
  <img style="width: 350px; height: auto;"  src="https://github.com/devxbasit/NikeStore/blob/master/ss/rider-overview.png" hspace="10" >
</p>

#### Commands

Run in OrderApi & CouponApi

```
dotnet user-secrets init
dotnet user-secrets set "Stripe:SecretKey" "stripe secret key here"
```

#### Other Stuff

```
AppFlowDiagram Here(sign in using mailtobasit74@gmail.com) - https://lucid.app/lucidchart/aa986b8f-13f0-48dd-8023-72f977b2872e/edit?beaconFlowId=79A866E7CA46BB7F&invitationId=inv_0de6832b-7c0e-40e9-8dda-1e23f83e53a3&page=0_0#
```

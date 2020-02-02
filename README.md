# Checkout.com - Payment Gateway API
### Bank-Payment-Merchant ASP.Net Core 2.2  With C#.Net, EF and SQL Server ###

- Introduction 
- Application Architecture 
- Design of Application
- Security - JWT Token Based Authentication
- Development Environment
- Technologies
- Tools
- Web Api Endpoints
- Solution Structure
- Swagger: API Documentation
- How to run the Application
- Demo: Merchant Portal & Gateway Client

***
## Introduction ##
This an ASP.NET Core Application that allows a merchant to process a payment through a secure payment gateway. The payment gateway communicates with an issuing bank (simulator) to perform the transactions
required. 

## Application Architecture ##
The diagram illustrates the flow of the application as follows:
- **Client**: The client submits an order for an item on an online store
- **Online Store**: The online store contacts the payment gateway, which in turn requests the client to authenticate himself with his credentials. 
- **Payment Gateway**: Once the authentication is completed, the payment gateway requires the client to confirm and pay for the order
- **Issuing Bank**: The payment gateway confirms the transaction through an issuing bank, which in turns credit the merchant’s account and debit the client’s account
![checkout_1](https://user-images.githubusercontent.com/23207774/73608319-07d86d00-45db-11ea-9be8-78a263df8e82.png)

## Design of Application ##

- Onion Architecture

![checkout_2](https://user-images.githubusercontent.com/23207774/73608937-16765280-45e2-11ea-8053-8e904bddbff2.png)

## Security: JWT Token Based Authentication ##
JWT Token based authentication is implementated to secure the WebApi for the Payment Gateway. When the user logins to the payment gateway, the gateway 
issues a valid token after validating the user credentials. The API Gateway sends the token to the client. The client app uses the token for the subsequent request.

![checkout_3](https://user-images.githubusercontent.com/23207774/73609073-79b4b480-45e3-11ea-92b1-68c538478d58.png)

## Development Environment ##
- Visual Studio 2019
- .NET Core 2.2
- SQL Server Management Studio 2019

## Technologies ##
- C# NET
- ASP.NET Core Web API 

## Tools ## 
- Entity Framework Core (For Data Access)
- Swash Buckle (For API Documentation)
- xUnit (For Unit Testing)

## Web Api Endpoints ## 

### End points configured and accessible through Issuing Bank (Bank Simulator) API ### 
1. Uri: "**api/bank**" [HttPost] - To perform debit and credit transactions

### End points configured and accessible through Payment Gateway API ###
1. Uri: "**api/payment/authenticate**" `[HttPost]` - To authenticate user and issue a valid token
2. Uri: "**api/payment**" `[HttPost]` - To process payment through gateway 
3. Uri: "**api/payment**" `[HttpGet("{id}")]` - To retrieve all transactions by passing a merchant unique identifier as parameter

## Solution Structure ## 

![checkout_4](https://user-images.githubusercontent.com/23207774/73609317-ca2d1180-45e5-11ea-836c-f99811b905d8.png)

## Swagger API Documentation ## 
Swashbuckle Nuget package added to the "Payment.WebApi" and Swagger Middleware configured in the startup.cs for API documentation. when running the WebApi service, the swagger UI can be accessed through the swagger endpoint "/swagger".

```
public void ConfigureServices(IServiceCollection services)
{            
       services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment API", Version = "V1" });
        });
}
```
```
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
{           
     app.UseSwagger();
     app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "post API v1"); });        
}
```




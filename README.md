# Rest Easy Client

![Travis CI Build Status](https://api.travis-ci.org/programatt/RestEasyClient.svg?branch=master)

Rest Easy Client is a Http(s) client library for calling RESTful services. It uses a generics model based convention to generate the routes being called, with overloads to specify routes directly as well.

In the future, I will be adding features such as:
* caching (etags, client only)
* ~~async await~~
* rich configuration
and other bells as and whistles as I think of them.

I started this project in an attempt to streamline some of the http clients I normally write in my day job into something more reusable. This is also inspired by other convention-based frameworks I have noticed in the open source ecosystem as well.

## Usage

#### Install in your project

PM> Install-Package RestEasyClient

#### Using the factory

Create your models / pocos and use the factory to start retrieving rest clients of the type of model. It will autogenerate routes based on that model.

```csharp
var factory = new GatewayFactory("http://dummy.com");
var gateway1 = factory.GetCqrsGateway<MyModel>();
var gateway2 = factory.GetCqrsGateway<MyOtherModel>();
``` 

Do Http calls based on the type and it will build the routes for you based on convention. If they aren't correct, use the overloads to specify the type and let it handle JSON serialization for you:

```csharp
var id = 15;
var result = gateway1.FindById(id);
gateway1.Delete(id);
```

And now the client supports async!

```csharp
var search = new MySearchModel() { Name = "Grendel" };
var result = await gateway1.SearchAsync(search);

```

## Platforms

Rest Easy Client is currently available on the following .NET Frameworks:
* .NET 4.6

Check back again as I plan on supporting more in the near future!
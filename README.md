### Disclaimer

This repository represents my implementation of the book [Microservices in .NET, Second Edition](https://www.manning.com/books/microservices-in-net-second-edition), which is a beginner guide to building microservice applications using the .NET stack and Docker. 

The project focuses on creating an e-commerce shopping cart functionality architected using microservices as its building blocks. 

This repository aims to implement the below diagrams.

![GitHub Logo](/images/application.png)

![GitHub Logo](/images/promotion.png)

Up to now, the following services have been implemented:

* ApiGateway
* LoyaltyProgram
* PriceCalculation
* ProductCatalog
* ShoppingCart
* SpecialOffers

Other technologies also integrated into the project are:

* Nats
* Kibana
* Elasticsearch
* Mongodb

### Running

The simplest way to get it up and running is by typing the following command:

```
docker compose up
```

After the app has started up, head to API's gateway and query some already seeded data:

```
http://localhost:4007/swagger/index.html
```





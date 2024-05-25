# EShopMicroservices

## Introduction

This is a project made alongside [mehmetozkaya](https://github.com/mehmetozkaya/EShopMicroservices/commits?author=mehmetozkaya)'s microservices course on Udemy. However a lot of though was put into adding extra choices and flavors on the solutions that the course brings. This is all in .NET 8.

## Solution

This is a monorepo that holds inside more than 4 microservice projects and 1 Web App made with Blazor. This is a brief description of what they do:
* Catalog.API
  * Restful API with Marten in order to make a hybrid Transactional-Document database
  * Vertical Slice Architecture applied alongside CQRS (MediatR)
* Basket.API
  * Restful API with Marten in order to make a hybrid Transactional-Document database
  * Redis Cache implemented through Decorator and Cache Aside patterns
  * Vertical Slice Architecture applied alongside CQRS (MediatR)
* Discount.API
  * Grpc service connected to a SQLite database
  * CQRS (MediatR)
  * N-Layer architecture implemented with
* Ordering.API
  * Restful API connected to a T-SQL database
  * DDD and Clean Code Architecture
  * CQRS (MediatR)
  * Migrations for database Code-First approach

* Everything has been orchestrated with docker-compose in order to establish a production-like environment through one simple command

## Patterns and libraries

Many libraries were used, mostly to connect to the different databases, but also MediatR for the usage of Command and Query classes that also had applied to them Behaviors in order to tackle cross-cutting concerns. Hare are some other libraries used across all solutions:
* Mapster
* Carter
* FluentValidations
* gRPC

* Technologies utilized to manage to get things up and running: Docker, 

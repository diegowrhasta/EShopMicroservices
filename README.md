# EShopMicroservices

## Introduction

This is a project made alongside [mehmetozkaya](https://github.com/mehmetozkaya/EShopMicroservices/commits?author=mehmetozkaya)'s microservices course on Udemy. However a lot of thought was put into adding extra choices and flavors on the solutions that the course brings. This is all in .NET 8. This is a full e-commerce platform solution that relies on microservices in order to facilitate purchasing operations.

## Solution

This is a monorepo that holds inside more than 4 microservice projects and 1 Web App made with Blazor. This is a brief description of what they do:
* BuildingBlocks
  * This is shared project that exposes abstract classes, interfaces and dependency injection extensions that can be reused across all the other microservices. DI and base classes for CQRS, Pagination, Exceptions.
* BuildingBlocks.Messaging
  * There is asynchronous communication between a subset of microservices, and so abstractions and interfaces that work with MassTransit's RabbitMQ connector are in here so that they can be reused
* Catalog.API
  * Restful API with Marten in order to make a hybrid Transactional-Document database
  * Vertical Slice Architecture applied alongside CQRS (MediatR)
* Basket.API
  * Restful API with Marten in order to make a hybrid Transactional-Document database
  * Redis Cache implemented through Decorator and Cache Aside patterns
  * Vertical Slice Architecture applied alongside CQRS (MediatR)
  * MassTransit implementation in order to publish events to RabbitMQ
* Discount.API
  * Grpc service connected to a SQLite database
  * CQRS (MediatR)
  * N-Layer architecture implemented with
  * Migrations with EF.
* Ordering.API
  * Restful API connected to a T-SQL database
  * DDD and Clean Code Architecture (Ordering.API, Ordering.Domain, Ordering.Application, Ordering.Infrastructure)
  * CQRS (MediatR)
  * Migrations for database Code-First approach
  * EFC Interceptors to publish integration events after domain events have been acted upon on the Application layer.
  * MassTransit implementation in order to consume events out of RabbitMQ

* Everything has been orchestrated with docker-compose in order to establish a production-like environment through one simple command

## Patterns and libraries

Many libraries were used, mostly to connect to the different databases, but also MediatR for the usage of Command and Query classes that also had applied to them Behaviors in order to tackle cross-cutting concerns. Hare are some other libraries used across all solutions:
* Mapster
* Carter
* FluentValidations
* gRPC
* MassTransit
* Scrutor

* Technologies utilized to manage to get things up and running: Docker.

## Design Choices

* **Custom Exception Handler** that will handle all manners of exceptions based on their type and hydrating an informative and clean json response when an exception is thrown.
* **Health checks** in all services that go into checking the healthchecks of their own backing services (databases, caches)
* **Pipeline behaviors** in the form of behaviors injected into the CQRS Handler pipelines. ValidationBehavior (to validate payloads through FluentValidations), LoggingBehavior (to log all the queries and commands that are being sent to the microservice).

## Extra notes

GitHub's dependabot has been configured in this repository in order to check for updates to packages and stay up to date.

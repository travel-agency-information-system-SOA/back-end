# Travel agency information system (service-oriented architecture)  

This project represents an upgrade in software design, where we transitioned from a monolithic application we previously designed to a service-oriented architecture. 

You can find more information about the project's basis [here](https://github.com/travel-agency-information-system/back-end).

# Back-end

The central part of our application, which we refer to as the **back-end**, represents the remaining portion of our monolithic application. Over time, we gradually deactivated its services and moved the logic into separate applications (microservices) written in different languages, each using its own database.

Initially, we called the microservices methods through controllers by accessing the appropriate paths. Eventually, we transitioned to RPC (gRPC) where we invoked remote service methods by sending protobuf messages.

Our back-end has largely evolved into an **API Gateway** that offers a RESTful API, accessed by clients (front-end). Internally, the application services use gRPC. The API Gateway provides the necessary translation between the external and internal APIs.

The microservices that make up our application, which are called through the back-end (API Gateway), include:
- Tours
- Encounters
- Followers

Requests are sent from the client side (front-end).

We run all the components together using Docker, which allows us to efficiently manage and orchestrate the entire system.

## Transition to gRPC

The transition to gRPC represents a significant step in enhancing our microservice architecture. This remote procedure call (RPC) framework offers numerous advantages over traditional RESTful APIs, especially in complex systems with a large number of microservices.

gRPC uses Protocol Buffers (Protobuf) for data serialization, which allows for faster and more efficient encoding and decoding compared to JSON used in REST APIs. This significantly reduces message size and improves communication speed between services.

Services and methods are defined in .proto files, providing clear and consistent API documentation. This makes it easier to maintain and enhance communication between services.

The transition to gRPC has made our architecture more efficient, scalable, and easier to maintain, improving the overall performance and quality of communication between microservices.

## Observability (Logging, Tracing, Metrics)

To gain a better understanding of our system's functioning and to simplify error resolution, we introduced logging, tracing, and metrics (the three pillars of observability).

## Technologies

- ***Central Back-end***: C# (ASP.NET), serving as the API Gateway, managing requests, and orchestrating the overall application flow

- ***Microservices***:
  - ***Encounters***: Go (Golang) with a document-oriented databse (MongoDB)
  - ***Followers***: Go (Golang) with a  graph database (Neo4J)
  - ***Tours***: Go (Golang) with a relational database (PostgreSQL)
 
- ***Client platform***: Angular (TypeScript, HTML, CSS) with RESTful services for the front-end interface

## Getting started

To set up the project locally using Docker, follow these steps:

```
1. Clone the repository
2. Run the entire setup using Docker Compose
```
- Ensure Docker and Docker Compose are installed and running on your machine
- Use the provided docker-compose.yml file and docker-compose-migrations.yml file to manage the services
- To build and start all services:
```
docker-compose up --build
```
- To stop all services:
```
docker-compose down
```
Use appropriate tools like pgAdmin and MongoDB Compass to interact with the database.

## Configuration

Make sure to review the docker-compose.yml file and Dockerfiles for specific configuration details, such as environment variables, volume mounts, and network settings.

Check the port mappings in docker-compose.yml to ensure that services are properly exposed and accessible.

You can download the files from [this link](https://ufile.io/f/ud3nw). If you are unable to do so, please contact me via email.

## Contributors
- Ana Radovanović
- Kristina Zelić
- Milica Petrović
- Petar Kovačević

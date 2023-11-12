# Fifa-Microservices
A small "FIFA" project that makes use of microservices architecture. The project contains two services, a Club service and a Player service. Both services are .NET 6 Web API's that use EF Core and AutoMapper. Both services are built in Docker and deployed to Kubernetes. HTTP, gRPC and RabbitMQ are used to share/receive data between the services.
# Web API's
The two Web API's in this project (ClubService and PlayerService) are both built in .NET 6 using latest best practices. Both use EF Core, DbContext and the repository pattern. Internal and external DTO's are used for data transfer along with AutoMapper. Each API service connects to its own SQL DB.
# Data Sharing
As proof of concept HTTP/REST, gRPC and RabbitMQ were used to share data between the two services.
# Docker and Kubernetes
Both API services and their respective SQL DB's are built on docker and deployed to a Kubernetes cluster. 
A nodeport is used for networking on a local development environment. Whilst an NGINX controller and load balancer utilizing cluster IP's is used on the production environment within Kubernetes.

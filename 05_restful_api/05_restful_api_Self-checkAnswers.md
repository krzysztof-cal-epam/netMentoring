# Questions for the self-check:

## 1. Explain the difference between terms: REST and RESTful. What are the six constraints?

REST is an architectural style for designing web services. It describes principles and constrains that API priject should follow to be effective.
RESTful can be used to describe system that follows REST architectural style.
The REST constrains:
### 1. Client-server architecture

System is designet as client-server architecture, where the client is responsible for initiating requests and the server is processing requests and sending responses.

### 2. Stateless

Client must take care of providing all required information within one request. Therefore, REST services are easy to scale, because server is not storing any data for the client.

### 3. Cacheable

Cache is being used for improving system performance, reduce latency and server load. Cache allows response from the server to be stored and used by clients without invoking actual network connection.

### 4. Uniform Interface

Is being used for standarising interfaces for interacting between server and the client. Some example of the uniform are the identification of resource, standarised responses i.e. JSON, XML.

### 5. Layered System

Service can be implemented as layered service, which improves scalability and flexibility. There are many variation of layers such as security, trafic management.

### 6. Code on demand

Server can provide executable code (i.e. JavaScript) to the client, therefore improvinf flexibility. This constrint is optional.

## 2. HTTP Request Methods (the difference) and HTTP Response codes. What is idempotency?  Is HTTP the only protocol supported by the REST?

We can distunguish the following HTTP request methods:
### 1. GET

The purpose of this is to retrieve data from the server. This method can be cached.

### 2. POST

The purpose of POST is to create new resource on the server. This method cannot be cached.

### 3. PUT

The purpose of PUT is to update existing data on server. This method cannot be cached.

### 4. PATCH

The purpose of PATCH is to do partial update on an existing resource on the server side. This metchod cannot be cached.

### 5. DELETE

The purpose of DELETE is to remove a resource from the server. This metchod cannot be cached.

### 6. HEAD

The purpose of HEAD is to retrievie headers i.e. to check metadata of the resource. This methed can be cached.

TODO rest of the question

## 3. What are the advantages of statelessness in RESTful services?

todo

## 4. How can caching be organized in RESTful services?

todo


## 5. How can versioning be organized in RESTful services?

todo


## 6. What are the best practices of resource naming?

todo


## 7. What are OpenAPI and Swagger? What implementations/libraries for .NET exist? When would you prefer to generate API docs automatically and when manually?

todo


## 8. What is OData? When will you choose to follow it and when not?

todo


## 9. What is Richardson Maturity Model? Is it always a good idea to reach the 3rd level of maturity?

todo


## 10. What does pros and cons REST have in comparison with other web API types?

todo


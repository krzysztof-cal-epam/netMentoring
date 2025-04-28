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

### 7. Options

The purpose is to describe the communication options available for a specific URL or server. 

### HTTP response codes
We can distinguish several groups of the HTTP response codes that can be described as follows:
#### 1xx : Information

Provides information about the request that is being processed, i.e. "100 continue" - server received initial request and now expecting the client to send the body of the request.

#### 2xx : Success

Informs client that the request was successfully processed. We can distinguish 
##### 200 ok 
Request was successful, the response contains the requested data.

##### 201 created
Request was successful, new resource has been created.

##### 204 no content
Request was successful, no data is returned in the response. 

#### 3xx : Redirection

Indicates that client needs to do additional action, i.e. follow the redirection url. We can distinguish:

##### 301 Moved pernamently
Resource has been relocated to a new location.

##### 302 Found
The resource is temporarly available under a new url.

##### 304 Not Modified
The resource has not been changed.

#### 4xx : Client error
Indicates that the client request was not correct and cannot be processed. We can distunguish:

##### 400 Bad request
The request is not correct

##### 401 Unauthorized
The client needs to authenticate

##### 403 Forbidden
The client does need permission to access the resource

##### 404 Not found
The requested resource could not be found

##### 405 Mothod not allowed
The HTTP method used in this request is not supported for this resource

#### 5xx : Server error
Indicates an error happend on a server-side when processing the request. We can distinguish:

##### 500 Internal Server Error
Generic error message

##### 502 Bad Gateway
Server acting as a gateway received an invalid response from the other server

##### 503 Service unavailable
The server is temporarly unavailable due to i.e. overload traffic etc.

##### 504 Gateway timeout
The server acting as gateway timeouted when waiting for the response from other server

### Idempotency
Reffers to the oprerations that return the same data when repeated, without causing changes. For HTTP idempotent methods are GET, PUT, DELETE, HEAD.

### Other protocols supported by REST
Other protocols supported by REST are HTTPS (secure HTTP), WebSockets or SOAP. However the most common one is HTTP.

## 3. What are the advantages of statelessness in RESTful services?

We can distinguish some advantages of statelessness in Restful:

### Scalability
Since servers do not have to store session information for clients, any available server can handle the client request. Therefore, distribute request accross many servers is easy.

### Reliability
In case of server crash, other servers can be used to handle requests. Moreover, since session information for clients do not have to be saved on server side, memory management is beinf avoided. 

### Simplicity
Stateless makes service less complex, there is no server side state logic at all.


## 4. How can caching be organized in RESTful services?

There are several ways to organize caching in Restful seervices. We can distinguish the following:
### Expires
Specifies the date/time at which the response absolute expires.

### Cache-Control
Determines whether the response is cacheable, who can use cache and for how long.

### ETag (Entity Tag)
Used for checking whether the resource has changed. If teh content has not changed, the server response is 304 Not Modified.  

### Last-Modified
Returns the last modification date/time of the resource. 

## 5. How can versioning be organized in RESTful services?

### Uri versioning
Called also path-based versioning. The api version is included in the URI. Example:
GET /v1/cart/{id}
GET /v2/cart/{id}

### Versioning using custom Request Header
The Api version is specified in a custom HTTP header. Example:
GET /cart/{id}
Headers:
Accept-Version: v1

### Versioning specified in query parameters
Version is being specified in the query as parameter. Example:
GET /cart/{id}?version=1
GET /cart/{id}?version=2

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


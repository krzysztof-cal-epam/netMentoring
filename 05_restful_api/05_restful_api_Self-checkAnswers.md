# Questions for the self-check:

## 1. Explain the difference between terms: REST and RESTful. What are the six constraints?
REST is an architectural style for designing web services. It describes principles and constrains that API priject should follow to be effective.
RESTful can be used to describe system that follows REST architectural style.
The REST constrains:
- Client-server architecture - System is designet as client-server architecture, where the client is responsible for initiating requests and the server is processing requests and sending responses.
- Stateless - Client must take care of providing all required information within one request. Therefore, REST services are easy to scale, because server is not storing any data for the client.
- Cacheable - Cache is being used for improving system performance, reduce latency and server load. Cache allows response from the server to be stored and used by clients without invoking actual network connection.
- Uniform Interface - Is being used for standarising interfaces for interacting between server and the client. Some example of the uniform are the identification of resource, standarised responses i.e. JSON, XML.
- Layered System - Service can be implemented as layered service, which improves scalability and flexibility. There are many variation of layers such as security, trafic management.
- Code on demand - Server can provide executable code (i.e. JavaScript) to the client, therefore improvinf flexibility. This constrint is optional.

## 2. HTTP Request Methods (the difference) and HTTP Response codes. What is idempotency?  Is HTTP the only protocol supported by the REST?

We can distunguish the following HTTP request methods:
- GET - The purpose of this is to retrieve data from the server. This method can be cached.
- POST - The purpose of POST is to create new resource on the server. This method cannot be cached.
- PUT - The purpose of PUT is to update existing data on server. This method cannot be cached.
- PATCH - The purpose of PATCH is to do partial update on an existing resource on the server side. This metchod cannot be cached.
- DELETE - The purpose of DELETE is to remove a resource from the server. This metchod cannot be cached.
- HEAD - The purpose of HEAD is to retrievie headers i.e. to check metadata of the resource. This methed can be cached.
- Options - The purpose is to describe the communication options available for a specific URL or server. 

### HTTP response codes
We can distinguish several groups of the HTTP response codes that can be described as follows:

#### 1xx : Information
Provides information about the request that is being processed, i.e. "100 continue" - server received initial request and now expecting the client to send the body of the request.

#### 2xx : Success
Informs client that the request was successfully processed. We can distinguish 
- 200 ok - Request was successful, the response contains the requested data.
- 201 created - Request was successful, new resource has been created.
- 204 no content - Request was successful, no data is returned in the response. 

#### 3xx : Redirection
Indicates that client needs to do additional action, i.e. follow the redirection url. We can distinguish:

- 301 Moved pernamently - Resource has been relocated to a new location.
- 302 Found - The resource is temporarly available under a new url.
- 304 Not Modified - The resource has not been changed.

#### 4xx : Client error
Indicates that the client request was not correct and cannot be processed. We can distunguish:

- 400 Bad request - The request is not correct
- 401 Unauthorized - The client needs to authenticate
- 403 Forbidden - The client does need permission to access the resource
- 404 Not found - The requested resource could not be found
- 405 Mothod not allowed - The HTTP method used in this request is not supported for this resource

#### 5xx : Server error
Indicates an error happend on a server-side when processing the request. We can distinguish:

- 500 Internal Server Error - Generic error message
- 502 Bad Gateway - Server acting as a gateway received an invalid response from the other server
- 503 Service unavailable - The server is temporarly unavailable due to i.e. overload traffic etc.
- 504 Gateway timeout - The server acting as gateway timeouted when waiting for the response from other server

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
- Expires - Specifies the date/time at which the response absolute expires.
- Cache-Control - Determines whether the response is cacheable, who can use cache and for how long.
- ETag (Entity Tag) - Used for checking whether the resource has changed. If teh content has not changed, the server response is 304 Not Modified.  
- Last-Modified - Returns the last modification date/time of the resource. 

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

Thise are the best practices for resource naming:

### Use nouns
Resources in REST represents entities, not actions. Therefore, names should be nouns like 'carts' or 'cart', instead of verbs like 'getCarts'.

### Plurar nouns for collections
Use plurar nouns for collections of resources and singular nouns for individual entities. This way logical consistency is being reinforced.

### Consistency
Use consistency for naming resources in order to improve readibility and decrease ambiguity. Moreover: 
- use hypens '-' instead of underscore '_'.
- Avoid use of trailing forwars slash '/'.
- Use lowercase letters for convinient.
- do not use file extensions.
- do not use CRUD function names.
- use query component (parameters) for filtering resource collections

## 7. What are OpenAPI and Swagger? What implementations/libraries for .NET exist? When would you prefer to generate API docs automatically and when manually?

### OpenAPI and Swagger
OpenAPI is a specification for defining RESTful API, it also provides format standard to describe the structure of API. It can be processed by machines, thus it allows to generate documentation automatically.
OpanAPI definition are written in standard format as JSON or YAML.

Swagger is a set of tools build around OpenAPI specification. These tools help to design, document, test and consume api. 
I.e. Swagger UI generates interactive documentation from OpenAPI definition which allows to test endpoints from a web browser.

### .Net libraries for OpenAPI and Swagger
Swashbuckle - library used for automaticly generate documentation. It includes Swagger UI for interactive documentation.
NSwag - library that generates OpenAPI from the code 

### When to generate docs automatically and when manually?
Auto-generated documentation will be best suited for the:
- large API - enormous amount of code, therefore a lot of documents generated automatically improves project development time.
- reinforced interactive testing - changes can be tested manually fast.
- improved maintanance - changes are reflected in docs automatically.
- standardized API - controllers that follows consistent conventions can be easly automatically documented .

Manually generated documentation will be best suited for the:
- Complex api - api that requires strict control, i.e. containing Hypermedia HATEOAS.
- small API - small amount of endpoints to be documented might be just fine for manuall documentation creation.

## 8. What is OData? When will you choose to follow it and when not?
OData (Open Data Protocol) is a open protocol for building and consuming REST api developed by Microsoft. 
It is worth to follow it for the given scenarios:
- dynamic querying,
- rapidly changing apis,
- large entities with relations data.

It is not worth to follow it for:
- performance critical api,
- simple api.

## 9. What is Richardson Maturity Model? Is it always a good idea to reach the 3rd level of maturity?
The RMM is a concept for evaluation of RESTful api based on its adherence to REST principles. There are 4 levels, each level enhances the previous one. We can distinguish:
- Level 0: The swamp of POX - api operates within one endpoint and the api does not leverage RESTful principles.
- Level 1: Resources - api uses several endpoints, however action methods are still not using all HTTP (i.e. only POST).
- Level 2: HTTP Verbs (Methods) - api uses several endpoints and fully utilizes HTTP methods. Api also uses HTTP response codes correctly.
- Level 3: Hypermedia as the Engine of Application State (HATEOAS) - relation between resources is enhanced by hypermedia (links). Api responses includes hypermedia for each endpoint.

In some scenarios it might not be neccessary to acheive level 3 of RMM. Some scenarion are described below:
- performance - some apis might need to have great performance and HATEOAS can slow down them.
- lack of such requirenment - client might not want/need this level. i.e. api for simple CRUD operations.

## 10. What does pros and cons REST have in comparison with other web API types?

Rest api pros:
- simple - rather simple to implement.
- statelessness - servers do not need keep sesion related data, easy to scale.
- cacheable - improve data payload.
- human readable

Rest api cons:
- limited real-rime capability - real time updates might need other architecture i.e. websockets.
- quering not standardized - quering might differ based on framework used.
- low performance for complex data - REST does not support nested resources

REST can be compared with other web api types. In the given scenarion those web apis will be better-suited:
- GraphQL - will be better for managing complex interrelated data structures that must be quiried.
- gRPC - will be better for high performance and real-time streaming.
- SOAP - will be better for applications that requires high secure operations.
- WebSockets - will be better for real-time communication like chat bots, dashboard etc.

# Questions from Self-check:

## 1. Name examples of the layered architecture. Do they differ or just extend each other?

The list of examples of the layered architecture:
### 1. N-Layer Architecture 
General/common architecture for a layered pattern. Number of layers might differ (this is why N-tier), but it's usually layers of presentation, business, persistance and database .
### 2. Model-View-Controller 
Common version of a N-tier architecture for web development. It defines layers: Model, View and Controller.
### 3. Model-View-ViewModel 
Common version of a N-tier architecture for desktop development. It defines layers: Model, View and ViewModel.
### 4. Clean architecture 
Layered architecture that follows the concept of SOLID principles and Clean Code by Robert C. Martin.

MVC and MVVM extends the N-Layer architecture to reinforce specific interaction (MVC - web development and MVVM desktop development). 
Clean architecture extends N-Layer architecture in such a way that besides defining leyers it also follows concepts of clean code and SOLID principles. It also introduces strict rules about the dependencies in the project and on core logic / domain isolation.

## 2. Is the below layered architecture correct and why? Is it possible from C to use B? from A to use C?

The presented layered architecture is not correct, because it creates a circular  dependency. We call a circular dependency the situation when layer C is dependent upon layer B and layer B is also dependent upon layer C. Moreover, this type of graph also is tightening the concerns, while one of the key components for layered architecture is to have separated concerns.
It should not be possible from C to use B, because B is already dependent upon C. Such a relation is causing circular dependencies.
Case from A to use C. According to the concept of layers of isolation it is forbidden to call from A to C. 'Layers of isolation' concept states that changes made in one layer don't impact another layer. The change is isolated within one layer AND POSSIBLY another ASSOCIATED layer. Therefore, associated layer to A is B, so A should not use C. However, if we consider concept of open layers where the flow can be bypassed through the layers to the bottom layer, it should be possible from A to use C. Overall such a bypass should be avoided, since it can cause tightly coupled modules, which might become hard to maintain.

## 3. Is DDD a type of layered architecture? What is Anemic model? Is it really an antipattern?

Domain Driven Design (DDD) is not a layered architecture. DDD does not structure the code in a layers, but rather focuses on modeling the application based on a domain.
We call a model anemic when the domain model does not contain bussines logic, but only contains data (i.e. properties). In DDD domain models should also contain all busines logic in place (in the same domain), and not have it calsulated in some external services or functions.
Anemic model is being considered as antipatern, because it violates Single Responsibility Principle which states that class should have only single responsibility and in case of DDD single responsibility refers to the domain object and domain object behavior. Therefore, any behavior (logic) should remain in the same class as the domain object data.

## 4. What are architectural anti-patterns? Discuss at least three, think of any on your current or previous projects.

Examples of architectural anti-patterns:
### 1. Spagetti code. 
Describes a software that is not structured anyhow, does not follow any architecture nor design pattern. Any code can be called from anywhere, which is making the code hard to understand, maintain and debug. I can image an exapmple of a spagetti code as an e-commerce shop, where all the processes (orders, payments, inventory updates, confirmations, etc.) are being handled in one class or method - when user clicks buy button. 

### 2. Big Ball of Mud
Same as spagetti code, but differs in scope. Big ball of Mud is usually related to the whole system architecture. I can image it as an example of a e-commerce platform, where UI layer queries database layer directly, business logic exists in each layer, etc.

### 3. God object
Refers to the situation that sincle class is carring to many responsibilities. It violates the Single Responsibility principle. I can image this antipatern as an example of e-commerce e-shop that contains only one class i.e. "ShopManager", which is responsible for everything. That icludes order processing, payment processing, inventory management, users/customers management, notifications, etc. 

## 5. What do Testability, Extensibility and Scalability NFRs mean. How would you ensure you reached them? Does Clean Architecture cover these NFRs?

Testability specifyes how effectively parts of the project can be tested. System with high testability can be verified easly, whether each part works correctly.
Extensibility specifies how flexible the project is. The system with high extensibility allows to extend it by the new features without modifying existing code or witch a little of modifications.
Scalability specifies how the system can handle increased load. System with high Scalability can be easly scaled (vertically or horizontally) to handle larger load (more requests, more users, more data, etc.).

High testability can be achived with by applying the following patterns:
### Dependency Injection 
Helps to mock dependencies with fake data, which makes it easy to write integration tests.
### Single Responsibility Principle 
Keeps logic in one place and separates concerns.

High Extensibility can be achived by applying the folowign design patterns / architectural styles:
### Open/Closed Principle 
According to the definition code should be open for extension and closed for modification, therefore raising level of Extensibility,
### Microservices 
Each microservice can be extended independently. 

High Scalibility can be acheived by applying the folowing:
### Microservices 
Invidual microservices can be scalled independently,
### Asynchronous and pararell programming 
Allows to execute code in pararel by multiple threads, therefore raising scalability level.

Clean Architecture covers Testability, Extensibility and Scalability Non functional requirenments.
It strongly emphasizes separation of concerns, therefore improving Testability and Scalability.
Clean Architecture defines also the flow of dependencies in the architecture, therefore making it framework, database and UI independent. Thus, every framework can be easly exchanged, therefore improving the Extensibility. 

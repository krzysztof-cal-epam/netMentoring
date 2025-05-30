# Questions for the self-check:

## 1. What is the difference between authentication and authorization?
Authentication verifies the identity of a user or system. Usually i.e. checking if a user is who it claims to be by credentials like username and password.
Authorization determines what an authenticated user is allowed to do. Specifically it checks is user granted to assecc to specific resources or action based on permissions.

## 2. What authorization approaches can you list? What is role-based access control?

### What authorization approaches can you list?
We can distinguish several authorization approaches:
- Role Based Access Control - Access is granted based on predefined roles that were assigned to the user,
- Attribute Based Access Control - Access decisions are based on attributes i.e. user location, time, device,
- Discretionary Access Control - Resource owners access permission. In this case access is granted by an invidual iser with authority,
- Mandatory Access Control - Central policies define access control based on a classification system. In this case access is enforced based on strict security policy,
- Policy based Access Control - Combines rules and policiesto dinamically determine access. Access is defined by i.e. JSON or YAML configurations,

### What is role-based access control?
Role based access control is an access control approach where permissions are assigned to the specific roles (like admin, user) instead of directly to the invidual users. 

## 3. What exactly is Identity Management (Identity and Access Management)?
Identity and Access Management (IAM) is a framework for managing identities and controlling access to resources. It ensures that the right user has the appropriate access to the given resource at the right time. It also involves:
- Identity management - creating, storing, managing user identities,
- Access Management - controling what authenticated user can access based on the policies, roles.

## 4. What authentication/authorization protocols do you know? What is the difference between OAuth & OpenID?

### What authentication/authorization protocols do you know?
We can distinguish several authentication/authorization protocols:
- OAuth - is an authorization protocol for granting access to the resourcess without sharing credentials,
- OpenID Connect (OIDC) - is an authentication layer built on OAuth 2.0, used for verifying user identity,
- SAML (Security Assertion Markup Language) - is being used for exchanging authentication and authorization data.

### What is the difference between OAuth & OpenID?
There are several differences between OAuth & OpenID:
- OAuth is focusing on authorization, allowing 3rd party apps to access resources,
- OpenID is focusing on authentication and it verifies user identity and provides user information. It also includes authorization.

## 5. What is Authentication/Authorization Token. What is JWT token? What other approaches except authentication/authorization, can we use with security token?

### What is Authentication/Authorization Token? 
Authentication/Authorization Token is a digital object issued after success authentication. It is used to prove identity or access rights in order to access the given resource.

### What is JWT token?
JWT token is also a token. It is compact, self-contained token format consisting of Header, Payload and signature encoded in Base64. It is used for authentication and authorization. 

### What other approaches except authentication/authorization, can we use with security token?
We can distinguish the following other approaches to use with security token:
- Session management - tokens can be used to maintain user sessions, and track users across requests,
- API Security - secure API calls by validating tokens,
- Data integrity - ensure data was not changed.

## 6. What is Single Sign-On (SSO)? Name the steps to implement SSO. What are the benefits of SSO?

### What is Single Sign-On (SSO)?
Sign-On (SSO) is an authentication mechanism where a user logs in once and gains access to multiple independent applications without need to log in again. 

### Name the steps to implement SSO.
Steps to implement SSO are the following:
1. Choose an Identity Provider (IDP) to manage user identities,
2. Set up application - configure application to work with the chosen identity provider,
3. Integrate SSO protocol to enable secure communication between IDP and the application,
4. Create a login page - page where user enters credentials,
5. Enable token generation - configure IDP to issue tokens when user successfully logs in,
6. Configure app to accept tokens - modify the app to access security tokens. When user tries to access the application, app will check whether the provided token is valid. If it is, the user gets logged automatically.

### What are the benefits of SSO?
Benefits of Single Sign On are the following:
1. Improved user experience - user has to login only once for multiple applications,
2. Improved security - centralized authentication will improve overal security. Also fewer password to be remmembered - user needs to remmember only one strong password,
3. Increased productivity - faster access to applications without repeated logins.

## 7. What is the difference between Two-Factor Authentication and Multi-Factor Authentication?
Two-Factor Authentication requires exactly wto distinct authentication factories i.e. password and sms code.
Multi-Factor Authentication tequires two or more authentication factories. In this case i.e. password, mobile phone sms code and biometrics. Multi-Factor Authentication includes Two-Factor Authentication, it is a broader security approach.

## 8. Which of the OAuth flows can be used for user (customer) and which for client (server) authentication?
OAuth flows for User (customer) Authentication can be used for authorization web applications, where user authenticates via web browser. 
OAuth flows for Clients (server) Authentication can be used for server to server communication, where the server authenticates to access resourcess without user involvement.

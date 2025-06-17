# Questions for the self-check:

## 1. What is orchestration?
Term orchestration refers to the automated management, coordination and deployment of multiple containers or services to ensure they work together. This includes scaling, load balancing, service discovery, resource allocation, disaster recovery and fault tolerance. 

## 2. What is containerization and the pros and cons of using it?
Contenerization is a type of virtualization that allows applications and their dependencies to be deployed into portable units called containers. Those containers run in an isolated environments.
Pros od contenerization:
1. Portability - containers run accross different environment like OS (Windows, Linux, Mac),
2. Efficency - containers uses fewer resources than Virtual Machines,
3. Scalability - containers can be scaler up to handle increased workload,
4. Isolation - containers run in their own environment, therefore do not affecting other applications.

Cons of contenerization:
1. Security - containers share the host core, therefore compromised container could potentially affect the host and other containers,
2. Complexity - managing large numbers of containers requires orchestration tools like Kubernetes, which can be difficult to manage,
3. Limited isolation - containers are less isolated than Virtual Machines. 

## 3. What is the difference between containerization and virtualization?
We can distingush several differences between container and virtual machines (VM):
1. Containers package application with the dependencies into single container that share host OS kernel, while VM runs entire OS on virtual hardware,
2. Containers are isolated but shares host OS kernel, while VMs are creating whole OS with separated kernels,
3. Containers use minimal CPU, memory and storage, while VMs require more resources to run the whole OS kernel,
4. Containers have better performance, while VS performance is significantly worse due to need of emulation OS,
5. Containers sturtup time is fast, while VMs needs more time to boot OS. 

## 4. Explain the usage flow of Docker & Kubernetes.
The Docker usage flow is as follows:
1. Create Dockerfile - a file that lists instructions how to start application with all the required dependencies,
2. Build a docker image - based on the dockerfile,
3. Run container - can be run locally or on a dedicated server. Running container lets to test whether the application works correclty,
4. Push to registry - optionally container can be shared to a storage place,
5. Manage container,

The Kubernetes usage flow:
1. Setup a Kubernetes cluster - group of nodes which are being hosted on a cloud service,
2. Define Kubernetes resources - write yaml manifest, which describes the application i.e. how many instances of the application should be run,
3. Deploy to cluster - Kubernetes starts the container on the cluster accross the nodes,
4. Expose Application - define how users can access the application, Kubernetes will set up the connection service,
5. Scale and update - Kubernetes can run more containers for heavy traffic, and update the application by pushing new container image,
6. Monitor and manage - Kubernetes will handle containers failure, if the container crashes, it will start a new one automatically.

## 5. What are the best practices for containerization?
The best practices for contenerization are the following:
1. Avoid modifying running containers - better to stop, rebuild and redeploy image,
2. Manage secrets securely - avoid hardcoding secrets in images - use env variables or other secret vaults,
3. Keep container small - avoid using heavy images, rather use lighweighted distribution i.e. Linux Alpine,
4. Use orchestration - use Kubernetes or similar tools for large scale container system.

## 6. How is Docker CI different from classic CI pipeline?
Clasic CI pipeline runs tasks direcltly on the dedicated server without contenerization, while Docker CI uses containers for building, testing and deploying applications. 
Docker CI is more consistent than classic CI, due to isolation and identical environment.
Docker CI might also be faster than classic CI. Containers can be setup within seconds, while Classic Ci might require more time to setup environment configuration.
However, Docker CI require Docker specific knowledge, therefore it is more complicated to manage than classic CI.

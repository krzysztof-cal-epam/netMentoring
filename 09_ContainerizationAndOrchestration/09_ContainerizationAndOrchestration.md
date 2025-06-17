# Questions for the self-check:

## 1. What is orchestration?
Term orchestration refers to the automated management, coordination and deployment of multiple containers or services to ensure they work together. This includes scaling, load balancing, service discovery, resource allocation, disaster recovery and fault tolerance. 

## 2. What is containerization and the pros and cons of using it?
Contenerization is a type of virtualization that allows applications and their dependencies to be deployed into portable units called containers. Those containers run in an isolated environments.
Pros od contenerization:
1. Portability - containers run accross different environment like OS (Windows, Linux, Mac),
2. Efficency - containers uses fewer resources than Virtual Machines,
3. Scalability - containers can be scaler up to handle increased workload,
4. Isolation - containers run in tehir own environment, therefore do not affecting other applications.

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
1. Create Dockerfile,
2. Build a docker image,
3. Run container,
4. Push to registry,
5. Manage container,

The Kubernetes usage flow:
1. Define Kubernetes resources - yaml manifest,
2. Deploy to cluster,
3. Expose Application,
4. Scale and update,
5. Monitor and manage.

## 5. What are the best practices for containerization?
The best practices for contenerization are the following:
1. Avoid modifying running containers - better to stop, rebuild and redeploy image,
2. Manage secrets securely - avoid hardcoding secrets in images - use env variables or other secret vaults,


## 6. How is Docker CI different from classic CI pipeline?
Clasic CI pipeline runs tasks direcltly on the dedicated server without contenerization, while Docker CI uses containers for building, testing and deploying applications. 

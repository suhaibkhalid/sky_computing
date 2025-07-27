# Intra-Cloud (Horizontal) Migration
Overview

This project demonstrates a basic load distribution strategy using:

- Two Node.js applications (app1 and app2)
- An NGINX reverse proxy
- Docker containers (to run apps without pm2)
- A basic threshold-based routing logic
- A simple load-testing script (test-req) to validate request routing

When total requests cross a defined threshold (e.g., 10), traffic is automatically rerouted from Node App 1 to Node App 2.

# Project Structure
project-root/
├── app1/                    # Node App 1
│   ├── Dockerfile
│   └── index.js
├── app2/                    # Node App 2
│   ├── Dockerfile
│   └── index.js
├── nginx/
│   ├── Dockerfile
│   └── default.conf         # NGINX reverse proxy config
├── docker-compose.yml       # Multi-container orchestration
└── test-req/
    └── test.js              # Axios script to simulate multiple requests
    
# Setup Instructions
1. Clone / Prepare Your Project
   Ensure your folders/files are laid out as shown above.

2. Build and Run the Containers
   From the root of the project, run:
   docker-compose up --build -d

   This will:
   - Build Docker containers for both apps and NGINX
   - Start all containers in detached mode

3. Verify All Services
   Run:
   docker ps

   Expected services:
   - node-app1
   - node-app2
   - nginx

# How It Works
Node App 1:
- Listens for incoming HTTP requests
- Maintains a request counter (in-memory)
- If request count is ≤ threshold (e.g., 10), it responds itself
- If request count is > threshold, it proxies the request to App 2

Node App 2:
- Just handles and responds to incoming requests
- Used when load crosses threshold


# NGINX
- Reverse proxies all requests to App 1 at the start
- Could be extended to handle load balancing as needed

**Note:** The request counter resets if the container restarts. This is acceptable for demo/testing purposes.
# How to Test (Using Axios in test-req/)
1. Navigate to Test Folder
   cd test-req

2. Install Dependencies
   npm install axios

3. Run the Load Simulation Script
   npm run start

**Example Output:**
[Request 1] Response from App 1
[Request 2] Response from App 1
... 
[Request 11] Response from App 2
[Request 12] Response from App 2

This verifies:
- First 10 requests are handled by App 1
- Requests beyond the threshold go to App 2
Optional Enhancements
- Use Redis or shared memory to manage request counters across instances
- Integrate NGINX’s built-in load-balancing for true horizontal scaling
- Add metrics/logging to monitor which app served the request

# NGINX Role Details

# What is NGINX ?
NGINX (Engine-X) is a powerful, lightweight web server and reverse proxy. It’s used to:
Web Serve: Serves static files (HTML, CSS, JS, etc.) directly to users
Reverse Proxy: Receives HTTP requests and forwards them to backend services (like Node.js apps)

# Scope in our Project
To simulate a load-balanced system where requests go to:
App1, until it hits a threshold (e.g., 10 requests
Then App1 forwards excess load to App2
NGINX can make this system scalable, organized, and cloud-ready.


# NGINX Role in our project
Role:  What NGINX Does                                                        
Reverse Proxy:  Forwards all HTTP requests to our Node.js backend(s)                  
Load Balancer:  Chooses App1 or App2 based on current load (via `least_conn`)            
Gatekeeper:  Listens on public port 80 (EC2) and routes traffic internally via Docker 
Scalability Enabler: Allows you to add more Node.js apps without rewriting routing logic     

# Benefits of NGINX
Reverse : Decouples your client from backend complexity 
Load Balancer: Distributes requests efficiently           
Modular: Easily add App3, App4, etc. later     
External Access: NGINX binds to EC2 port 80, not your apps   
Optional SSL: Can be extended to support HTTPS in the future



# Without NGINX (Just Node Apps)
We have to expose App1 and App2 on different ports (3000, 4000)
We need a frontend or a script to decide when to use App1 vs App2
We have to maintain routing logic manually

# Project Documentation
Workload Distribution Using Dockerized Node.js Apps and NGINX on AWS EC2**

# Project Objective
The goal of this project is to demonstrate load distribution (Intra-cloud migration) between multiple Node.js servers using a Dockerized architecture with NGINX as a reverse proxy, 
hosted on an Amazon EC2 instance (Free Tier). The system ensures that incoming client requests are handled efficiently and forwarded intelligently based on server load conditions.

#System Architecture
**Components**:
Local Client: Sends HTTP requests to the public IP of the EC2 instance.
NGINX Container: Acts as the load balancer and reverse proxy.
App1 Container: Primary request handler (Node.js app).
App2 Container: Secondary/overflow request handler (Node.js app).
Docker Engine: Manages container orchestration.
EC2 Instance: Host server running Amazon Linux 2.



Architecture Diagram:
+------------------+
|   Local Client   |
+--------+---------+
|
v
[EC2 Public IP :80]
|
+-------+--------+
|     NGINX      |
|  (Load Balancer)|
+-------+--------+
|
+-----------+-----------+
|                       |
+--------------+     +----------------+
| App1 (Node.js) |   | App2 (Node.js) |
|   port 3000    |   |    port 4000   |
+----------------+   +----------------+

**Technologies Used**
Amazon EC2: Cloud hosting of the application (Free Tier) 
Docker:  Containerization of backend services         
Node.js: Backend API servers (App1 and App2)          
NGINX: Reverse proxy and load balancer              
Git & GitHub: Source control and deployment repo           
Bash/SSH: Secure remote access to EC2 instance         
curl / Axios: Testing HTTP requests from local machine

# Deployment Workflow
1.	Project Setup:
	Created and pushed the application to a GitHub repository.
2.	AWS EC2 Configuration:
	Launched a Free Tier EC2 instance using Amazon Linux 2.
	Generated a .pem key for secure SSH access.
3.	Environment Setup on EC2:
	SSH’d into the EC2 instance.
	Installed Docker and Git.
	Cloned the project from GitHub.

# connect to the EC2 instance from local terminal with the pem file
ssh -i sky_computing.pem ec2-user@<You_EC2_Instance_Public_IP>

# clone project into EC2 instance from github
git clone https://suhaibkhalid:ghp_xxxxxxx@github.com/suhaibkhalid/sky_computing.git

# On new instance
sudo yum update -y
sudo yum install docker -y
sudo service docker start
sudo usermod -aG docker ec2-user


# Install Docker Compose
sudo curl -L "https://github.com/docker/compose/releases/download/v2.24.6/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose

# Reboot to apply group change
sudo reboot


4.	Security Configuration:
	Updated EC2's inbound rules to allow HTTP (port 80) and SSH (port 22) traffic.

5.	Dockerization:
	Built Docker containers for:
	App1 (Node.js)
	App2 (Node.js)
	NGINX (proxy server)
	All services orchestrated using Docker Compose.

6.	Running the Application:
	Used Docker Compose to run all containers.
	NGINX exposed on port 80; internal routing to App1 and App2 based on load.

# Command: '
docker-compose up --build -d
docker ps

You should see something like this:
CONTAINER ID   IMAGE         ...   NAMES
xxxxxx         app1-image    ...   app1
xxxxxx         app2-image    ...   app2
xxxxxx         nginx-image   ...   nginx

# Install Node.js (if not already):
sudo yum install -y nodejs

# Go to the Test script Folder
cd test-req
npm install axios
npm run start 

**Example Output:**

[Request 1] Response from App 1
[Request 2] Response from App 1
[Request 3] Response from App 1
...
[Request 11] Response from App 2
[Request 12] Response from App 2



# Testing and Load Distribution:
	Sent a sequence of POST requests from the local machine using the EC2 public IP.
	Verified how requests were balanced between App1 and App2 via logs and responses.
     How Load Distribution Works
•	NGINX acts as the gatekeeper for all incoming HTTP requests.
•	Based on connection load (using the least_conn policy), NGINX routes requests to:
	App1 by default, until it reaches a threshold
	App2 when App1 is busy or forwarded explicitly
•	This simulates a threshold-based workload offloading strategy, commonly used in scalable distributed systems.



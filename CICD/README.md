# Introduction
The Bank demo web application and associated web services.

# Build and Install
1. In the root directory run following docker commands:
* docker build -t bankapp .
* docker run --name=bankapp -p 8080:8080 -p 9001:9001 bankapp

# Access the Application
Navigate by http://localhost:8080/parabank/index.htm - Application is now accessible
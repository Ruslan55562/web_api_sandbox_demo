# Parabank Project with Test Framework

This project contains the Parabank application along with API and UI test frameworks for testing its functionality. Docker is used for easy setup and deployment.

## Table of Contents
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
  - [Clone the Repository](#clone-the-repository)
  - [Set Execute Permissions for run.sh](#set-execute-permissions-for-runsh)
  - [Build and Run the Docker Container](#build-and-run-the-docker-container)
  - [Example Output](#example-output)
  - [Stopping and Removing the Container](#stopping-and-removing-the-container)
- [Accessing the Application](#accessing-the-application)
- [Running Tests](#running-tests)
- [Contributing](#contributing)

## Project Structure

- **CICD**: Contains `run.sh` script for building and running the Docker container.
- **sandbox_demo_API**: API test framework for testing Parabank's API endpoints.
- **sandbox_demo_UI**: UI test framework for testing the Parabank application interface.
- **web_api_sandbox_demo.sln**: Solution file for the project in Visual Studio.

## Prerequisites

- **Docker**: Ensure Docker is installed and running on your system. [Get Docker](https://www.docker.com/get-started).
- **Environment Variables**: The following environment variables need to be set for HSQLDB:

  ```plaintext
  HSQLDB_USER=sa
  HSQLDB_PASSWORD=empty
   ```
## Installation
### Clone the Repository:

 ```bash
git clone https://github.com/Ruslan55562/web_api_sandbox_demo.git
cd web_api_sandbox_demo
 ```
### Set Execute Permissions for run.sh:

-**Linux/macOS**: Use chmod to make run.sh executable:
 ```bash
chmod +x CICD/run.sh
 ```
-**Windows**: This step is not needed if using Git Bash. Simply run the script directly with:
 ```bash
./CICD/run.sh
 ```
### Build and Run the Docker Container:
To build and run the Parabank application in a Docker container, use the following command:
 ```bash
./CICD/run.sh
 ```
You will be prompted to choose one of the following options:
1. Build and run the Parabank application (builds the Docker image and starts the container).
2. Run the existing Parabank container (runs the already built Docker image).

### Example Output:
```bash
$ ./CICD/run.sh

Select an option:
1. Build and run the parabank application
2. Run the parabank application

Enter option number (1 or 2): 1
```

### Stopping and Removing the Container:
To stop and remove the container manually, use the following commands:
```bash
# Stop the container
docker stop bankapp

# Remove the container
docker rm bankapp
```
## Accessing the Application

Once the container is running, you can access the Parabank application and its API documentation:

- **Parabank Application**: Access the application at:
  ```bash
  http://localhost:8080/parabank
  ```
- **API Documentation (Swagger)**: Access the Swagger documentation for the Parabank API at:
    ```bash
   http://localhost:8080/parabank/api-docs/index.html
  ```

## Running Tests

To run the tests, open the solution file `web_api_sandbox_demo.sln` in Visual Studio and follow these steps:

1. Build the solution.
2. Open the **Test Explorer** window.
3. Choose the test you want to run and click the **Run** button.

You can select between the following test projects:

- **API Tests** in the `sandbox_demo_API` directory.
- **UI Tests** in the `sandbox_demo_UI` directory.

After running the tests, a report will be generated automatically and saved in the `Reports` folder of the corresponding project.

## Contributing

Contributions are welcome! Please follow these steps to contribute to the project:

1. **Fork the Repository**: Start by forking this repository to create your own copy on GitHub.

2. **Create a Feature Branch**:
   - If you're working on a new feature, create a branch named `feature/your-feature-name`.
   - For bug fixes, create a branch named `bugfix/your-bugfix-name`.

   ```bash
   git checkout -b feature/your-feature-name
    ```
3. **Make Your Changes**:
- Implement your feature or fix in the code.
- Follow existing code style and conventions.
- Include comments where necessary to explain your code.

4. **Commit Your Changes**:
- Write clear and concise commit messages.
 ```bash
git commit -m "Add feature: your feature description"
 ```

5. **Push Your Changes**:
Push your branch to your GitHub fork.
```bash
git push origin feature/your-feature-name
 ```

6. **Review Process**:
- Your branch will be reviewed by the maintainers.
- You may be asked to make further changes before it is approved and merged.
- Engage in discussions and provide additional context if requested.

Thank you for contributing! Your efforts help improve the project and make it more robust for everyone.

## Troubleshooting

- **Docker Issues**: If you encounter issues with Docker, ensure Docker Desktop is running and you have the latest version installed. You can restart the Docker service to resolve connectivity issues.
- **Test Failures**: If some tests fail, check that the environment variables are correctly set and that the Docker container is running.
- **Accessing Swagger**: If Swagger documentation does not load, confirm that the container is running and that there are no firewall restrictions on port 8080.
- **UI Test Issues**: If you experience issues running the UI tests, it may be necessary to update the WebDriver for your browser (e.g., Chrome, Edge, or Firefox). Ensure that the WebDriver version matches the installed browser version:

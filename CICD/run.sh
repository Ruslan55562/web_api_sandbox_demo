#!/bin/bash

IMAGE_NAME="bankapp"
CONTAINER_NAME="bankapp"

build_and_run() {
    echo "Building and running the container $IMAGE_NAME..."
    docker build -t $IMAGE_NAME . && docker run --name=$CONTAINER_NAME -p 8080:8080 -p 9001:9001 $IMAGE_NAME
}

run_existing() {
    echo "Starting existing container $CONTAINER_NAME..."
    docker start $CONTAINER_NAME && docker attach $CONTAINER_NAME
}

if [ "$(docker ps -q -f name=$CONTAINER_NAME)" ]; then
    echo "Container $CONTAINER_NAME is already running."
    echo "Use option 2 to attach to the running container."
fi

echo "Select an option:"
echo "1. Build and run the parabank application"
echo "2. Run the parabank application"

read -p "Enter option number (1 or 2): " choice

case $choice in
    1)
        if [ "$(docker ps -a -q -f name=$CONTAINER_NAME)" ]; then
            docker rm -f $CONTAINER_NAME
        fi
        build_and_run
        ;;
    2)
        if [ "$(docker ps -a -q -f name=$CONTAINER_NAME)" ]; then
            if [ "$(docker ps -q -f name=$CONTAINER_NAME)" ]; then
                echo "Container $CONTAINER_NAME is already running. Attaching to it..."
                docker attach $CONTAINER_NAME
            else
                run_existing
            fi
        else
            echo "Container $CONTAINER_NAME does not exist. Please select option 1 to build and run first."
        fi
        ;;
    *)
        echo "Invalid input. Please choose 1 or 2."
        ;;
esac
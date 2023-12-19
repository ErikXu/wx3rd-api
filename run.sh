#!/bin/bash

docker rm -f wx3rd-api
docker run --name wx3rd-api -d -p 80:5000 wx3rd-api:latest
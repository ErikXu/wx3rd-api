#!/bin/bash

docker rm -f wx3rd-api
docker run --name wx3rd-api -d -e ASPNETCORE_URLS="http://*:80" -p 80:80 wx3rd-api:latest
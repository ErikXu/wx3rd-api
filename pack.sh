#!/bin/bash

IMAGE_NAME=${IMAGE_NAME:-wx3rd-api}
echo "IMAGE_NAME: ${IMAGE_NAME}"

IMAGE_TAG=${IMAGE_TAG:-latest}
echo "IMAGE_TAG: ${IMAGE_TAG}"

docker build -t $IMAGE_NAME:${IMAGE_TAG} -f ./docker/Dockerfile ./publish/
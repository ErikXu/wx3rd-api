#!/bin/bash

rm -rf ./publish

cd ./src/Wx3rdApi
dotnet publish -c Release -o ../../publish -r alpine-x64  /p:IncludeAllContentForSelfExtract=true /p:PublishSingleFile=true /p:PublishTrimmed=true /p:DebugType=None /p:DebugSymbols=false --self-contained true
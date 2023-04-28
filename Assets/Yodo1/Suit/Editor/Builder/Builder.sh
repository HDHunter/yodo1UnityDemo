#!/bin/bash

filepath=$(cd "$(dirname "$0")"; pwd)
cd $filepath
java -jar Builder.jar
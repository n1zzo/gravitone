#!/bin/sh

for NAME in $(ls)
do
    ffmpeg -i $NAME -vf scale=400:400 planet_$NAME
    echo $NAME
done

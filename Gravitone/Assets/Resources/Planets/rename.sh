#!/bin/sh

i=1
for NAME in $(ls)
do
    mv $NAME $i.png
    echo $i
    i=$[i + 1]
done

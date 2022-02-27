#!/bin/bash

# docker pull alpine/bombardier

ready=false
$mode="manual"

while [[ "$#" -gt 0 ]]; do
  case $1 in
    -s|--site)
      target="$2";
      mode="manual"
      shift ;;
    -a|--auto) mode="auto" ;;
  esac
  shift
done

if [ $mode == "manual" ] ; then
  docker run alpine/bombardier -c 1000 -d 60s -l "$target"
fi

if [ $mode == "auto" ]; then
  while read line; do
    echo $line;
    docker run alpine/bombardier -c 1000 -d 60s -l "$line" &
  done < targets.txt

fi

echo "done"
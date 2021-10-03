#!/bin/bash

if [[ -n `nvr --serverlist | grep unity` ]]; then
  nvr --servername unity --remote-silent $@
else
  $TERMINAL -- nvr --servername unity --remote-silent $@
fi

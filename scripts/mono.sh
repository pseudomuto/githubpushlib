#!/bin/bash
git clone https://github.com/mono/mono.git
cd mono
./autogen.sh --prefix=/usr/local
make && make install
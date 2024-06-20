#!/bin/bash
echo "password" | openconnect -bq --user=user --passwd-on-stdin --no-dtls https://vdi.fhict.nl
cd /app
dotnet TALPA.dll

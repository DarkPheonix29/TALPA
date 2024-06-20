#!/bin/bash
echo "Olm4mxExne" | openconnect -bq --user=i532486 --passwd-on-stdin --no-dtls https://vdi.fhict.nl
cd /app
dotnet TALPA.dll

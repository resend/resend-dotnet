#!/bin/bash
# ------------------------------------------------------------------------
set -eux

yell() { echo "$0: $*" >&2; }
die() { yell "$*"; exit 111; }
try() { "$@" || die "cannot $*"; }


#
# Run all commands from the repository root!
# (That's the directory above the current one :)
# ------------------------------------------------------------------------
#
SCRIPT_PATH="${BASH_SOURCE[0]}"
if ([ -h "${SCRIPT_PATH}" ]); then
  while([ -h "${SCRIPT_PATH}" ]); do cd "$(dirname "$SCRIPT_PATH")";
  SCRIPT_PATH=$(readlink "${SCRIPT_PATH}"); done
fi
cd "$(dirname ${SCRIPT_PATH})" > /dev/null
cd ..


#
# Build
# ------------------------------------------------------------------------
dotnet clean   -c Release
dotnet restore --packages .nuget
dotnet build   -c Release --no-restore
dotnet test    -c Release --no-restore --no-build --verbosity=normal


#
# Native AOT smoke test
# ------------------------------------------------------------------------
dotnet publish tests/Resend.AotTests/Resend.AotTests.csproj -c Release -r linux-x64
./tests/Resend.AotTests/bin/Release/net8.0/linux-x64/publish/Resend.AotTests

dotnet publish tests/Resend.AotTests/Resend.AotTests.csproj -c Release -r linux-x64 -p:RootLibraries=false -o artifacts/aot-unrooted
./artifacts/aot-unrooted/Resend.AotTests

# eof
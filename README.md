# Sample application for chunked file upload
[![Build Status](https://travis-ci.org/edsoncunha/chunked-file-upload-csharp.svg?branch=master)](https://travis-ci.org/edsoncunha/chunked-file-upload-csharp)
[![License Apache2](https://img.shields.io/hexpm/l/plug.svg)](http://www.apache.org/licenses/LICENSE-2.0)

An example to demonstrate chunked file uploading to a .NET Core WebApi 2 server.
The client uses [Resumable.js](https://github.com/23/resumable.js), which in turn uses HTML 5 file API to generate the chunks.

A Java version is also available [https://github.com/edsoncunha/chunked-file-upload-sample](https://github.com/edsoncunha/chunked-file-upload-sample). 

## How to run

Install requirements:

[.Net Core](https://www.microsoft.com/net/download)

Run:

    cd ChunkedUploadWebApi
    dotnet run

Then browse to [localhost:5000](http://localhost:5000).
API Documentation will be available on [localhost:5000/api-docs](http://localhost:5000/api-docs).


### Testing

Generate a large file with random data and compare its checksum with downloaded file's checksum.
#### Windows:
Create a 1GB dummy file

	fsutil file createnew file.tmp 1073741824


Upload generated file.tmp, download it from server and check whether the checksums match with [File Checksum Integrity Verifier Utility](https://www.microsoft.com/en-us/download/details.aspx?id=11533):

	fciv -add file.tmp -sha1

#### Linux:
Create a 1GB file from /dev/urandom

    dd if=/dev/urandom of=file.tmp bs=1M count=1024 #creates a 1GB file



Upload generated files, download it from server and check whether the checksums match with ``sha1sum``

    sha1sum file.tmp


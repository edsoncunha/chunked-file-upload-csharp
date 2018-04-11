using ChunkedUploadWebApi.Controllers;
using ChunkedUploadWebApi.Model;
using Xunit;
using System;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace ChunkedUploadWebApi.Tests
{
    public class FileApiTest
    {
        private const int STATUS_OK = 200;
        private const int STATUS_CREATED = 201;
        private const int CHUNK_SIZE = 10;
        private const long USER_ID = 1L;
        private const int NUMBER_OF_CHUNKS = 5;
        private const int FILE_SIZE = ((NUMBER_OF_CHUNKS - 1) * CHUNK_SIZE) + (CHUNK_SIZE / 2);

        byte[] originalData;

        private FileController api;

        [Fact]
        public void TestChunkedUpload()
        {

            api = new FileController();
            originalData = generateRandomArray(FILE_SIZE);
            string fileName = Guid.NewGuid().ToString();

            var parameters = new CreateSessionParams
            {
                FileName = fileName,
                TotalSize = FILE_SIZE,
                ChunkSize = CHUNK_SIZE
            };

            SessionCreationStatusResponse session = createSession(parameters, api);

            for (int i = 0; i < NUMBER_OF_CHUNKS; i++)
            {
                uploadChunk(session.SessionId, i);
            }

            byte[] downloadedData = downloadFile(session.SessionId);

            Assert.Equal(SHAsum(originalData), SHAsum(downloadedData));
        }

        private byte[] downloadFile(string sessionId)
        {
            MemoryStream ms = new MemoryStream();
            api.SetOuputStream(ms);
            api.SetTargetResponse(CreateMockedResponse());
            api.DownloadFile(sessionId);

            byte[] downloadedContent = ms.ToArray();

            return downloadedContent;
        }

        private HttpResponse CreateMockedResponse()
        {
            var mock = Substitute.For<HttpResponse>();
            var headers = Substitute.For<IHeaderDictionary>();
            mock.Headers.Returns(headers);
            return mock;
        }

        private void uploadChunk(string sessionId, int chunkIndex)
        {
            int start = chunkIndex * CHUNK_SIZE;
            int end = (chunkIndex == NUMBER_OF_CHUNKS - 1) ? start + (FILE_SIZE % CHUNK_SIZE) : start + CHUNK_SIZE;
            byte[] chunkContent = originalData.Skip(start).Take(end - start).ToArray();
            api.UploadFileChunk(USER_ID, sessionId, (chunkIndex + 1), CreateInputFile(chunkContent));
        }

        private SessionCreationStatusResponse createSession(CreateSessionParams parameters, FileController api)
        {
            return api.StartSession(USER_ID, parameters);
        }

        private static byte[] generateRandomArray(int size)
        {
            byte[] arr = new byte[size];

            var rnd = new Random(10);

            while (--size >= 0)
            {
                arr[size] = (byte)(65 + ((rnd.NextDouble() * 100) % 26));
            }

            return arr;
        }

        private IFormFile CreateInputFile(byte[] content)
        {
            IFormFile form = Substitute.For<IFormFile>();

            form.Length.Returns(content.Length);
            form.OpenReadStream().Returns(new MemoryStream(content));

            return form;
        }

        private static string SHAsum(byte[] array)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();

            return Convert.ToBase64String(sha1.ComputeHash(array));
        }

    }


}

using System;
using System.Collections.Generic;
using Xunit;
using NSubstitute;
using ChunkedUploadWebApi.Data;


namespace ChunkedUploadWebApi.Tests
{
    public class SessionTest
    {
        public SessionTest()
        {
        }

        [Fact]
        public void sessionExpiration_noTimeout()
        {
            long user = 1L;
            long timeout = 100000L;
            FileInformation fileInfo = Substitute.For<FileInformation>(null, null, 0);

            Session session = new Session(user, fileInfo, timeout);

            Assert.False(session.IsExpired);
        }

        [Fact]
        public void sessionExpiration_timeout()
        {
            long user = 1L;
            long timeout = 0L;
            FileInformation fileInfo = Substitute.For<FileInformation>(null, null, 0);

            Session session = new Session(user, fileInfo, timeout);

            Assert.True(session.IsExpired);
        }

        [Fact]
        public void conclusion_withTotalNumberOfChunksDownloaded_shouldReturnConcluded()
        {
            long user = 1L;
            long timeout = 10000L;
            FileInformation fileInfo = Substitute.For<FileInformation>(null, null, 0);
            fileInfo.AlreadyPersistedChunks.Returns(new HashSet<int>(new int[] { 1 }));
            fileInfo.TotalNumberOfChunks.Returns(1);

            Session session = new Session(user, fileInfo, timeout);

            Assert.True(session.IsConcluded);
        }

        [Fact]
        public void conclusion_withPartialNumberOfChunksDownloaded_shouldReturnConcluded()
        {
            long user = 1L;
            long timeout = 10000L;
            FileInformation fileInfo = Substitute.For<FileInformation>(null, null, 0);
            fileInfo.AlreadyPersistedChunks.Returns(new HashSet<int>());
            fileInfo.TotalNumberOfChunks.Returns(1);

            Session session = new Session(user, fileInfo, timeout);

            Assert.False(session.IsConcluded);
        }
    }
}


using System;

namespace ChunkedUploadWebApi.Data
{
    public class Session
    {
        public string Id { get; private set; }

        public long User { get; private set; }

        public DateTime CreatedDate { get; private set; }
        private bool failed = false;

        public DateTime LastUpdate { get; private set; }

        public long Timeout { get; private set; }

        private static long DEFAULT_TIMEOUT = 3600L;

        public Session(long user, FileInformation fileInfo) : this(user, fileInfo, DEFAULT_TIMEOUT)
        {

        }

        public Session(long user, FileInformation fileInformation, long timeout)
        {
            this.Id = System.Guid.NewGuid().ToString();
            this.CreatedDate = DateTime.Now;
            this.LastUpdate = this.CreatedDate;
            this.User = user;
            this.FileInfo = fileInformation;
            this.Timeout = timeout;
        }


        public double Progress
        {
            get
            {
                if (FileInfo.TotalNumberOfChunks == 0)
                    return 0;

                return SuccessfulChunks / (FileInfo.TotalNumberOfChunks * 1f);
            }
        }

        public String Status
        {
            get
            {
                if (failed)
                    return "failed";
                else if (IsConcluded)
                    return "done";

                return "ongoing";
            }
        }

        public bool IsConcluded
        {
            get
            {
                return FileInfo.TotalNumberOfChunks == FileInfo.AlreadyPersistedChunks.Count;
            }
        }


        public int SuccessfulChunks
        {
            get
            {
                return FileInfo.AlreadyPersistedChunks.Count;
            }
        }

        public bool HasFailed()
        {
            return failed;
        }

        public bool IsExpired
        {
            get
            {
                TimeSpan span = DateTime.Now - LastUpdate;
                return span.TotalSeconds >= Timeout;
            }
        }

        public void MaskAsFailed()
        {
            failed = true;
        }

        public FileInformation FileInfo
        {
            get; private set;
        }

        public void RenewTimeout()
        {
            LastUpdate = DateTime.Now;
        }
    }
}
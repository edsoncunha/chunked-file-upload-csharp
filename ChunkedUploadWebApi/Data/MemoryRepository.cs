using System.Collections.Generic;
using System;

namespace ChunkedUploadWebApi.Data
{
    public class MemoryRepository : FileRepository
    {
        private Dictionary<string, Dictionary<int, byte[]>> internalStorage;

        public MemoryRepository()
        {
            internalStorage = new Dictionary<string, Dictionary<int, byte[]>> ();
        }

        public override void Persist(string id, int chunkNumber, byte[] buffer)
        {
		    if (!internalStorage.ContainsKey(id)) {
			    internalStorage.Add(id, new Dictionary<int, byte[]>());
            }

            Dictionary<int, byte[]> blocks = internalStorage[id];
		    blocks.Add(chunkNumber, buffer);
        }

        public override byte[] Read(string id, int chunkNumber)
        {
            if (!internalStorage.ContainsKey(id)) {
                throw new System.Exception("Session not found on internalStorage");
            }

            return internalStorage[id][chunkNumber];
        }
    }
}
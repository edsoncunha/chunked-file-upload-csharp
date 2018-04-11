using System.IO;

namespace ChunkedUploadWebApi.Data
{
    public class LocalFileSystemRepository : FileRepository
    {
        string ROOT = "./files_store";
        public async override void Persist(string id, int chunkNumber, byte[] buffer)
        {
            string chunkDestinationPath = Path.Combine(ROOT, id);

            if (!Directory.Exists(chunkDestinationPath))
            {
                Directory.CreateDirectory(chunkDestinationPath);
            }

            string path = Path.Combine(ROOT, id, chunkNumber.ToString());
            await File.WriteAllBytesAsync(path, buffer);
        }

        public override byte[] Read(string id, int chunkNumber)
        {
            string targetPath = Path.Combine(ROOT, id, chunkNumber.ToString());
            return File.ReadAllBytes(targetPath);
        }

    }
}
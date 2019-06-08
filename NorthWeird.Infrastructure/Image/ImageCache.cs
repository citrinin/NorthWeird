using System;
using System.IO;
using System.Linq;

namespace NorthWeird.Infrastructure.Image
{
    public class ImageCache
    {
        private readonly string _cachePath;
        private readonly TimeSpan _imageLifeTime;
        private readonly int _cacheSize;
        private readonly object _lockObject = new object();

        public ImageCache(string cachePath, int cacheSize, TimeSpan imageLifeTime)
        {
            _cachePath = cachePath;
            _cacheSize = cacheSize;
            _imageLifeTime = imageLifeTime;
        }

        public bool IsImageInCache(string imagePath)
        {
            var fileName = GetPathToTheImageByUrlPath(imagePath);

            if (!File.Exists(fileName))
            {
                return false;
            }

            if (!IsImageExpired(fileName))
            {
                return true;
            }

            File.Delete(fileName);
            return false;
        }

        public byte[] GetImageFromCache(string imagePath)
        {
            var fileName = GetPathToTheImageByUrlPath(imagePath);
            byte[] buffer;
            lock (_lockObject)
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    var br = new BinaryReader(fs);
                    buffer = br.ReadBytes((int)fs.Length);
                }
            }

            return buffer;
        }

        public void PutImageInCache(Stream imageStream, string imagePath)
        {
            var fileName = GetPathToTheImageByUrlPath(imagePath);
            lock (_lockObject)
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    imageStream.Position = 0;
                    imageStream.CopyTo(file, (int)imageStream.Length);
                }

                RemoveOldFileIfMaxCacheSizeIsReached();
            }
        }

        public bool IsContentTypeImage(string contentType)
        {
            return string.Equals(contentType, "image/jpeg", StringComparison.OrdinalIgnoreCase);
        }

        private string GetPathToTheImageByUrlPath(string imagePath)
        {
            return Path.Combine(_cachePath, $"{imagePath.Replace("/", "")}.jpeg");
        }

        private bool IsImageExpired(string fileName)
        {
            return DateTime.Now - File.GetCreationTime(fileName) > _imageLifeTime;
        }

        private void RemoveOldFileIfMaxCacheSizeIsReached()
        {
            var filesInDirectory = Directory.EnumerateFiles(_cachePath).OrderBy(File.GetCreationTime).ToList();
            if (filesInDirectory.Count() <= _cacheSize) return;
            while (filesInDirectory.Count() > _cacheSize)
            {
                var fileToDelete = filesInDirectory.FirstOrDefault();
                filesInDirectory.Remove(fileToDelete);
                File.Delete(fileToDelete);
            }
        }
    }
}

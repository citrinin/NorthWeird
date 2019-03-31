using System;
using System.Collections.Generic;
using System.Text;

namespace NorthWeird.Infrastructure.Image
{
    public static class ImageHelper
    {
        private const int GarbageBytesInImage = 78;

        public static byte[] FixBrokenImage(byte[] bytes)
        {
            var img = new byte[bytes.Length - GarbageBytesInImage];
            Array.Copy(bytes, GarbageBytesInImage, img, 0, bytes.Length - GarbageBytesInImage);
            return img;
        }

        public static bool IsValidImage(byte[] bytes)
        {
            try
            {
                var img = SixLabors.ImageSharp.Image.Load(bytes);
            }
            catch (NotSupportedException)
            {
                return false;
            }
            return true;
        }
    }
}

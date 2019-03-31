using System;

namespace NorthWeird.WebUI.Middleware
{
    public class ImageCachingMiddlewareOptions
    {
        public string ContentFolder { get; set; }

        public int MaxCount { get; set; }

        public TimeSpan ExpirationTime { get; set; }
    }
}

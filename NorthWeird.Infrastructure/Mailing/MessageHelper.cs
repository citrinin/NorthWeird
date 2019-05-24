using System;
using System.Collections.Generic;
using System.Text;

namespace NorthWeird.Infrastructure.Mailing
{
    public static class MessageHelper
    {
        public static string GetEmailConfirmationMessage(string url)
        {
            return "<h1>You are welcomed at our lovely website NorthWeird!</h1> <br /> " +
                   $"Click <a href=\"{url}\">here</a> to confirm your email";
        }

        public static string GetEmailResetPassMessage(string url)
        {
            return "<h1>You forgot your password from NorthWeird web site.</h1> " +
                   $"<br /> Click <a href=\"{url}\">here</a> to restore you pass";
        }

        public static string GetEmailDeniedResetPassMessage()
        {
            return "<h1>You forgot your password from NorthWeird web site.</h1> " +
                   "<br /> But you never even have an account on our lovely site :3 <br />" +
                   "Bye!";
        }
    }
}

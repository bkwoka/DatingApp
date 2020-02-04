using Microsoft.AspNetCore.Http;

namespace DatingApp.Helpers
{
    public static class Extentions
    {
        /// <summary>
        /// The content of this method is repeat in Startup class. But can be called from here
        /// </summary>
        /// <param name="response"></param>
        /// <param name="message"></param>
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
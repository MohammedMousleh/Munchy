using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Munchy.Models
{
    public class PSPClient
    {

        private static  HttpClient instance = null;

        private PSPClient()
        { 
        
        }

      
        public static HttpClient Instance()
        {
            if(instance == null)
            {
                instance = new HttpClient();
                instance.DefaultRequestHeaders.Add("Authorization", "Basic OmZmNWFkOGQ0ZTg5NTUzNjBjY2NjZTE3NDExNGQyY2ViMDYwNTU0MzlhNzYwNTZjZTExOTFiZmVkNWFiMTcyZTU=");
                instance.DefaultRequestHeaders.Add("Accept-Version", "v10");
                return instance;
            } else
            {
                return instance;
            }
        }
   
      
}
}

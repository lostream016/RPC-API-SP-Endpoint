using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace RPC_API_SP_EndPoint.Helper
{
    public class InformationExtractor
    {
        public static string request(HttpRequestMessage request, out NameValueCollection param)
        {
            try
            {
                String ext = request.RequestUri.Segments[request.RequestUri.Segments.Count() - 1];
                HttpResponseMessage http_response = new HttpResponseMessage();
                var url_link = request.RequestUri.ToString();
                var query = request.RequestUri.Query;
                param = new NameValueCollection();
                if (query != "")
                {
                    string querystring = url_link.Substring(url_link.IndexOf('?'));
                    param = HttpUtility.ParseQueryString(querystring);
                }

                return ext;
            }
            catch (Exception ex)
            {
                throw ex;
            }        
                
        }
    }
}
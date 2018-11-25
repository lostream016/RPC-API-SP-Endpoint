using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using RPC_API_SP_EndPoint.Helper;
using System.Text;

namespace RPC_API_SP_EndPoint.Controllers
{
    public class SPController : ApiController
    {
        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                NameValueCollection param;
                string ext = InformationExtractor.request(Request, out param);
                string query_result;
                var result = SPExecutor.ExecuteSp(ext, param, out query_result);

                response.Content = new StringContent(query_result, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something Went Wrong");
                return response;
            }            
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                NameValueCollection param;
                string ext = InformationExtractor.request(Request, out param);
                string query_result;

                string dataBody = await Request.Content.ReadAsStringAsync();                
                param.Add("stringJson", dataBody);

                var result = SPExecutor.ExecuteSp(ext, param, out query_result); //sp execution

                if(query_result != null)
                {
                    response.Content = new StringContent(query_result, Encoding.UTF8, "application/json");
                }                
                return response;
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something Went Wrong");
                return response;
            }
        }
    }
}

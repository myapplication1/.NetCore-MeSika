using System.Runtime.Serialization;

namespace MeSika.Web
{
    public class RestClient<T> where T : class
    {
        HttpRequestMessage request = new HttpRequestMessage();

        public string Baseurl { get; private set; } = "https://app-api-pjs.herokuapp.com/api/";

        public async Task<T> Get()
        {
            try
            {
                using (var client = new HttpClient())
                {

                    request.RequestUri = new Uri(Baseurl);
                    request.Method = HttpMethod.Get;
                    //request.Headers.Add("SecureApiKey", "12345");
                    HttpResponseMessage response = await client.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var statusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        
                        //API call success, Do your action
                    }

                    else
                    {
                        //API Call Failed, Check Error Details
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                ///Message = "Done";
                throw;
            }
        }

        public async Task<List<T>> GetList()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    
                    request.RequestUri = new Uri(Baseurl);
                    request.Method = HttpMethod.Get;
                    //request.Headers.Add("SecureApiKey", "12345");
                    HttpResponseMessage response = await client.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var statusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        //API call success, Do your action
                    }

                    else
                    {
                        //API Call Failed, Check Error Details
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                ///Message = "Done";
                throw;
            }
        }


    }

}

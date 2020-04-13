using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    //it will have defenitions of all interfaces defined in IHttpClient
    public class CustomHttpClient : IHttpClient
    {
       private readonly HttpClient _client;   //this HttpClient will make actual call to Api. It comes from System.Net.Http 
        public CustomHttpClient()
        {
            _client = new HttpClient(); // instance of Http Client is created
        }

        public  async Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            //request message
            var requestmessage = new HttpRequestMessage(HttpMethod.Get, uri); //it is a get request. HttpRequestMessage is coming from System.Net.Http
            var response= await  _client.SendAsync(requestmessage) ; //now http will send request msg and wait for response since it is async
            //this tresponse is from microservice which is at mentioned uri
            return await response.Content.ReadAsStringAsync();// return the string as content to read
        }

        public Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }
    }
}

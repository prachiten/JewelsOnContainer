using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
   public  interface IHttpClient// everything we write in interface is public 
    {
        // client asking server to get data
        //if get request is made always JASON is brought back so return is string. Task is a thread
        Task<string> GetStringAsync(string uri,          //uri is url where microservice is
            string authorizationToken = null,           //needed for authentication
            string authorizationMethod = "Bearer");     //

        // client asking server to create new data. when someone wants to add their own catalog they will send their own data so it is postdata
        //Post is made generic because dont know what type of data user will post. After posting some response is returned whetehr succesful or not
        Task<HttpResponseMessage> PostAsync<T>(string uri,
            T item,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        //client ask server to make changes to data
        Task<HttpResponseMessage> PutAsync<T>(string uri,
            T item,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> DeleteAsync(string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

    }
}

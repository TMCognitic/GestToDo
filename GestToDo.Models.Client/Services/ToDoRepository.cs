using GestToDo.Models.Interfaces;
using GestToDo.Models.Client.Data;
using G = GestToDo.Model.Global;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;
using GestToDo.Models.Client.Mappers;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.Security.Authentication;

namespace GestToDo.Models.Client.Services
{
    public class ToDoRepository : IRepository<int, ToDo>
    {
        HttpClient _httpClient;

        public ToDoRepository(string url)
        {
            var handler = new HttpClientHandler
            {
                SslProtocols = SslProtocols.Default
            };

            handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
            {
                return true;
            };

            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }        

        public IEnumerable<ToDo> Get()
        {
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("todo").Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<G.ToDo[]>(json).Select(td => td.ToClient());
        }

        public ToDo Get(int key)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("todo/"+key).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<G.ToDo>(json)?.ToClient();
        }

        public ToDo Insert(ToDo entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity.ToGlobal()));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PostAsync("todo", content).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<G.ToDo>(json).ToClient();
        }

        public bool Update(int key, ToDo entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity.ToGlobal()));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PutAsync("todo/"+key, content).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }

        public bool Delete(int key)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.DeleteAsync("todo/" + key).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}

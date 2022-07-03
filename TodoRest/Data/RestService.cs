using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TodoRest.Model;
using Xamarin.Forms;


namespace TodoRest.Data
{
    public class RestService : IRestService
    {
        HttpClient client;

        public List<TodItem> Items { get; private set; }

        public RestService()
        {
#if DEBUG
            client = new HttpClient();
            DependencyService.Get<IHttpClientHandlerService>()
                .GetInsecureHandler();
#else
            client = new HttpClient();
#endif
        }
        public async Task<List<TodItem>> RefreshDataAsync()
        {
            Items = new List<TodItem>();
            string action = "Get";

            var uri=new  Uri(string.Format(Constants.RestUrl, action));
            try
            {
                var response = await client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var content =await response.Content.ReadAsStringAsync();
                    //Request es un JSON ,toca deserializar
                    Items= JsonConvert.DeserializeObject<List<TodItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}",ex.Message);
            }
            return Items;
            
        }
        public async Task SaveTodoItemAsync(TodItem item,bool isNewItem = false)
        {
            try
            {
                //serializando objeto Response
                var json =JsonConvert.SerializeObject(item);
                var content =new StringContent(json,Encoding.UTF8,"application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri =new Uri(string.Format(Constants.RestUrl,"Create")); 
                    response= await client.PostAsync(uri,content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.RestUrl, "Edit"));
                    response= await client.PutAsync(uri,content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\TodoItem successfully saved.");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task DeleteTodoItemAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.RestUrl, id));
            try
            {
                var response =await client.DeleteAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\TodoItem successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}",ex.Message);
            }
        }

    }
}

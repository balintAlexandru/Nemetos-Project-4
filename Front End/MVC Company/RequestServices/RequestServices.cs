using Microsoft.AspNetCore.Http;
using MVC_Company.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace MVC_Company.RequestServices
{
    public interface IRequestServices {
    void CreateEmployee(IFormFile Image, Employee employee);
        void UpdateEmployee(IFormFile Image, Employee employee, int id);
        void UpdateSocialMedia(int id, SocialMedia social);
    }
    public class RequestService : UrlConstant , IRequestServices 
    {
        public static async void GetEntity<T>()
        {
            List<T> EmpInfo = new List<T>();
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync(BaseURL);
            if (Res.IsSuccessStatusCode)
            {
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                EmpInfo = JsonConvert.DeserializeObject<List<T>>(EmpResponse);
            }

        }

        //example: JsonService.sendJson<Employee>("https://localhost:44398/api/Employees");
        public  async void CreateEmployee(IFormFile Image, Employee employee)
        {

            using (var stream = new MemoryStream())
            {
                await Image.CopyToAsync(stream);
                employee.Image = stream.ToArray();
            }


            using (var client = new HttpClient())
            {
                //client.BaseAddress = "https://localhost:44399/";                  //configuration["EndpointAPIStrings:EndpointUrl"];
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient

                var json = JsonConvert.SerializeObject(employee);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(BaseURL + "/createEmploy", httpContent);

            }


        }

        public async void UpdateEmployee(IFormFile Image, Employee employee, int id)
        {
            employee.IdEmployee = id;
            using (var stream = new MemoryStream())
            {
                await Image.CopyToAsync(stream);
                employee.Image = stream.ToArray();
            }
            using (var client = new HttpClient())
            {
                //client.BaseAddress = "https://localhost:44399/";                  //configuration["EndpointAPIStrings:EndpointUrl"];
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient

                var json = JsonConvert.SerializeObject(employee);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(BaseURL + "/UpdateEmployee", httpContent);

            }
        }

        public async void UpdateSocialMedia(int id, SocialMedia social)
        {
            social.IdSocialMedia = id;
            social.EmployeesRef = id;
            using (var client = new HttpClient())
            {
                
                //client.BaseAddress = "https://localhost:44399/";                  //configuration["EndpointAPIStrings:EndpointUrl"];
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient

                var json = JsonConvert.SerializeObject(social);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(BaseURL + "/UpdateSocial", httpContent);

            }
        }
    }
}

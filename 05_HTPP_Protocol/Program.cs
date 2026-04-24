using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace _05_HTPP_Protocol
{
    class Post
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
    }
    internal class Program
    {
        static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  
        static async Task Main(string[] args)
        {
            //GET
            #region GET           
            // string url = "https://jsonplaceholder.typicode.com/users";
            // HttpClient httpClient = new HttpClient();   

            //var responce =  await httpClient.GetAsync(url);

            // Console.WriteLine($"Status : {responce.StatusCode}");
            // var result = await responce.Content.ReadAsStringAsync();
            // Console.WriteLine(result);
            #endregion
            #region POST


            //POST
            //Post post = new Post() { 
            //    Title = "About SIMI",
            //    Body = "I like simi. Cool store",
            //    UserId = 523
            //}; 

            //string json = JsonConvert.SerializeObject(post);

            //var data = new StringContent(json , Encoding.UTF8, "application/json");
            //HttpClient httpClient = new HttpClient();
            //var url = "https://jsonplaceholder.typicode.com/posts";

            //var responce =await httpClient.PostAsync(url, data);
            //Console.WriteLine($"Status : {responce.StatusCode}");

            //var res = await responce.Content.ReadAsStringAsync();
            //Console.WriteLine(res);
            #endregion
            #region Downloading
            //var url = @"https://cdn.pixabay.com/photo/2015/04/19/08/32/flower-729510_1280.jpg";
            //HttpClient httpClient = new HttpClient();   
            //HttpRequestMessage request = new HttpRequestMessage()
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri(url)            
            //};

            //HttpResponseMessage message =  await httpClient.SendAsync(request);
            //Console.WriteLine($"Status code : {message.StatusCode}");

            //using (FileStream fs = new FileStream(desktop + "/image.jpg", FileMode.Create))
            //{
            //    await message.Content.CopyToAsync(fs);
            //}

            //DownloadFileAsync(@"https://ash-speed.hetzner.com/100MB.bin");
            DownloadFileAsync(@"https://static.vecteezy.com/system/resources/thumbnails/057/068/323/small/single-fresh-red-strawberry-on-table-green-background-food-fruit-sweet-macro-juicy-plant-image-photo.jpg");
            //DownloadFileAsync(@"https://ash-speed.hetzner.com/1GB.bin");

            Console.ReadKey();
            #endregion
        }

        private static async void DownloadFileAsync(string address)
        {
            WebClient webClient = new WebClient();

            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
          
            string filename = Path.GetFileName(address);
            string fullpath = Path.Combine(desktop, filename);
            await webClient.DownloadFileTaskAsync(address, fullpath);
            Console.WriteLine($"{filename} was loaded!");           
        }
        private static void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine($"Downloading .... {(float)e.BytesReceived/1024/1024} Mb from " +
                $"{(float)e.TotalBytesToReceive/1024/1024}Mb. " +
                $"Percentage : {e.ProgressPercentage} %");
        }

        private static void WebClient_DownloadFileCompleted(object? sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if ( e.Cancelled)
            {
                Console.WriteLine("File downloading canceled !");
            }
            else
            {
                Console.WriteLine("File downloaded !");
            }
        }
    }
}

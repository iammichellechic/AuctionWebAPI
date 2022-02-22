using AdvertisementAPIClient;
using System.Threading.Tasks;
namespace APIClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var client = new swaggerClient("https://localhost:44389/",httpClient);

            var ads= await client.GetAsync();

            foreach (var item in ads)
            {
                Console.WriteLine(item.Title);
            }
        }
    }
}

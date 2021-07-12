using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Tests.Integration.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> DeserializeAsAsync<T>(this HttpContent httpContent)
        {
            return JsonConvert.DeserializeObject<T>(await httpContent.ReadAsStringAsync());
        }
    }
}

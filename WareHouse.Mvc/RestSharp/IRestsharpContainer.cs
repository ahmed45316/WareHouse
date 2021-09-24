using RestSharp;
using System.Threading.Tasks;

namespace WareHouse.Mvc.RestSharp
{
    public interface IRestsharpContainer
    {
        Task<T> SendRequest<T>(string uri, Method method, object obj = null);
        Task SendRequest(string uri, Method method, object obj = null);
    }
}

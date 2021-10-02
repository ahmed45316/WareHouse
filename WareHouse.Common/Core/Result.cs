using WareHouse.Common.Enum;

namespace WareHouse.Common.Core
{
    public class Result<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public MessageType messageType  { get; set; } 
    }
    
}

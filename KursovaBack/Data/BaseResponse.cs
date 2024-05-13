using Microsoft.AspNetCore.Http;

namespace KursovaBack.Data
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }

        public T Data { get; set; }

        public string JwtToken { get; set; }

   
    }
    public interface IBaseResponse<T>
    {
        string Description { get; }
        StatusCode StatusCode { get; }
        T Data { get; }
        string JwtToken { get; }
    }
    public enum StatusCode
    {
        UserNotFound = 0,
        UserAlreadyExists = 1,
        OK = 200,
        InternalServerError = 500
    }
}

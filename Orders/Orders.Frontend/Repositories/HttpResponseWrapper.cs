using System.Net;

namespace Orders.Frontend.Repositories;

public class HttpResponseWrapper<T>(T? response, bool error, HttpResponseMessage httpResponseMessage)
{
    public T? Response { get; set; } = response;

    public bool Error { get; set; } = error;

    public HttpResponseMessage HttpResponseMessage { get; set; } = httpResponseMessage;


    public async Task<string> GetErrorMessage()
    {
        if (!Error)
        {
           return null!;
        }

        var statusCode = HttpResponseMessage.StatusCode;
        
        if (statusCode == HttpStatusCode.NotFound)
        {
            return "Recurso no encontrado";
        }

        if (statusCode == HttpStatusCode.BadRequest)
        {
            return await HttpResponseMessage.Content.ReadAsStringAsync();   
        }

        if (statusCode == HttpStatusCode.Unauthorized)
        {
            return "Tienes que estar logueado para ejecutar esta operación";
        }

        if (statusCode == HttpStatusCode.Forbidden)
        {
            return "No tienes permisos para hacer esta operación.";
        }

        return "Ha ocurrido un error inesperado. Inténtalo de nuevo más tarde.";

    }
}

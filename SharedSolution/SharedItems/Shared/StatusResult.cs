using System;
namespace SharedItems.Shared
{
    public class
        StatusResult
    {
        public StatusResult()
        {
        }


        public bool Success { get; set; }

        public string Message { get; set; }

        public int Code { get; set; }

        public string DetailedMessage { get; set; }

        public string Title { get; set; }

        public long Count { get; set; }


        public void SuccessSaveResult()
        {
            Title = "GUARDADO";
            Message = "DATOS GUARDADOS CORRECTAMENTE";
            Success = true;
            Code = 200;
        }
        public void Deleted()
        {
            Title = "ELIMINADO";
            Message = "DATOS ELIMINADOS CORRECTAMENTE";
            Success = true;
            Code = 200;
        }
        public void SuccessGetResult()
        {
            Title = "EXITOSO";
            Message = "DATOS OBTENIDOS CORRECTAMENTE";
            Success = true;
            Code = 200;
        }
        public void FailedResultServer(Exception ex)
        {
            Success = false;
            Code = 500;
            Title = "ERROR";
            Message = $"Ha ocurrido un error en el servidor";
            DetailedMessage = $"Detalles: {ex.InnerException?.StackTrace} {ex.InnerException?.Message} " +
                $" {ex.Message}";
        }
        public void FailedBadRequest(List<string> validationMessages)
        {
            validationMessages = validationMessages ?? new List<string>();
            validationMessages.Insert(0, "Ha ocurrido un error de validacion: ");
            Success = false;
            Code = 400;
            Title = "ERROR";
            Message = $"Por favor validar los siguientes datos";
            DetailedMessage = string.Join('\n', validationMessages);
        }
        public void Forbidden(List<string> validationMessages)
        {
            validationMessages = validationMessages ?? new List<string>();
            validationMessages.Insert(0, "No tiene acceso al recurso solicitado: ");
            Success = false;
            Code = 401;
            Title = "ERROR";
            Message = $"Por favor validar los siguientes datos";
            DetailedMessage = string.Join('\n', validationMessages);
        }
    }
}


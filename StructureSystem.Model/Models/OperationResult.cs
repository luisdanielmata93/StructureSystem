using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifications.Wpf;
using static StructureSystem.Model.Enums;

namespace StructureSystem.Model
{
    public class OperationResult
    {
        public string Message { get; private set; }
        public Object Data { get; private set; }
        public ActionType ActionType { get; set; }
        public NotificationType NotificationType { get; set; }

        public string ErrorInformation { get; set; }
        public bool Error { get; private set; }
        public Exception Exception { get; private set; }

        private const string MessageGet = "Información obtenida exitosamente.";
        private const string MessageUpdate = "Información actualizada exitosamente.";
        private const string MessageCreate = "Información creada exitosamente.";
        private const string MessageDelete = "El registro se eliminó exitosamente.";
        private const string MessageOther = "Operación realizada exitosamente.";

        public OperationResult()
        {
          
        }

        public void OperationSuccess(Object data, ActionType action)
        {
            this.Message = GetOperationMessage(action);
            this.NotificationType = NotificationType.Success;
            this.Data = data;
            this.ActionType = action;
            this.Error = false;
        }

     

        public void OperationError(string messageError,ActionType action, Exception Exception)
        {
            this.ErrorInformation = messageError;
            this.NotificationType = NotificationType.Error;
            this.ActionType = action;
            this.Error = true;
            this.Exception = Exception;
        }

        private string GetOperationMessage(ActionType action)
        {
            string result = string.Empty;
            switch (action)
            {
                case ActionType.Create:
                    result =  MessageCreate;
                    break;
                case ActionType.Update:
                    result = MessageUpdate;
                    break;
                case ActionType.Delete:
                    result = MessageDelete;
                    break;
                case ActionType.Get:
                    result = MessageGet;
                    break;
                case ActionType.NonAction:
                    result = MessageOther;
                    break;
                default:
                    break;
            }

            return result;
        }

    }//end of class
}//end of class

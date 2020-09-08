using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StructureSystem.Model.Enum;

namespace StructureSystem.Model
{
    public class OperationResult
    {
        public string Code { get; private set; }
        public string Message { get; private set; }
        public Object Data { get; private set; }
        public bool Error { get; private set; }
        public Exception Exception { get; private set; }

        public OperationResult(string Code, string Message, bool Error)
        {
            this.Code = Code;
            this.Message = Message;
            this.Error = Error;
        }

        public OperationResult(string Code, string Message, bool Error, Object Data)
        {
            this.Code = Code;
            this.Message = Message;
            this.Error = Error;
            this.Data = Data;
        }

        public OperationResult(string Code, string Message, bool Error, Exception Exception)
        {
            this.Code = Code;
            this.Message = Message;
            this.Error = Error;
            this.Exception = Exception;
        }

    }//end of class
}//end of class

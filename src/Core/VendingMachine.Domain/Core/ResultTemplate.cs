using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Domain.Core
{
    public class ResultMessageLine
    {
        
        public string Message { get; set; }
        public object[] Args { get; set; }
        public bool SkipLocalize { get; set; }
        public ResultMessageLine()
        {
            Message = "";
            Args = Array.Empty<object>();
        }
        public ResultMessageLine(string message, params object[] args)
        {
            Message = message;
            Args = args;

        }

    }
    public class ResultTemplate : IResultTemplate
    {
        public bool Succeeded { get; init; }
        public string Message { get; set; }
        public object[] MessageArgs { get; set; }
        protected List<ResultMessageLine> MessageLines { get; set; }

        public ResultTemplate()
        {
            MessageArgs = Array.Empty<object>();
            MessageLines = new List<ResultMessageLine>();
            Succeeded = true;
        }
        public static ResultTemplate SucceededResult()
        {
            return SucceededResult(null);
        }
        public static ResultTemplate SucceededResult(string message)
        {
            return SucceededResult(message, Array.Empty<object>());
        }

        public static ResultTemplate SucceededResult(string message, params object[] args)
        {
            return new ResultTemplate
            {
                Message = message,
                MessageArgs = args,
                Succeeded = true,
            };
        }

        public static ResultTemplate FailedResult()
        {
            return FailedResult(null);
        }
        public static ResultTemplate FailedResult(string message)
        {
            return FailedResult(message, Array.Empty<object>());
        }

        public static ResultTemplate FailedResult(string message, params object[] args)
        {
            return new ResultTemplate
            {
                Message = message,
                MessageArgs = args,
                Succeeded = false
            };
        }

        public IReadOnlyList<ResultMessageLine> GetMessageLines()
        {
            return MessageLines;
        }

        public virtual IResultTemplate AppendMessageLine(ResultMessageLine messageLine)
        {
            MessageLines.Add(messageLine);
            return this;
        }
    }

    public class ResultTemplate<T> : ResultTemplate, IResultTemplate<T>
    {
        public ResultTemplate()
        {

        }
        public ResultTemplate(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public override IResultTemplate<T> AppendMessageLine(ResultMessageLine messageLine)
        {
            base.AppendMessageLine(messageLine);
            return this;
        }

        public IResultTemplate<T> WithData(T data)
        {
            Data = data;
            return this;
        }

        public static new ResultTemplate<T> SucceededResult()
        {
            return SucceededResult(null);
        }
        public static new  ResultTemplate<T> SucceededResult(string message)
        {
            return SucceededResult(message, Array.Empty<object>());
        }

        public static new ResultTemplate<T> SucceededResult(string message, params object[] args)
        {
            return SucceededResult(data: default, message, args);
        }

        public static ResultTemplate<T> SucceededResult(T data, string message = null, params object[] args)
        {
            return new ResultTemplate<T>
            {
                Message = message,
                MessageArgs = args,
                Succeeded = true,
                Data = data
            };
        }

        public static new ResultTemplate<T> FailedResult()
        {
            return FailedResult(null);
        }
        public static new ResultTemplate<T> FailedResult(string message)
        {
            return FailedResult(message, Array.Empty<object>());
        }

        public static new ResultTemplate<T> FailedResult(string message, params object[] args)
        {
            return FailedResult(data: default, message: message, args);
        }
        public static  ResultTemplate<T> FailedResult(T data, string message, params object[] args)
        {
            return new ResultTemplate<T>
            {
                Message = message,
                MessageArgs = args,
                Succeeded = false,
                Data = data
            };
        }
    }
}

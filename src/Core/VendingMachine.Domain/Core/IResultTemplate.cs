using System;
using System.Collections;
using System.Collections.Generic;

namespace VendingMachine.Domain.Core
{
    public interface IResultTemplate
    {
        public bool Succeeded { get; init; }
        public string Message { get; set; }
        public object[] MessageArgs { get; set; }
        IReadOnlyList<ResultMessageLine> GetMessageLines();
        IResultTemplate AppendMessageLine(ResultMessageLine messageLine);
    }
}

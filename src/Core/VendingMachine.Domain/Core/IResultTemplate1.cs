namespace VendingMachine.Domain.Core
{
    public interface IResultTemplate<T> : IResultTemplate
    {
        public T Data { get; set; }
        IResultTemplate<T> AppendMessageLine(ResultMessageLine messageLine);
        public IResultTemplate<T> WithData(T data);
    }
}

using System;

namespace VendingMachine.ConsoleApp.Application
{
    public interface IConsoleApp : IDisposable
    {
        public void Run(string[] args);
    }
}

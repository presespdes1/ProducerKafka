using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endpoint.src
{
    public interface IConsoleService
    {
        void Write(string value);
        void WriteLine(string value);
        ConsoleKeyInfo ReadKey();
        string? ReadLine();
        void Clear();
    }
}

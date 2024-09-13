using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endpoint.src
{
    public interface IRequestService
    {
        public Task<string> GetRequest(string option);
    }
}

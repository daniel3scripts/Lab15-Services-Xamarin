using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace TodoRest
{
    public interface IHttpClientHandlerService
    {
        HttpClientHandler GetInsecureHandler();
    }
}

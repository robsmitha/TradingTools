using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeStockTrends.Job.Services
{
    public interface IIEXClient
    {
        Task<T> SendAsync<T>(string function, string @params = null);
    }
}

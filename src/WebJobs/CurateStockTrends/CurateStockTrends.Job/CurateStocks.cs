using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurateStockTrends.Job
{
    public class CurateStocks
    {

        public async Task Run(string[] stocks, string range = "5d")
        {
            //observe successive downtrending days sorting by maxium percent lost

            //observe successive uptrending days sorting by maxium percent gained

            await Task.FromResult(0);
        }
    }
}

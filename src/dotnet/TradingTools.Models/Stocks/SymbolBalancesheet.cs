using System;
using System.Collections.Generic;
using System.Text;

namespace TradingTools.Models.Stocks
{
    public class SymbolBalancesheet
    {
        public string Symbol { get; set; }
        public List<BalancesheetQuarter> Balancesheet { get; set; }
    }

    public class BalancesheetQuarter
    {
        public DateTimeOffset ReportDate { get; set; }
        public DateTimeOffset FiscalDate { get; set; }
        public string Currency { get; set; }
        public decimal? CurrentCash { get; set; }

        /// <summary>
        /// marketable securities or temporary investments, are those which can easily be converted to cash, typically within 5 years. 
        /// Many short-term investments are sold or converted to cash after a period of only 3-12 months.
        /// </summary>
        public decimal? ShortTermInvestments { get; set; }
        
        /// Accounts receivables consist of the short-term obligations owed to the company by its clients. 
        public decimal? Receivables { get; set; }
        
        /// <summary>
        /// inventory represents the company's raw materials, work-in-progress goods and finished goods.
        /// </summary>
        public decimal? Inventory { get; set; }
        
        /// <summary>
        /// Current assets have a lifespan of one year or less, meaning they can be converted easily into cash. 
        /// Such asset classes include cash and cash equivalents, accounts receivable and inventory. 
        /// Cash, the most fundamental of current assets, also includes non-restricted bank accounts and checks. 
        /// Cash equivalents are very safe assets that can be readily converted into cash; U.S. Treasuries are one such example. 
        /// </summary>
        public decimal? CurrentAssets { get; set; }
        public decimal? OtherCurrentAssets { get; set; }
        /// <summary>
        /// goodwill, patents or copyright
        /// </summary>
        public decimal? IntangibleAssets { get; set; }

        /// <summary>
        /// account on the asset side of a company's balance sheet that represents the company's investments, including stocks, bonds, real estate, and cash
        /// </summary>
        public decimal? LongTermInvestments { get; set; }

        /// <summary>
        /// long-term assets vital to business operations and not easily converted into cash
        /// </summary>
        public decimal? PropertyPlantEquipment { get; set; }

        /// <summary>
        /// intangible asset that is associated with the purchase of one company by another
        /// </summary>
        public decimal? Goodwill { get; set; }
        public decimal? OtherAssets { get; set; }
        public decimal? TotalAssets { get; set; }

        /// <summary>
        /// Accounts payable (AP) is an account within the general ledger that represents a company's obligation to pay off a short-term debt to its creditors or suppliers.
        /// </summary>
        public decimal? AccountsPayable { get; set; }

        /// <summary>
        /// debt that matures in more than one year
        /// </summary>
        public decimal? CurrentLongTermDebt { get; set; }


        /// <summary>
        /// Current liabilities are a company's short-term financial obligations that are due within one year or within a normal operating cycle.
        /// </summary>
        public decimal? TotalCurrentLiabilities { get; set; }
        public decimal? OtherCurrentLiabilities { get; set; }
        public decimal? OtherLiabilities { get; set; }

        /// <summary>
        /// financial obligations of a company that are due more than one year in the future. 
        /// </summary>
        public decimal? LongTermDebt { get; set; }

        /// <summary>
        /// interest of less than 50% of an enterprise
        /// </summary>
        public decimal? MinorityInterest { get; set; }

        /// <summary>
        /// financial obligations a company owes to outside parties.
        /// current and long-term.
        /// </summary>
        public decimal? TotalLiabilities { get; set; }

        public decimal? CommonStock { get; set; }

        /// <summary>
        /// amount of net income left over for the business after it has paid out dividends to its shareholders
        /// </summary>
        public decimal? RetainedEarnings { get; set; }

        /// <summary>
        /// previously outstanding stock that is bought back from stockholders by the issuing company
        /// </summary>
        public decimal? TreasuryStock { get; set; }

        /// <summary>
        /// Capital surplus, or share premium, most commonly refers to the surplus resulting after common stock is sold for more than its par value
        /// </summary>
        public object CapitalSurplus { get; set; }

        /// <summary>
        /// corporation's owners' residual claim after debts have been paid
        /// </summary>
        public decimal? ShareholderEquity { get; set; }

        /// <summary>
        /// machinery, computers, buildings and land
        /// </summary>
        public decimal? NetTangibleAssets { get; set; }
    }
}

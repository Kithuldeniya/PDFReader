using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFReader.Models
{
    public class OutputModel
    {
        public OutputModel(string data)
        {
            var textLines = data.Split('\n');
            var title = textLines.FirstOrDefault(t => t.Contains("Confirmation For"));
            var ourRef = textLines.FirstOrDefault(t => t.Contains("Our Ref")).Split(':')[1].Trim();
            var fundName = textLines.FirstOrDefault(t => t.Contains("Fund Name")).Split(':')[1].Trim();
            var fundIdentifier = textLines.FirstOrDefault(t => t.Contains("Fund Identifier")).Split(':')[1].Replace("ISIN", "").Trim();
            var orderRef = textLines.FirstOrDefault(t => t.Contains("Order Reference")).Split(':')[1].Trim();
            var amount = textLines.FirstOrDefault(t => t.Contains("Amount")).Split(':')[1].Trim();
            var units = textLines.FirstOrDefault(t => t.Contains("Units")).Split(':')[1].Trim();
            var price = textLines.FirstOrDefault(t => t.Contains("Price")).Split(':')[1].Trim();
            var currency = textLines.FirstOrDefault(t => t.Contains("Currency")).Split(':')[1].Trim();
            var tradeDate = textLines.FirstOrDefault(t => t.Contains("Trade Date")).Split(':')[1].Trim();
            var settlementDate = textLines.FirstOrDefault(t => t.Contains("Settlement Date")).Split(':')[1].Trim();

            // if Fund name has two lines
            if (string.IsNullOrEmpty(fundName))
            {
                var fundNameLineText = textLines.FirstOrDefault(t => t.Contains("Fund Name"));
                var fundNameIndex = Array.IndexOf(textLines, fundNameLineText);
                fundName = textLines[fundNameIndex - 1] + " " + textLines[fundNameIndex + 1];
            }

            this.Title = title;
            this.OurRef = ourRef;
            this.FundName = fundName;
            this.FundIdentifier = fundIdentifier;
            this.OrderReference = orderRef;
            this.Amount = amount;
            this.Units = units;
            this.Price = price;
            this.Currency = currency;
            this.TradeDate = tradeDate;
            this.SettlementDate = settlementDate;
        }

        public string FileName { get; set; }
        public string OurRef { get; set; }
        public string Title { get; set; }
        public string FundName { get; set; }
        public string FundIdentifier { get; set; }
        public string OrderReference { get; set; }
        public string Amount { get; set; }
        public string Units { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string TradeDate { get; set; }
        public string SettlementDate { get; set; }
    }
}

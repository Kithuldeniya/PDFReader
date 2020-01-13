using CsvHelper;
using PDFReader.Models;
using System.Collections.Generic;
using System.IO;

namespace PDFReader.Helpers
{
    public static class CsvFileHelper
    {
        public static void WriteToFile(string outputPath, List<OutputModel> output)
        {
            
            using (var writer = new StreamWriter(outputPath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = ",";

                csvWriter.WriteField("File Name");
                csvWriter.WriteField("Our Ref");
                csvWriter.WriteField("Title");
                csvWriter.WriteField("Fund Name");
                csvWriter.WriteField("Fund Identifier");
                csvWriter.WriteField("Order Reference");
                csvWriter.WriteField("Amount");
                csvWriter.WriteField("Units");
                csvWriter.WriteField("Price");
                csvWriter.WriteField("Currency");
                csvWriter.WriteField("Trade Date");
                csvWriter.WriteField("Settlement Date");
                csvWriter.NextRecord();


                foreach (var item in output)
                {
                    csvWriter.WriteField(item.FileName);
                    csvWriter.WriteField(item.OurRef);
                    csvWriter.WriteField(item.Title);
                    csvWriter.WriteField(item.FundName);
                    csvWriter.WriteField(item.FundIdentifier);
                    csvWriter.WriteField(item.OrderReference);
                    csvWriter.WriteField(item.Amount);
                    csvWriter.WriteField(item.Units);
                    csvWriter.WriteField(item.Price);
                    csvWriter.WriteField(item.Currency);
                    csvWriter.WriteField(item.TradeDate);
                    csvWriter.WriteField(item.SettlementDate);
                    csvWriter.NextRecord();
                }

                writer.Flush();
            }
        }
    }
}

using System;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FileProcessor
{
    public class FileProcessor
    {
        private string inputPath = @"C:\FileInput\input.txt";
        private string outputPath = @"C:\FileOutput";

        public void ProcessFile()
        {
            string[] lines = File.ReadAllLines(inputPath);
            string fileName = $"Processed_{DateTime.Now:yyyy-MM-dd}.xlsx";
            string fullPath = Path.Combine(outputPath, fileName);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");
                for (int i = 0; i < lines.Length; i++)
                {
                    worksheet.Cells[i + 1, 1].Value = lines[i];
                }

                package.SaveAs(new FileInfo(fullPath));
            }

            EmailService.SendStatusEmail(true);
        }
    }
}

using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.utls.datas;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace VMS_Phase1PortalAT.utls
{
    public class WriteResultToCSV
    {
        private setupData setup;
        public string filePath;
        private int count = 0;

        public WriteResultToCSV()
        {
            setup = new();
            filePath = setup.resultFilePath;

        }

        public void WriteTestResults(TestContext TestContext, string time, string expectedResult, string errorMessage, string descrption)
        {
            count = count + 1;
            bool fileExists = File.Exists(filePath);
            var workbook = fileExists ? new XLWorkbook(filePath) : new XLWorkbook();
            var worksheet = workbook.Worksheets.Count > 0 ? workbook.Worksheet(1) : workbook.AddWorksheet("Test Results");

            // If the file is new, create the header row
            if (!fileExists)
            {
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Testcase Result";
                worksheet.Cell(1, 3).Value = "Expected Result";
                worksheet.Cell(1, 4).Value = "Duration";
                worksheet.Cell(1, 5).Value = "Executed Time";
                worksheet.Cell(1, 6).Value = "Testcase Name";
                worksheet.Cell(1, 7).Value = "Description";
                worksheet.Cell(1, 8).Value = "Error Message";
            }

            string testName = TestContext.TestName!;
            string testStatus = TestContext.CurrentTestOutcome.ToString();
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            time = time + " sec";
            string replaceWith = "";
            errorMessage = errorMessage.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);

            // Find the next available row by using LastRowUsed()
            int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 1;
            int id = lastRow;
            lastRow += 1;  // Move to the next row

            // Write the test data into the worksheet
            worksheet.Cell(lastRow, 1).Value = id;
            worksheet.Cell(lastRow, 2).Value = testStatus;
            worksheet.Cell(lastRow, 3).Value = expectedResult;
            worksheet.Cell(lastRow, 4).Value = time;
            worksheet.Cell(lastRow, 5).Value = currentTime;
            worksheet.Cell(lastRow, 6).Value = testName;
            worksheet.Cell(lastRow, 7).Value = descrption;
            worksheet.Cell(lastRow, 8).Value = errorMessage;

            // Save the workbook
            workbook.SaveAs(filePath);
        }
    }
}

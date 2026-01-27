//using AventStack.ExtentReports;
//using AventStack.ExtentReports.Reporter;
//using System;

//namespace VMS_Phase1PortalAT.Modules.Authentication
//{
//    public static class ExtentManager
//    {
//        private static ExtentReports _instance;
//        private static readonly object _lock = new object();

//        public static ExtentReports GetInstance()
//        {
//            if (_instance == null)
//            {
//                lock (_lock)
//                {
//                    if (_instance == null)
//                    {
//                        string reportPath = "D:\\ExtentReport\\AutomationReport"+ DateTime.Now.ToString("dd-MM-yyyy hh:mm") + ".html";

//                        var htmlReporter = new ExtentSparkReporter(reportPath);

//                        htmlReporter.Config.DocumentTitle = "Automation Test Report";
//                        htmlReporter.Config.ReportName = "Workflow Test Report";
//                        htmlReporter.Config.TimeStampFormat = "HH:mm:ss";

//                        // Optional JS to convert ms to sec in HTML table
//                        htmlReporter.Config.JS = @"
//                            document.addEventListener('DOMContentLoaded', function() {
//                                const durationCells = document.querySelectorAll('.duration');
//                                durationCells.forEach(function(cell) {
//                                    let ms = parseFloat(cell.innerText);
//                                    if (!isNaN(ms)) {
//                                        let sec = (ms / 1000).toFixed(2);
//                                        cell.innerText = sec + ' sec';
//                                    }
//                                });
//                            });
//                        ";

//                        _instance = new ExtentReports();
//                        _instance.AttachReporter(htmlReporter);
//                    }
//                }
//            }
//            return _instance;
//        }

//        /// <summary>
//        /// Log test duration in seconds
//        /// </summary>
//        public static void LogDuration(ExtentTest test, long elapsedMilliseconds)
//        {
//            double seconds = elapsedMilliseconds / 1000.0;
//            test.Log(AventStack.ExtentReports.Status.Info, $"Duration: {seconds:F2} sec");
//        }
//    }
//}
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;


public class ExtentManager
{
    private static ExtentReports _instance;
    private static readonly object _lock = new object();

    public static ExtentReports GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock) // thread safety
            {
                if (_instance == null)
                {
                    string timestamp = DateTime.Now.ToString("dd-MM-yyyy  HH-mm");

                    // Folder path
                    string folderPath = @"D:\ExtentReport\AutomationReport";

                    // Ensure folder exists
                    Directory.CreateDirectory(folderPath);

                    // Full file path
                    string reportPath = Path.Combine(folderPath, $"ExtentReport_{timestamp}.html");

                    var htmlReporter = new ExtentSparkReporter(reportPath);


                    htmlReporter.Config.DocumentTitle = "Automation Test Report";
                    htmlReporter.Config.ReportName = "WorkFlow Test Report";


                    _instance = new ExtentReports();
                    _instance.AttachReporter(htmlReporter);
                }
            }
        }

        return _instance;
    }
}
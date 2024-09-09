using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ResmarkApi;

namespace ResmarkApiDemo
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //Search Resmark printers
            var printers = PrinterSearch.Search();
            foreach (var message in printers.Result) Console.WriteLine(message);

            // Create an instance of the ResmarkAPI
            var printerUID = "02fd";
            var ipAddress = "192.168.174.225";
            var folderName = "/";
            var resmarkAPI = new ResmarkAPI(printerUID, ipAddress, folderName);

            // Demonstrating ResmarkAPI functions
            try
            {
                // 1. Retrieve printer status
                Console.WriteLine("\nRetrieving printer status...");
                var statusResult = await resmarkAPI.GetStatus();
                Console.WriteLine("Printer status: " + statusResult.Status);

                // 2. Retrieve messages
                Console.WriteLine("Retrieving messages...");
                var messagesResult = await resmarkAPI.GetMessages();
                if (messagesResult.Success)
                {
                    Console.WriteLine("Messages:");
                    foreach (var message in messagesResult.List) Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("Error retrieving messages: " + messagesResult.Message);
                }

                // 3. Print XML message
                Console.WriteLine("\nPrinting an .next message...");
                var messageXmlPath = "variable.next";
                if (File.Exists(messageXmlPath))
                {
                    var messageXml = File.ReadAllText(messageXmlPath);
                    var printXmlResult = await resmarkAPI.PrintMessageXML(messageXml);
                    Console.WriteLine("Printed successfully: " + printXmlResult.Success);
                }
                else
                {
                    Console.WriteLine("variable.next file not found.");
                }

                // 4. Cancel printing
                Console.WriteLine("\nCanceling print...");
                var cancelPrintResult = await resmarkAPI.CancelPrint();
                Console.WriteLine("Print canceled: " + cancelPrintResult.Success);

                // 5. Print stored message
                Console.WriteLine("\nPrinting a stored message...");
                var printStoredResult = await resmarkAPI.PrintStoredMessage("variable.next");
                Console.WriteLine("Printed successfully: " + printStoredResult.Success);

                // 6. Retrieve message variable data
                Console.WriteLine("\nRetrieving message variable data...");
                var messageVariablesResult = await resmarkAPI.GetMessageVariableData();
                if (messageVariablesResult.Success && messageVariablesResult.Dictionary != null)
                    foreach (var variable in messageVariablesResult.Dictionary)
                        Console.WriteLine($"{variable.Key}: {variable.Value}");
                else
                    Console.WriteLine("Error retrieving message variables: " + messageVariablesResult.Message);

                // 7. Set message variable data
                Console.WriteLine("\nSetting message variable data...");
                var newVariables = new Dictionary<string, string>
                {
                    { "", DateTime.Now.ToLongTimeString() }
                };

                var setVariableDataResult = await resmarkAPI.SetMessageVariableData(newVariables);
                Console.WriteLine("Message variable data set: " + setVariableDataResult.Success);

                // 8. Set message count
                Console.WriteLine("\nSetting message count...");
                var messageCount = 1; // Example count
                var setMessageCountResult = await resmarkAPI.SetMessageCount(messageCount);
                Console.WriteLine("Message count set: " + setMessageCountResult.Success);

                // 9. Pause printing
                Console.WriteLine("\nPausing print...");
                var pausePrintResult = await resmarkAPI.PausePrinting();
                Console.WriteLine("Print paused: " + pausePrintResult.Success);

                // 10. Resume printing
                Console.WriteLine("\nResuming print...");
                var resumePrintResult = await resmarkAPI.ResumePrinting();
                Console.WriteLine("Print resumed: " + resumePrintResult.Success);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("\nProgram completed. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
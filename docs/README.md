# ResmarkAPI Demo

This is a simple C# program for interfacing with a Resmark printer using the ResmarkAPI. It demonstrates how to connect to a Resmark printer, retrieve information, handle printing tasks, and manage message variables.

## Prerequisites

Before running this program, ensure you have the following prerequisites:

- A Diagraph Resmark printer with a reachable IP address.
- The .NET SDK installed.
- Diagraph-ResmarkAPI NuGet package (e.g., Diagraph-ResmarkAPI.1.0.0.nupkg)

## Getting Started

 1. Create a new C# project in your preferred development environment.

 2. Add the Diagraph-ResmarkAPI NuGet package to your project.

 3. Replace the default code in your project with the provided code.

 4. Build your program.

 5. Place a printer message "variable.next" in the bin Folder and run your program.
 
Content of the file "variable.next" could be

```xml
<?xml version="1.0"?>
<ProductObject Name="variable" GapBetweenPrint="1.000" PrintCount="1" TaskType="HighResTask" DPI="300">
	<Margin />
	<Box Width="10.000" Length="10.000" Height="18.000" />
	<Variables />
	<Panel Name="Front">
		<Head UID="0" Name="PrintHead1" Type="384" Offset="1.000" Enabled="True" DPI="300">
			<FieldObject xsi:type="VarFieldObject" X="1.708" Y="0.656" LockAspectRatio="True" Data="WWWWWWWWWW" Family="Arial" Style="Regular" Width="36" Height="36" Length="10" />
		</Head>
	</Panel>
	<Panel Name="Right" />
	<Panel Name="Back" />
	<Panel Name="Left" />
	<Panel Name="Top" />
	<Panel Name="Bottom" />
</ProductObject>
```

## Usage

This program connects to a Resmark printer, retrieves printer status, and manages messages and variable data. It provides the following functionalities:

- **Search for available Resmark printers** on the network and display their information.
- **Fetch information** about the connected printer, including status and errors.
- **Retrieve and print XML messages**.
- **Manage and print stored messages**.
- **Set message variable data** and **message counts** for printing.
- **Pause, resume, and cancel printing jobs**.

## Code Explanation

- The program utilizes the `ResmarkAPI` class to interact with the Resmark printer.
  
- It begins by searching for available printers on the network and connecting to the specified printer using its IP address and printer UID.

- It fetches important information about the connected printer's status and provides options to handle messages, including printing XML-based messages or stored messages on the printer.

- It allows you to set custom variable data for messages and set a specific message count for printing.

- The program also includes error handling and printer job control features such as pausing, resuming, and canceling prints.

### Example Code Snippets

Here are some important functions in the demo:

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Diagraph.ResmarkApi;

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
```

## Features

1. **Retrieve Printer Information**:
   - Get the current status and potential error details from the printer.
   
2. **Print Messages**:
   - Print messages stored in XML files or predefined stored messages on the printer.

3. **Set Message Variables**:
   - Dynamically set the variables for messages before printing using the `SetMessageVariableData()` method.

4. **Set Message Count**:
   - Define how many times a specific message should be printed with the `SetMessageCount()` function.

5. **Job Control**:
   - Control printing jobs by pausing, resuming, or canceling them.

## How to Run

1. Make sure the Resmark printer is connected to the network.
2. Modify the `printerUID`, `ipAddress`, and `folderName` fields in the demo program to match your printer's configuration.
3. Run the program and follow the prompts in the console to interact with the Resmark printer.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.md) file for details.

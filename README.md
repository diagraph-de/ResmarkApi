# ResmarkAPI

The `ResmarkAPI` is a C# library for interacting with Resmark printers. This project includes a demo application that showcases how to use the API to perform various printer operations such as printing messages, retrieving status, and managing print jobs.

## Features

- **Print XML Messages**: Send XML-formatted messages to the printer.
- **Print Stored Messages**: Print messages that are pre-stored on the printer.
- **Retrieve Messages**: Fetch messages available on the printer.
- **Manage Message Variables**: Set and retrieve variables used in stored messages.
- **Control Printing Operations**: Cancel, pause, and resume printing.
- **Get Printer Status**: Retrieve the printer's current status and error details.
 
## Installation

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/diagraph.de/ResmarkAPI.git
    ```

2. **Navigate to the Project Directory**:
    ```bash
    cd ResmarkAPI
    ```

3. **Restore NuGet Packages**:
    ```bash
    dotnet restore
    ```

4. **Build the Project**:
    ```bash
    dotnet build
    ```

## Usage

1. **Configure the Printer Details**:

    Update the printer ID, IP address, and optional folder name in the `Program.cs` file:
    ```csharp
    var printerUID = "02fd";
    var ipAddress = "192.168.174.225";
    var folderName = "/";
    ResmarkAPI resmarkAPI = new ResmarkAPI(printerId, ipAddress, folderName);
    ```

2. **Run the Demo Program**:

    Execute the application with visual studio to see various API functionalities in action. Use the following command:
 
    The program will demonstrate:
    - Retrieving messages
    - Printing an XML message
    - Printing a stored message
    - Managing message variables
    - Checking printer status
    - Cancelling, pausing, and resuming print jobs
 
--- 

using System.Collections.Generic;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;

namespace Diagraph.ResmarkApi.Interfaces;

public interface IPrinterService
{
    Task<OperationResultList> GetMessagesAsync(string printerId, string ipAddress, string folderName = "/");

    Task<OperationResult> PrintMessageAsync(string printerId, string ipAddress, string messageXml);

    Task<OperationResult> PrintStoredMessageAsync(string printerId, string ipAddress, string messageName,
        string folderName = "/");

    Task<OperationResult> SetMessageCountAsync(string printerId, string ipAddress, int count);

    Task<OperationResultDictionary> GetMessageVariableDataAsync(string printerId, string ipAddress);

    Task<OperationResult> SetMessageVariableDataAsync(string printerId, string ipAddress,
        Dictionary<string, string> variables);

    Task<OperationResult> CancelPrintAsync(string printerId, string ipAddress);

    Task<OperationResult> PausePrintingAsync(string printerId, string ipAddress);

    Task<OperationResult> ResumePrintingAsync(string printerId, string ipAddress);

    Task<OperationResultStatus> GetStatusInformationAsync(string printerId, string ipAddress);
}

public class OperationResult
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public CallResponse Response { get; set; }
    public string Message { get; set; }
}

public class OperationResultStatus : OperationResult
{
    public string Status { get; set; }
    public int EncoderSpeed { get; set; }
    public string MessageName { get; set; }
    public List<string> PrinterErrors { get; set; }
    public List<string> PrinterErrorDetails { get; set; }
}

public class OperationResultList : OperationResult
{
    public List<string> List { get; set; }
}

public class OperationResultDictionary : OperationResult
{
    public Dictionary<string, string> Dictionary { get; set; }
}
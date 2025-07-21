using Diagraph.ResmarkApi.Interfaces;
using Diagraph.ResmarkApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Diagraph.ResmarkApi.Services.ResmarkPrinterService;

public class ResmarkPrinterWrapper
{
    private readonly ResmarkPrinterService _service;

    public ResmarkPrinterWrapper(ResmarkPrinterService service, string id, string ip)
    {
        _service = service;
        IpAddress = ip;
        PrinterId = id;
    }

    public string IpAddress { get; }
    public string PrinterId { get; }
    public string LastStatus { get; private set; } = "";
    public bool LastSuccess { get; private set; }
    public bool IsConnected { get; set; }

    public string Status { get; private set; } = "";
    public int EncoderSpeed { get; private set; }
    public string MessageName { get; private set; }
    public List<string> PrinterErrors { get; private set; } = new();
    public List<string> PrinterErrorDetails { get; private set; } = new();

    public async Task<bool> SendMessageAsync(string xml)
    {
        var result = await _service.PrintMessageAsync(PrinterId, IpAddress, xml);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        IsConnected = result.Success;
        return result.Success;
    }

    public async Task<string> GetStatusAsync()
    {
        var result = await _service.GetStatusInformationAsync(PrinterId, IpAddress);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        IsConnected = result.Success;

        if (result.Success)
        {
            Status = result.Status ?? "";
            EncoderSpeed = result.EncoderSpeed;
            PrinterErrors = result.PrinterErrors ?? new List<string>();
            PrinterErrorDetails = result.PrinterErrorDetails ?? new List<string>();

            return $"{Status} ({EncoderSpeed} mm/s)" +
                   (PrinterErrors.Count > 0 ? $" | Errors: {string.Join(", ", PrinterErrors)}" : "");
        }

        return $"Error: {result.Error ?? "unknown"}";
    }

    public async Task<bool> SetPrintCountAsync(int count)
    {
        var result = await _service.SetMessageCountAsync(PrinterId, IpAddress, count);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result.Success;
    }

    public async Task<bool> PrintStoredMessageAsync(string name)
    {
        var result = await _service.PrintStoredMessageAsync(PrinterId, IpAddress, name);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result.Success;
    }

    public async Task<Dictionary<string, string>> GetVariablesAsync()
    {
        var result = await _service.GetMessageVariableDataAsync(PrinterId, IpAddress);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result.Success ? result.Dictionary : new Dictionary<string, string>();
    }

    public async Task<bool> SetVariableAsync(string key, string value)
    {
        var dict = new Dictionary<string, string> { [key] = value };
        var result = await _service.SetMessageVariableDataAsync(PrinterId, IpAddress, dict);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result.Success;
    }

    public async Task<bool> CancelPrintAsync()
    {
        var result = await _service.CancelPrintAsync(PrinterId, IpAddress);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result.Success;
    }

    public async Task<bool> PausePrintingAsync()
    {
        var result = await _service.PausePrintingAsync(PrinterId, IpAddress);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result.Success;
    }

    public async Task<bool> ResumePrintingAsync()
    {
        var result = await _service.ResumePrintingAsync(PrinterId, IpAddress);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result.Success;
    }

    public async Task<List<string>> GetMessagesAsync()
    {
        var result = await _service.GetMessagesAsync(PrinterId, IpAddress);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result.Success ? result.List : new List<string>();
    }

    public async Task<byte[]> PrintPreviewAsync(string messageXml, int task = 1)
    {
        return await _service.PrintPreviewAsync(PrinterId, IpAddress, messageXml, task);
    }

    public async Task<byte[]> PathPrintPreviewAsync(string messageName, int task = 1, string folderName="/")
    { 
        return await _service.PathPrintPreviewAsync(PrinterId, IpAddress, folderName, messageName, task);
    }

    public async Task<string> RecallMessageAsync(string messageName)
    {
        return await _service.RecallMessageAsync(PrinterId, IpAddress, messageName);
    }

    public async Task<OperationResult> SetConfiguration(string configurationXml)
    {
        var result = await _service.SetConfigurationAsync(PrinterId, IpAddress, configurationXml);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result;
    }

    public async Task<string> GetConfiguration()
    {
        var result = await _service.GetConfigurationAsync(PrinterId, IpAddress);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result.Success ? result.Value ?? string.Empty : string.Empty;
    }

    //public async Task<OperationResult> SetPrintHeadConfiguration(string configurationXml)
    //{
    //    var result = await _service.SetPrintHeadConfigurationAsync(PrinterId, IpAddress, configurationXml);
    //    LastStatus = result.Message;
    //    LastSuccess = result.Success;
    //    return result;
    //}

    //public async Task<string> GetPrintHeadConfiguration()
    //{
    //    var result = await _service.GetPrintHeadConfigurationAsync(PrinterId, IpAddress);
    //    LastStatus = result.Message;
    //    LastSuccess = result.Success;
    //    return result.Success ? result.Value ?? string.Empty : string.Empty;
    //}
}

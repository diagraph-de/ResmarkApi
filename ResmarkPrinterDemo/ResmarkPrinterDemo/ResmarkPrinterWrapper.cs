using System.Collections.Generic;
using System.Threading.Tasks;
using Diagraph.ResmarkApi.Services;

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
    public bool LastSendSuccess { get; set; }

    public async Task<bool> SendMessageAsync(string xml)
    {
        var result = await _service.PrintMessageAsync(PrinterId, IpAddress, xml);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result.Success;
    }

    public async Task<string> GetStatusAsync()
    {
        var result = await _service.GetStatusInformationAsync(PrinterId, IpAddress);
        LastStatus = result.Message;
        LastSuccess = result.Success;
        return result.Message ?? "unknown";
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
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Diagraph.ResmarkApi.Interfaces;
using Diagraph.ResmarkApi.Services;
using static Diagraph.ResmarkApi.Services.ResmarkPrinterService;

namespace Diagraph.ResmarkApi;

public class ResmarkAPI
{
    private const string DefaultFolderName = "/";
    private readonly string _folderName;
    private readonly string _ipAddress;
    private readonly string _printerId;
    private readonly ResmarkPrinterService _resmarkPrinterService;
    private bool _disposed;

    // Constructor with optional parameters for cache and logger
    public ResmarkAPI(string printerId, string ipAddress, string folderName = DefaultFolderName, bool cache = true)
    {
        _printerId = printerId;
        _ipAddress = ipAddress;
        _folderName = folderName;

        var clientService = new ClientService();
        _resmarkPrinterService = new ResmarkPrinterService(clientService, cache);
    }

    public async Task<OperationResultList> GetMessages()
    {
        return await _resmarkPrinterService.GetMessagesAsync(_printerId, _ipAddress, _folderName);
    }

    public async Task<OperationResult> PrintMessageXML(string messageXml)
    {
        return await _resmarkPrinterService.PrintMessageAsync(_printerId, _ipAddress, messageXml);
    }

    public async Task<OperationResult> PrintStoredMessage(string messageName)
    {
        return await _resmarkPrinterService.PrintStoredMessageAsync(_printerId, _ipAddress, messageName, _folderName);
    }

    public async Task<OperationResult> SetMessageCount(int count)
    {
        return await _resmarkPrinterService.SetMessageCountAsync(_printerId, _ipAddress, count);
    }

    public async Task<OperationResultDictionary> GetMessageVariableData()
    {
        return await _resmarkPrinterService.GetMessageVariableDataAsync(_printerId, _ipAddress);
    }

    public async Task<OperationResult> SetMessageVariableData(Dictionary<string, string> variables)
    {
        return await _resmarkPrinterService.SetMessageVariableDataAsync(_printerId, _ipAddress, variables);
    }

    public async Task<OperationResult> CancelPrint()
    {
        return await _resmarkPrinterService.CancelPrintAsync(_printerId, _ipAddress);
    }

    public async Task<OperationResult> PausePrinting()
    {
        return await _resmarkPrinterService.PausePrintingAsync(_printerId, _ipAddress);
    }

    public async Task<OperationResult> ResumePrinting()
    {
        return await _resmarkPrinterService.ResumePrintingAsync(_printerId, _ipAddress);
    }

    public async Task<OperationResultStatus> GetStatus()
    {
        return await _resmarkPrinterService.GetStatusInformationAsync(_printerId, _ipAddress);
    }

    public async Task<byte[]> PrintPreview(string messageXml, int task = 1)
    {
        return await _resmarkPrinterService.PrintPreviewAsync(_printerId, _ipAddress, messageXml, task);
    }

    public async Task<byte[]> PathPrintPreview(string messageName, int task = 1)
    {
        return await _resmarkPrinterService.PathPrintPreviewAsync(_printerId, _ipAddress, _folderName, messageName,
            task);
    }

    public async Task<string> RecallMessage(string messageName)
    {
        return await _resmarkPrinterService.RecallMessageAsync(_printerId, _ipAddress, messageName);
    }
    //public async Task<OperationResult> SetPrintHeadConfiguration(string configurationXml)
    //{
    //    return await _resmarkPrinterService.SetPrintHeadConfigurationAsync(_printerId, _ipAddress, configurationXml);
    //}

    //public async Task<OperationResultString> GetPrintHeadConfiguration()
    //{
    //    return await _resmarkPrinterService.GetPrintHeadConfigurationAsync(_printerId, _ipAddress);
    //}

    public async Task<OperationResult> SetConfiguration(string configurationXml)
    {
        return await _resmarkPrinterService.SetConfigurationAsync(_printerId, _ipAddress, configurationXml);
    }

    public async Task<OperationResultString> GetConfiguration()
    {
        return await _resmarkPrinterService.GetConfigurationAsync(_printerId, _ipAddress);
    }
}
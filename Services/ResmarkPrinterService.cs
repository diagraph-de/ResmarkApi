using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Diagraph.ResmarkApi.Interfaces;
using Diagraph.ResmarkApi.Models;
using Newtonsoft.Json;
using Opc.Ua;
using Opc.Ua.Client;

namespace Diagraph.ResmarkApi.Services;

public class ResmarkPrinterService : IPrinterService
{
    private const string ObjectIdString = "i=85";
    public const int DefaultOpcuaPort = 16664;
    private static readonly NodeId ObjectId = NodeId.Parse(ObjectIdString);

    public static Dictionary<string, Session> _sessions = new();
    private readonly bool _cache;

    private readonly ClientService _clientService;

    public ResmarkPrinterService(ClientService clientService, bool cache = true)
    {
        _clientService = clientService;
        _cache = cache;
    }

    public async Task<OperationResultList> GetMessagesAsync(string printerId, string ipAddress, string folderName = "/")
    {
        var request = CreateCallMethodRequest("ns=2;s=PathGetStoredMessageList", folderName);
        var result = await ExecutePrinterOperationAsync<object>(printerId, ipAddress, request);
        var list = new List<string>();
        try
        {
            if (result.Response != null && result.Response.Results != null)
                list = ((string[])result.Response.Results[0].OutputArguments[1].Value).ToList();
        }
        catch (Exception e)
        {
            result.Success = false;
        }

        return new OperationResultList
        {
            Response = result.Response,
            Success = result.Success,
            Message = result.Success ? "Message list loaded" : "Failed to get message list.",
            Error = result.Error,
            List = list
        };
    }

    public async Task<OperationResult> PrintMessageAsync(string printerId, string ipAddress, string messageXml)
    {
        var request = CreateCallMethodRequest("ns=2;s=PrintPrd", messageXml, 1);
        var result = await ExecutePrinterOperationAsync<int>(printerId, ipAddress, request);

        return new OperationResult
        {
            Response = result.Response,
            Success = result.Success,
            Message = result.Success ? $"Printing Message: {messageXml}" : "Failed to print message.",
            Error = result.Error
        };
    }

    public async Task<OperationResult> PrintStoredMessageAsync(string printerId, string ipAddress, string messageName,
        string folderName = "/")
    {
        if (string.IsNullOrEmpty(messageName))
            return new OperationResult { Success = false, Error = "A message name was not specified." };

        folderName = folderName ?? "/"; // Ensure folderName is not null
        var request = CreateCallMethodRequest("ns=2;s=PathPrintStoredMessage", 1, folderName, messageName);
        var result = await ExecutePrinterOperationAsync<int>(printerId, ipAddress, request);

        return new OperationResult
        {
            Response = result.Response,
            Success = result.Success,
            Message = result.Success
                ? $"Printing Message: {Path.Combine(folderName, messageName)}"
                : "Failed to print stored message.",
            Error = result.Error
        };
    }

    public async Task<OperationResult> SetMessageCountAsync(string printerId, string ipAddress, int count)
    {
        var request = CreateCallMethodRequest("ns=2;s=SetMessageCount", (ulong)count);
        var result = await ExecutePrinterOperationAsync<int>(printerId, ipAddress, request);

        return new OperationResult
        {
            Response = result.Response,
            Success = result.Success,
            Message = result.Success ? $"Message count set to {count}" : "Failed to set message count.",
            Error = result.Error
        };
    }

    public async Task<OperationResultDictionary> GetMessageVariableDataAsync(string printerId, string ipAddress)
    {
        var request = CreateCallMethodRequest("ns=2;s=GetMessageVariableData", 1);
        var result = await ExecutePrinterOperationAsync<string>(printerId, ipAddress, request);

        if (!result.Success)
            return new OperationResultDictionary
            {
                Response = result.Response,
                Success = false,
                Message = "Failed to get message variable data.",
                Error = result.Error
            };

        try
        {
            var message = result.Response.Results[0].OutputArguments[1].ToString();
            var deserializedDictionary = SerializableDictionary.Deserialize(message);
            var jsonResult = JsonConvert.SerializeObject(deserializedDictionary?.MessageVariableData);

            return new OperationResultDictionary
            {
                Success = true,
                Message = jsonResult,
                Dictionary = deserializedDictionary.MessageVariableData
            };
        }
        catch (Exception ex)
        {
            return new OperationResultDictionary
            {
                Response = result.Response,
                Success = false,
                Message = "Failed to process message variable data.",
                Error = $"Error during deserialization: {ex.Message}"
            };
        }
    }

    public async Task<OperationResult> SetMessageVariableDataAsync(string printerId, string ipAddress,
        Dictionary<string, string> variables)
    {
        if (variables == null || !variables.Any())
            return new OperationResult { Success = false, Error = "Variables cannot be null or empty." };

        var value = new SerializableDictionary { MessageVariableData = variables }.Serialize();
        var request = CreateCallMethodRequest("ns=2;s=SetMessageVariableData", 1, value);
        var result = await ExecutePrinterOperationAsync<int>(printerId, ipAddress, request);

        return new OperationResult
        {
            Response = result.Response,
            Success = result.Success,
            Message = result.Success ? "Variable Data set." : "Failed to set variable data.",
            Error = result.Error
        };
    }

    public async Task<OperationResult> CancelPrintAsync(string printerId, string ipAddress)
    {
        return await ExecuteSimplePrinterCommandAsync(printerId, ipAddress, "ns=2;s=CancelPrinting",
            "Cancelled Print.");
    }

    public async Task<OperationResult> PausePrintingAsync(string printerId, string ipAddress)
    {
        return await ExecuteSimplePrinterCommandAsync(printerId, ipAddress, "ns=2;s=StopPrinting",
            "Printing is Paused.");
    }

    public async Task<OperationResult> ResumePrintingAsync(string printerId, string ipAddress)
    {
        return await ExecuteSimplePrinterCommandAsync(printerId, ipAddress, "ns=2;s=ResumePrinting",
            "Printing has resumed.");
    }

    public async Task<OperationResultStatus> GetStatusInformationAsync(string printerId, string ipAddress)
    {
        // Create the request for getting status information
        var request = CreateCallMethodRequest("ns=2;s=GetStatusInformation", 1);

        // Execute the printer operation asynchronously and await the response
        var response = await ExecutePrinterOperationAsync<object>(printerId, ipAddress, request);

        // Initialize the result object with basic response data
        var ret = new OperationResultStatus
        {
            Response = response.Response,
            Success = response.Success,
            Message = response.Success ? "Status Data received." : "Failed to get status.",
            Error = response.Error
        };

        // Process the response if it was successful
        if (response.Success && response.Response?.Results != null && response.Response.Results.Count > 0)
        {
            var result = response.Response.Results[0];
            var outputArgs = result.OutputArguments;

            // Ensure output arguments are as expected before accessing them
            if (outputArgs.Count > 3 && outputArgs[3].Value != null)
                ret.EncoderSpeed = Convert.ToInt32(outputArgs[3].Value);
            if (outputArgs.Count > 1 && outputArgs[1].Value != null) ret.Status = outputArgs[1].Value?.ToString();

            // Populate PrinterErrorDetails with values from OutputArguments[6] if it exists
            if (outputArgs.Count > 6 && outputArgs[6].Value is string[] errorDetails)
                ret.PrinterErrorDetails = new List<string>(errorDetails);
            else
                ret.PrinterErrorDetails = new List<string>();

            // Populate PrinterErrors with values from OutputArguments[5] if it exists
            if (outputArgs.Count > 5 && outputArgs[5].Value is string[] errors)
                ret.PrinterErrors = new List<string>(errors);
            else
                ret.PrinterErrors = new List<string>();

            ret.MessageName = "";
            if (outputArgs.Count > 2 && outputArgs[2].Value != null) ret.MessageName = outputArgs[2].Value + "";

            if (ret.MessageName.Length > 0 && !ret.MessageName.EndsWith(".next"))
                ret.MessageName += ".next";
        }
        else
        {
            Console.WriteLine(response.Error);
            ret.Success = false;
        }

        // Return the populated result object
        return ret;
    }

    public async Task<byte[]> PrintPreviewAsync(string printerId, string ipAddress, string prdXml, int task = 1)
    {
        var request = CreateCallMethodRequest("ns=2;s=PrintPreview", prdXml, task);
        var response = await ExecutePrinterOperationAsync<object>(printerId, ipAddress, request);

        if (response.Success &&
            response.Response?.Results?.FirstOrDefault()?.OutputArguments?.LastOrDefault()
                .Value is string[] base64Array)
        {
            var first = base64Array.FirstOrDefault(s => !string.IsNullOrWhiteSpace(s));
            return first != null ? Convert.FromBase64String(first) : Array.Empty<byte>();
        }

        return Array.Empty<byte>();
    }


    public async Task<byte[]> PathPrintPreviewAsync(string printerId, string ipAddress, string folderName,
        string messageName, int task = 1)
    {
        var request = CreateCallMethodRequest("ns=2;s=PathPrintPreview", task, folderName, messageName);
        var response = await ExecutePrinterOperationAsync<object>(printerId, ipAddress, request);

        if (response.Success &&
            response.Response?.Results?.FirstOrDefault()?.OutputArguments?.LastOrDefault()
                .Value is string[] base64Array)
        {
            var first = base64Array.FirstOrDefault(s => !string.IsNullOrWhiteSpace(s));
            return first != null ? Convert.FromBase64String(first) : Array.Empty<byte>();
        }

        return Array.Empty<byte>();
    }


    public async Task<string> RecallMessageAsync(string printerId, string ipAddress, string messageName)
    {
        var request = CreateCallMethodRequest("ns=2;s=RecallMessage", messageName);
        var response = await ExecutePrinterOperationAsync<object>(printerId, ipAddress, request);

        if (response.Success &&
            response.Response?.Results?.FirstOrDefault()?.OutputArguments?.LastOrDefault().Value is string xml)
            return xml;

        return string.Empty;
    }

    public async Task<OperationResultString> GetConfigurationAsync(string printerId, string ipAddress)
    {
        var request = CreateCallMethodRequest("ns=2;s=GetConfiguration");
        var response = await ExecutePrinterOperationAsync<object>(printerId, ipAddress, request);

        if (response.Success &&
            response.Response?.Results?.FirstOrDefault()?.OutputArguments?.LastOrDefault().Value is string configXml)
            return new OperationResultString
            {
                Success = true,
                Message = "Configuration retrieved.",
                Value = configXml
            };

        return new OperationResultString
        {
            Success = false,
            Message = "Failed to retrieve print head configuration.",
            Error = response.Error
        };
    }

    public async Task<OperationResult> SetConfigurationAsync(string printerId, string ipAddress,
        string configurationXml)
    {
        var request = CreateCallMethodRequest("ns=2;s=SetConfiguration", configurationXml);
        var response = await ExecutePrinterOperationAsync<object>(printerId, ipAddress, request);

        return new OperationResult
        {
            Success = response.Success,
            Response = response.Response,
            Message = response.Success ? "Configuration set." : "Failed to set configuration.",
            Error = response.Error
        };
    }

    //public async Task<OperationResultString> GetPrintHeadConfigurationAsync(string printerId, string ipAddress)
    //{
    //    var request = CreateCallMethodRequest("ns=2;s=GetPrintHeadConfiguration");
    //    var response = await ExecutePrinterOperationAsync<object>(printerId, ipAddress, request);

    //    if (response.Success &&
    //        response.Response?.Results?.FirstOrDefault()?.OutputArguments?.LastOrDefault().Value is string configXml)
    //    {
    //        return new OperationResultString
    //        {
    //            Success = true,
    //            Message = "Print head configuration retrieved.",
    //            Value = configXml
    //        };
    //    }

    //    return new OperationResultString
    //    {
    //        Success = false,
    //        Message = "Failed to retrieve print head configuration.",
    //        Error = response.Error
    //    };
    //}

    //public async Task<OperationResult> SetPrintHeadConfigurationAsync(string printerId, string ipAddress, string configurationXml)
    //{
    //    var request = CreateCallMethodRequest("ns=2;s=SetPrintHeadConfiguration", configurationXml);
    //    var response = await ExecutePrinterOperationAsync<object>(printerId, ipAddress, request);

    //    return new OperationResult
    //    {
    //        Success = response.Success,
    //        Response = response.Response,
    //        Message = response.Success ? "Print head configuration set." : "Failed to set print head configuration.",
    //        Error = response.Error
    //    };
    //}

    private CallMethodRequest CreateCallMethodRequest(string methodId, params object[] args)
    {
        return new CallMethodRequest
        {
            MethodId = NodeId.Parse(methodId),
            ObjectId = ObjectId,
            InputArguments = new VariantCollection(args.Select(arg => new Variant(arg)))
        };
    }

    private async Task<OperationResult> ExecutePrinterOperationAsync<T>(string printerId, string ipAddress,
        CallMethodRequest request)
    {
        try
        {
            var callResponse = await CallResponse(printerId, ipAddress, request);
            return HandleResponse<T>(callResponse);
        }
        catch (Exception ex)
        {
            // Log the exception
            // Logger.LogError(ex, "Error executing printer operation");
            return new OperationResult
            {
                Success = false,
                Error = $"An error occurred: {ex.Message}"
            };
        }
    }

    private async Task<OperationResult> ExecuteSimplePrinterCommandAsync(string printerId, string ipAddress,
        string methodId, string successMessage)
    {
        var request = CreateCallMethodRequest(methodId, 1);
        var result = await ExecutePrinterOperationAsync<int>(printerId, ipAddress, request);

        return new OperationResult
        {
            Success = result.Success,
            Message = result.Success ? successMessage : "Command failed.",
            Error = result.Error
        };
    }

    private async Task<CallResponse?> CallResponse(string printerId, string ipAddress,
        CallMethodRequest callMethodRequest)
    {
        Session session = null;
        if (_cache)
        {
            var key = printerId + ipAddress;
            if (!_sessions.ContainsKey(key))
            {
                session = await _clientService.OpenOpcuaSessionAsync(printerId, ipAddress, DefaultOpcuaPort).ConfigureAwait(false);
                _sessions.Add(key, session);
            }
            else
            {
                session = _sessions[key];
            }
        }
        else
        {
            session = await _clientService.OpenOpcuaSessionAsync(printerId, ipAddress, DefaultOpcuaPort).ConfigureAwait(false);
        }

        try
        {
            var methods = new CallMethodRequestCollection { callMethodRequest };
            session.Call(null, methods, out var results, out var diagnosticInfos);

            return new CallResponse
            {
                ResponseHeader = new ResponseHeader { ServiceResult = StatusCodes.Good },
                Results = results,
                DiagnosticInfos = diagnosticInfos
            };
        }
        finally
        {
            if (!_cache && session != null)
            {
                try { session.Close(); } catch { }
                session.Dispose();
            }
        }
    }

    private OperationResult HandleResponse<T>(CallResponse? callResponse)
    {
        if (callResponse is null || callResponse.Results is null || callResponse.ResponseHeader is null)
            return new OperationResult
            {
                Success = false,
                Error = "The printer did not respond."
            };

        if (StatusCode.IsGood(callResponse.ResponseHeader.ServiceResult) && callResponse.Results.Count != 0)
            return new OperationResult
            {
                Success = true,
                Response = callResponse
            };

        return new OperationResult
        {
            Success = false,
            Error = "Bad request."
        };
    }

    public class OperationResultString : OperationResult
    {
        public string? Value { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Diagraph.PCD.Opcua.Interfaces;
using Newtonsoft.Json;
using ResmarkApi.PrinterApi.Shared.Interfaces;
using ResmarkApi.PrinterApi.Shared.Models;
using Workstation.ServiceModel.Ua;

namespace ResmarkApi.PrinterApi.Shared.Services;

public class ResmarkPrinterService : IPrinterService
{
    private const string ObjectIdString = "i=85";
    private const int DefaultOpcuaPort = 16664;
    private static readonly NodeId ObjectId = NodeId.Parse(ObjectIdString);

    private readonly IClientService _clientService;

    internal ResmarkPrinterService(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<OperationResultList> GetMessagesAsync(string printerId, string ipAddress, string folderName = "/")
    {
        var request = CreateCallMethodRequest("ns=2;s=PathGetStoredMessageList", folderName);
        var result = await ExecutePrinterOperationAsync<object>(printerId, ipAddress, request);
        var list = new List<string>();
        try
        {
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
        if (response.Success && response.Response?.Results != null && response.Response.Results.Length > 0)
        {
            var result = response.Response.Results[0];
            var outputArgs = result.OutputArguments;

            // Ensure output arguments are as expected before accessing them
            if (outputArgs.Length > 3 && outputArgs[3].Value != null)
                ret.EncoderSpeed = Convert.ToInt32(outputArgs[3].Value);
            if (outputArgs.Length > 1 && outputArgs[1].Value != null) ret.Status = outputArgs[1].Value?.ToString();

            // Populate PrinterErrorDetails with values from OutputArguments[6] if it exists
            if (outputArgs.Length > 6 && outputArgs[6].Value is string[] errorDetails)
                ret.PrinterErrorDetails = new List<string>(errorDetails);
            else
                ret.PrinterErrorDetails = new List<string>();

            // Populate PrinterErrors with values from OutputArguments[5] if it exists
            if (outputArgs.Length > 5 && outputArgs[5].Value is string[] errors)
                ret.PrinterErrors = new List<string>(errors);
            else
                ret.PrinterErrors = new List<string>();
        }

        // Return the populated result object
        return ret;
    }


    private CallMethodRequest CreateCallMethodRequest(string methodId, params object[] args)
    {
        return new CallMethodRequest
        {
            MethodId = NodeId.Parse(methodId),
            ObjectId = ObjectId,
            InputArguments = args.Select(arg => new Variant(arg)).ToArray()
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
        var channel = await _clientService.OpenOpcuaChannelAsync(printerId, ipAddress, DefaultOpcuaPort);

        if (channel is IRequestChannel requestChannel)
        {
            var callRequest = new CallRequest
            {
                MethodsToCall = new[] { callMethodRequest }
            };

            var ret = await requestChannel.CallAsync(callRequest);
            await channel.CloseAsync();
            return ret;
        }

        throw new InvalidOperationException("The channel is not of type IRequestChannel.");
    }

    private OperationResult HandleResponse<T>(CallResponse? callResponse)
    {
        if (callResponse is null || callResponse.Results is null || callResponse.ResponseHeader is null)
            return new OperationResult
            {
                Success = false,
                Error = "The printer did not respond."
            };

        if (StatusCode.IsGood(callResponse.ResponseHeader.ServiceResult) && callResponse.Results.Length != 0)
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
}
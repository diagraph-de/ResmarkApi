using System.Collections.Generic;
using System.Linq;
using Diagraph.ResmarkApi.Services;

namespace ResmarkPrinterGroupDemo;

public class PrinterGroupManager
{
    private readonly ResmarkPrinterService printerService = new(new ClientService());
    public List<ResmarkPrinterWrapper> Printers { get; } = new();

    public void AddPrinter(string printer)
    {
        var id = printer.Split("-")[0].Trim();
        var ip = printer.Split("-")[1].Trim();
        var wrapper = new ResmarkPrinterWrapper(printerService, id, ip);
        if (!Printers.Exists(p => p.IpAddress == ip)) Printers.Add(wrapper);
    }

    public void RemovePrinter(string ip)
    {
        Printers.RemoveAll(p => p.IpAddress == ip);
    }

    public ResmarkPrinterWrapper? GetPrinter(string printer)
    {
        var parts = printer.Split('-');
        if (parts.Length != 2)
            return null;

        var ip = parts[1].Trim();
        return Printers.FirstOrDefault(p => p.IpAddress == ip);
    }
}
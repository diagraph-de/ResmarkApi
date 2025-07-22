using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ResmarkPrinterGroupDemo
{
    public class OffsetConfigurationManager
    {
        private readonly string _configPath;
        private readonly Dictionary<string, int> _offsets;

        public OffsetConfigurationManager()
        {
            _configPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ResmarkPrinterGroupDemo",
                "offsets.json"
            );

            _offsets = Load();
        }

        private Dictionary<string, int> Load()
        {
            try
            {
                if (File.Exists(_configPath))
                {
                    var json = File.ReadAllText(_configPath);
                    return JsonSerializer.Deserialize<Dictionary<string, int>>(json) ?? new();
                }
            }
            catch
            {
                // ignore load error
            }

            return new Dictionary<string, int>();
        }

        public void Save()
        {
            try
            {
                var dir = Path.GetDirectoryName(_configPath);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                var json = JsonSerializer.Serialize(_offsets, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_configPath, json);
            }
            catch
            {
                // ignore save error
            }
        }

        private static string GetKey(string printerId, string ipAddress, string messageName)
        {
            return $"{printerId} | {ipAddress} | {messageName}";
        }

        public int? GetOffset(string printerId, string ipAddress, string messageName)
        {
            var key = GetKey(printerId, ipAddress, messageName);
            return _offsets.TryGetValue(key, out var value) ? value : null;
        }

        public void SetOffset(string printerId, string ipAddress, string messageName, int offset)
        {
            var key = GetKey(printerId, ipAddress, messageName);
            _offsets[key] = offset;
            Save();
        }

        public void RemoveOffset(string printerId, string ipAddress, string messageName)
        {
            var key = GetKey(printerId, ipAddress, messageName);
            if (_offsets.Remove(key)) Save();
        }

        public IReadOnlyDictionary<string, int> GetAll() => _offsets;

        public int GetStandardOffset(string printer)
        {
            if (string.IsNullOrWhiteSpace(printer) || !printer.Contains(" - "))
                return 0;

            var parts = printer.Split(new[] { " - " }, StringSplitOptions.None);
            if (parts.Length != 2)
                return 0;

            string printerId = parts[0].Trim();
            string ipAddress = parts[1].Trim();
            string messageName = "<Standard>";

            var existing = GetOffset(printerId, ipAddress, messageName);
            if (existing.HasValue)
                return existing.Value;
             
            SetOffset(printerId, ipAddress, messageName, 0); 
            return 0;

        }


    }
}

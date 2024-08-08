using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ResmarkApi.PrinterApi.Shared.Models;

[Serializable]
[XmlRoot(ElementName = "DictionaryOfString_String", Namespace = "", IsNullable = false)]
public class SerializableDictionary
{
    [XmlElement("Item")]
    public DictionaryOfString_StringItem[] Item
    {
        get
        {
            return MessageVariableData.Select(kv => new DictionaryOfString_StringItem
            {
                Key = kv.Key,
                Value = kv.Value
            }).ToArray();
        }
        set
        {
            if (value != null) MessageVariableData = value.ToDictionary(k => k.Key, v => v.Value);
        }
    }

    [XmlIgnore] public Dictionary<string, string> MessageVariableData { get; set; } = new();

    public string Serialize()
    {
        if (MessageVariableData == null)
            throw new InvalidOperationException("The MessageVariableData property must be set before serialization.");

        var xmlSerializer = new XmlSerializer(typeof(SerializableDictionary));
        using var memoryStream = new MemoryStream();
        xmlSerializer.Serialize(memoryStream, this);
        memoryStream.Position = 0;
        using var streamReader = new StreamReader(memoryStream);
        return streamReader.ReadToEnd();
    }

    public static SerializableDictionary? Deserialize(string xml)
    {
        if (string.IsNullOrEmpty(xml))
            throw new ArgumentNullException(nameof(xml));

        var xmlSerializer = new XmlSerializer(typeof(SerializableDictionary));
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
        return xmlSerializer.Deserialize(stream) as SerializableDictionary;
    }
}

public class DictionaryOfString_StringItem
{
    [XmlElement("Key")] public string Key { get; set; } = string.Empty;

    [XmlElement("Value")] public string Value { get; set; } = string.Empty;
}
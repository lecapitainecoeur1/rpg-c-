using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class LanguageService
{
    private Dictionary<string, string> translations = new();

    public void Load(string lang)
    {
        string path = Path.Combine("lang", $"{lang}.json");

        if (!File.Exists(path))
        {
            Console.WriteLine($"Lang file not found: {path}");
            return;
        }

        string json = File.ReadAllText(path);
        translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }

    public string Get(string key)
    {
        return translations.TryGetValue(key, out var value)
            ? value
            : $"[{key}]";
    }
}
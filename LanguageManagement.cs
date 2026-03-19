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
            Console.WriteLine($"Fichier langue introuvable : {path}");
            translations = new Dictionary<string, string>();
            return;
        }

        string json = File.ReadAllText(path);
        translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }

    // 👉 Méthode simple
    public string Get(string key)
    {
        if (translations == null)
            return $"[{key}]";

        return translations.TryGetValue(key, out var value)
            ? value
            : $"[{key}]";
    }

    // 👉 Méthode avec variables
    public string Get(string key, Dictionary<string, string> variables)
    {
        string text = Get(key);

        foreach (var pair in variables)
        {
            text = text.Replace("{" + pair.Key + "}", pair.Value);
        }

        return text;
    }
}
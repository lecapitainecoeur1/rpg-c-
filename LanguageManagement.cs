using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Reflection;

public class LanguageService
{
    private Dictionary<string, string> translations = new();

    public void Load(string lang)
    {
        string exePath = Assembly.GetExecutingAssembly().Location;
        if (string.IsNullOrEmpty(exePath))
        {
            Console.WriteLine("Impossible de déterminer le chemin de l'exécutable.");
            translations = new Dictionary<string, string>();
            return;
        }
        string exeDir = Path.GetDirectoryName(exePath);
        if (string.IsNullOrEmpty(exeDir))
        {
            Console.WriteLine("Impossible de déterminer le répertoire de l'exécutable.");
            translations = new Dictionary<string, string>();
            return;
        }
        DirectoryInfo? rootDirInfo = new DirectoryInfo(exeDir).Parent?.Parent?.Parent;
        if (rootDirInfo == null)
        {
            Console.WriteLine("Impossible de déterminer le répertoire racine du projet.");
            translations = new Dictionary<string, string>();
            return;
        }
        string rootDir = rootDirInfo.FullName;
        string path = Path.Combine(rootDir, "lang", $"{lang}.json");

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
        if (translations == null || !translations.ContainsKey(key))
            return $"[{key}]";

        return translations[key];
    }

    // 👉 Méthode avec variables
    public string Get(string key, Dictionary<string, string> variables)
    {
        // Récupération de la traduction pour la clé
        string text = Get(key);

        // Si la traduction existe, procéder au remplacement des variables
        if (text.Contains("{") && text.Contains("}"))
        {
            foreach (var pair in variables)
            {
                text = text.Replace("{" + pair.Key + "}", pair.Value);
            }
        }

        return text;
    }
}
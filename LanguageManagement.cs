using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class TraductionManager
{
    // Dictionnaire qui stocke toutes les traductions
    private Dictionary<string, string> textes;
    
    // Constructeur : charge une langue au démarrage
    public TraductionManager(string langue)
    {
        ChargerLangue(langue);
    }
    
    // Méthode pour charger un fichier de langue
    public void ChargerLangue(string langue)
    {
        // Chemin vers le fichier (ex: "fr.json")
        string fichier = $"{langue}.json";
        
        // Vérifier si le fichier existe
        if (!File.Exists(fichier))
        {
            Console.WriteLine($"ERREUR : Le fichier {fichier} n'existe pas !");
            textes = new Dictionary<string, string>();
            return;
        }
        
        // Lire tout le contenu du fichier
        string contenuJson = File.ReadAllText(fichier);
        
        // Convertir le JSON en dictionnaire C#
        textes = JsonConvert.DeserializeObject<Dictionary<string, string>>(contenuJson);
        
        Console.WriteLine($"Langue chargée : {langue}");
    }
    
    // Méthode pour récupérer un texte traduit
    public string Obtenir(string cle)
    {
        // Si la clé existe, retourne le texte
        if (textes.ContainsKey(cle))
        {
            return textes[cle];
        }
        
        // Sinon, affiche une erreur
        return $"[TEXTE MANQUANT : {cle}]";
    }
}
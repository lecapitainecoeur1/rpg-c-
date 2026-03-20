
public class Ennemy
{
    LanguageService lang = new LanguageService();
    Config config = Config.Load();
    public string nom;
    public int vie;
    public int degats;
    public int expDrop;
    public Ennemy(string unNom, int nbVie, int nbDegats, int nbExpDrop)
    {
        nom = unNom;
        vie = nbVie;
        degats = nbDegats;
        expDrop = nbExpDrop;
        lang.Load(config.language);
    }
    public Ennemy(Joueur joueur, Random rnd)
    {
        if (joueur.lvl < 5)
        {
            nom = "gobelin";
            vie = 20;
            degats = 3;
            expDrop = 5;
        }
        else if (joueur.lvl <= 10)
        {
            if (rnd.Next(0, 2) == 0)
            {
                nom = "gobelin";
                vie = 20;
                degats = 3;
                expDrop = 5;
            }
            else
            {
                nom = "orc";
                vie = 40;
                degats = 6;
                expDrop = 10;
            }
        }
        else
        {
            int ennemySpawn = rnd.Next(0, 3);
            if (ennemySpawn == 0)
            {
                nom = "gobelin";
                vie = 20;
                degats = 3;
                expDrop = 5;
            }
            else if (ennemySpawn == 1)
            {
                nom = "orc";
                vie = 40;
                degats = 6;
                expDrop = 10;
            }
            else
            {
                nom = "troll";
                vie = 80;
                degats = 12;
                expDrop = 20;
            }
        }
        lang.Load(config.language);
    }
    public string Infos()
    {
        var variables = new Dictionary<string, string>
        {
            { "nom", nom },
            { "vie", vie.ToString() },
            { "degats", degats.ToString() },
            { "expDrop", expDrop.ToString() },
        };
        string infoText = lang.Get("ennemiStats", variables);
        return infoText;
    }
    public bool enVie()
    {
        return vie > 0;
    }
}
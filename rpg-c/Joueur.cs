public class Joueur
{
    LanguageService lang = new LanguageService();
    Config config = Config.Load();
    string pseudo; // pseudo du joueur
    public int vie; // vie du joueur
    public int force; // degat que fait le joueur
    public int exp;
    public int lvlCapExp;
    public int lvl;
    public int maxVie;
    public int potions;
    public Joueur(string unPseudo, int nbVie, int nbForce, int nbExp, int nbLvlCapExp, int nbLvl, int nbPotion)
    {
        // actualisation des pseudo + vie + degats
        pseudo = unPseudo;
        vie = nbVie;
        maxVie = nbVie;
        force = nbForce;
        exp = nbExp;
        lvlCapExp = nbLvlCapExp;
        lvl = nbLvl;
        potions = nbPotion;
        lang.Load(config.language);
    }

    public string Infos()
    {
        int expAvantLvlUp = lvlCapExp - exp;
        int niveauSuivant = lvl + 1;
        var variables = new Dictionary<string, string>
        {
            { "pseudo", pseudo },
            { "vie", vie.ToString() },
            { "force", force.ToString() },
            { "exp", exp.ToString() },
            { "expRestant", expAvantLvlUp.ToString() },
            { "niveau", niveauSuivant.ToString() }
        };
        string infoText = lang.Get("playerStats", variables);
        return infoText;
    }
    public bool enVie()
    {
        return vie > 0;
    }
    public void GagnerExp(int experience)
    {
        exp += experience;
        if (exp >= lvlCapExp)
        {
            LvlUp();
        }
    }
    public void LvlUp()
    {
        lvl += 1;
        exp -= lvlCapExp ;
        force = force + 1;
        maxVie = maxVie + 2;
        lvlCapExp = lvlCapExp + 10;
    }
}
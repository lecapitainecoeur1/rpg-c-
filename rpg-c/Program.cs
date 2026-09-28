Random rnd = new Random();
LanguageService lang = new LanguageService();
Config config = Config.Load();

Console.Write("Choose language (fr/en): ");
string? choice = Console.ReadLine();
if (string.IsNullOrEmpty(choice)){
    choice = "en";
}

config.language = choice;
config.Save();
lang.Load(config.language ?? "en");
Console.WriteLine(lang.Get("get_pseudo"));
string? pseudoJoueur = Console.ReadLine();
if (string.IsNullOrEmpty(pseudoJoueur))
{
    if (choice == "fr")
        pseudoJoueur = "joueur";
    else if (choice == "en")
        pseudoJoueur = "player";
    else
        pseudoJoueur = "player";
}
Joueur joueur1 = new Joueur(pseudoJoueur, 20, 4, 0, 10, 1); // pseudo; max vie; force; exp; lvlcapexp; lvl;
Combats();
void Combats()
{
    joueur1.vie = joueur1.maxVie;
    Ennemy ennemy = new Ennemy(joueur1, rnd); 

    Console.WriteLine(joueur1.Infos());
    Console.WriteLine(ennemy.Infos());
    bool fuite = false;
    void Exp()
    {
        joueur1.exp = joueur1.exp + ennemy.expDrop;
        if (joueur1.exp >= joueur1.lvlCapExp)
        {
            joueur1.lvl = joueur1.lvl + 1;
            joueur1.exp = 0;
            joueur1.LvlUp();
        }
    }
    void CheckVie()
    {
        if (!joueur1.enVie())
        {
            Console.WriteLine(lang.Get("lose"));
            Restart();
        }
        else if (!ennemy.enVie())
        {
            Console.WriteLine(lang.Get("win"));
            Exp();
            Restart();
        }
    }
    void PlayerTurn()
    {
        var variables = new Dictionary<string, string>
        {
            { "joueur1.vie", joueur1.vie.ToString() },
            { "joueur1.force", joueur1.force.ToString() },
            { "Ennemy.nom", ennemy.nom },
            { "Ennemy.vie", ennemy.vie.ToString() }
        };
        Console.WriteLine(lang.Get("turn", variables));
        int chanceFuite = rnd.Next(0, 2);
        string choixAction = Console.ReadLine();
        if (choixAction == "1")
        {
            ennemy.vie = ennemy.vie - joueur1.force;
            variables = new Dictionary<string, string>
            {
                { "Ennemy.nom", ennemy.nom }
            };
            Console.WriteLine(lang.Get("player_attack", variables));
        }
        else if (chanceFuite == 1)
        {
            fuite = true;
        }
        else
        {
            Console.WriteLine(lang.Get("turn_past"));
        }
        CheckVie();
    }
    void EnnemyTurn()
    {
        joueur1.vie = joueur1.vie - ennemy.degats;
        var variables = new Dictionary<string, string>
        {
            { "Ennemy.nom", ennemy.nom }
        };
        Console.WriteLine(lang.Get("ennemie_attack", variables));
        CheckVie();
    }
    while (joueur1.enVie() && ennemy.enVie() && !fuite)
    {
        PlayerTurn();
        EnnemyTurn();
    }
    void Restart()
    {
        Console.WriteLine(lang.Get("restart"));
        string restart = Console.ReadLine();
        if (restart == "1")
        {
            Combats();
        }
        else
        {
            Environment.Exit(0);
        }
    }
}
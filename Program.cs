Random rnd = new Random();
LanguageService lang = new LanguageService();
Config config = Config.Load();

Console.WriteLine("Choose language (fr/en): ");
string choice = Console.ReadLine();
if (choice == ""){
    choice = "en";
}

config.language = choice;
config.Save();
lang.Load(config.language);
Console.WriteLine(lang.Get("get_pseudo"));
string pseudoJoueur = Console.ReadLine();
if (pseudoJoueur == "")
{
    if (choice == "fr")
        pseudoJoueur = "joueur";
    else if (choice == "en")
        pseudoJoueur = "player";
    else
        Console.WriteLine("langue indisponible");
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
        Console.WriteLine($"C'est a ton tour. Tu as {joueur1.vie} pv");
        Console.WriteLine($"Tu as deux action possible:");
        Console.WriteLine($"Tape 1 pour attaquer fait {joueur1.force} degats");
        Console.Write($"Tape 2 pour fuir ");
        int chanceFuite = rnd.Next(0, 2);
        string choixAction = Console.ReadLine();
        if (choixAction == "1")
        {
            ennemy.vie = ennemy.vie - joueur1.force;
            Console.WriteLine($"Tu as attaque l'ennemy il a maintenant {ennemy.vie}pv");
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
        Console.WriteLine(lang.get("Ennemie_attack"));
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
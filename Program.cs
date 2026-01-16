Random rnd = new Random();
Console.WriteLine("Quel est ton pseudo?");
string pseudoJoueur = Console.ReadLine();

Joueur joueur1 = new Joueur(pseudoJoueur, 20, 4, 0, 10, 1); // pseudo; max vie; force; exp; lvlcapexp; lvl;

Combats();
void Combats()
{
    joueur1.vie = 20;
    Ennemy ennemy = new Ennemy("gobelin", 16, 3, 5); // nom ennemy; vieEnnemy; forceEnnemy; dropExp

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
            Console.WriteLine($"Dommage tu vient de perdre un tour");
        }
    }
    void EnnemyTurn()
    {
        joueur1.vie = joueur1.vie - ennemy.degats;
        Console.WriteLine($"L'ennemi t'a attaque tu as perdu des pv");
        Console.WriteLine($"Tu as {joueur1.vie} pv");
    }
    while (joueur1.enVie() && ennemy.enVie() && !fuite)
    {
        PlayerTurn();
        EnnemyTurn();
    }
    void Restart()
    {
        Console.WriteLine("veux tu recommencer? 1 = oui 2 = non");
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
    if (!joueur1.enVie())
    {
        Console.WriteLine("Tu as perdu");
        Restart();
    }
    else if (!ennemy.enVie())
    {
        Console.WriteLine($"Tu as gagne");
        Exp();
        Restart();
    }
}
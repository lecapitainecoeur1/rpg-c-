
public class Ennemy
{
    string nom;
    public int vie;
    public int degats;
    public int expDrop;
    public Ennemy(string unNom, int nbVie, int nbDegats, int nbExpDrop)
    {
        nom = unNom;
        vie = nbVie;
        degats = nbDegats;
        expDrop = nbExpDrop;
    }
    public Ennemy(Joueur joueur, Random rnd)
    {
        if (joueur.lvl <= 5)
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
                vie = 30;
                degats = 5;
                expDrop = 10;
            }
        }
    }
    public string Infos()
    {
        return "Un " + nom + " est devant toi il a " + vie + " vie et fait " + degats + " degats, si tu le tue tu recevra " + expDrop + " exp";
    }
    public bool enVie()
    {
        return vie > 0;
    }
}
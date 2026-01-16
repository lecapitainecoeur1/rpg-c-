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
    public string Infos()
    {
        return "Un " + nom + " est devant toi il a " + vie + " vie et fait " + degats + " degats, si tu le tue tu recevra " + expDrop + " exp";
    }
    public bool enVie()
    {
        return vie > 0;
    }
}
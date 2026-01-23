public class Joueur
{
    string pseudo; // pseudo du joueur
    public int vie; // vie du joueur
    public int force; // degat que fait le joueur
    public int exp;
    public int lvlCapExp;
    public int lvl;
    public Joueur(string unPseudo, int nbVie, int nbForce, int nbExp, int nbLvlCapExp, int nbLvl)
    {
        // actualisation des pseudo + vie + degats
        pseudo = unPseudo;
        vie = nbVie;
        force = nbForce;
        exp = nbExp;
        lvlCapExp = nbLvlCapExp;
        lvl = nbLvl;
    }

    public string Infos()
    {
        int expAvantLvlUp = lvlCapExp - exp;
        int infoExpAvantLvlUp = lvl + 1;
        return "Bienvenu " + pseudo + ". Tu as " + vie + " vie, tu as " + force + " de force. Tu as " + exp + " exp et il te faut encore " + expAvantLvlUp + " exp pour passer au niveau " + infoExpAvantLvlUp;
    }
    public bool enVie()
    {
        return vie > 0;
    }
    public void LvlUp()
    {
        force = force + 2;
        vie = vie + 5;
        lvlCapExp = lvlCapExp + 10;
    }
}
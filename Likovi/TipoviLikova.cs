using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Likovi", menuName = "Likovi/Izradi lika")]

public class TipoviLikova : ScriptableObject
{
    [SerializeField] string ime;

    [TextArea]
    [SerializeField] string opis;

    [SerializeField] Sprite sprite;

    [SerializeField] Tipovi tip1;
    [SerializeField] Tipovi tip2;

    [SerializeField] int maxHP;
    [SerializeField] int napad;
    [SerializeField] int obrana;
    [SerializeField] int spNapad;
    [SerializeField] int spObrana;
    [SerializeField] int brzina;

    [SerializeField] List<NaučeniPotez> naučeniPotez;

    public string Ime
    {
        get { return ime; }
    }

    public string Opis
    {
        get { return opis; }
    }
    public Sprite Sprite
    {
        get { return sprite; }
    }

    public Tipovi Tip1
    {
        get { return tip1; }
    }
    public Tipovi Tip2
    {
        get { return tip2; }
    }

    public int MaxHP
    {
        get { return maxHP; }
    }
    public int Napad
    {
        get { return napad; }
    }
    public int Obrana
    {
        get { return obrana; }
    }
    public int SpNapad
    {
        get { return spNapad; }
    }
    public int SpObrana
    {
        get { return spObrana; }
    }
    public int Brzina
    {
        get { return brzina; }
    }
    public List<NaučeniPotez> NaučeniPotez
    {
        get { return naučeniPotez; }
    }
}
    [System.Serializable]


    public class NaučeniPotez
{
    [SerializeField] TipoviPoteza tipoviPoteza;
    [SerializeField] int level;

    public TipoviPoteza Baza
    {
        get { return tipoviPoteza; }
    }

    public int Level
    {
        get { return level; }
    }
}

public enum Tipovi
{
    Zombiji,
    Mutanti,
    Specijalci,
    Ljudi,
    Životinje
}

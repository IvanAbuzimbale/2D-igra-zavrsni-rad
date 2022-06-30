using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Likovi", menuName = "Likovi/Izradi potez")]

public class TipoviPoteza : ScriptableObject
{
    [SerializeField] string ime;

    [TextArea]
    [SerializeField] string opis;

    [SerializeField] Tipovi tipovi;
    [SerializeField] int moć;
    [SerializeField] int preciznost;
    [SerializeField] int izdržljivost;

    public string Ime
    {
        get { return ime; }
    }
    public string Opis
    {
        get { return opis; }
    }
    public Tipovi Tipovi
    {
        get { return tipovi; }
    }
    public int Moć
    {
        get { return moć; }
    }
    public int Preciznost
    {
        get { return preciznost; }
    }
    public int Izdržljivost
    {
        get { return izdržljivost; }
    }
}

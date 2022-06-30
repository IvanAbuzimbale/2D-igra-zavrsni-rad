using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potez
{
    public TipoviPoteza Baza { get; set; }

    public int Izdržljivost { get; set; }

    public Potez(TipoviPoteza pBaza)
    {
        Baza = pBaza;
        Izdržljivost = pBaza.Izdržljivost;
    }
}

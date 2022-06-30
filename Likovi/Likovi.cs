using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Likovi
{
    [SerializeField] TipoviLikova _baza;
    [SerializeField] int level;


    public TipoviLikova Baza
    {
        get 
            {
                return _baza;
            }
    }

    public int Level
    {
        get
        {
            return level;
        }
    }

    public int HP { get; set; }

    public List<Potez> Potezi { get; set; }

    public void Init()
    {
        HP = MaxHP;

        // Izradi poteze
        Potezi = new List<Potez>();
        foreach (var potez in Baza.NaučeniPotez)
        {
            if (potez.Level <= Level)
                Potezi.Add(new Potez(potez.Baza));
            if (Potezi.Count >= 4)
                break;
        }
    }

    public int MaxHP
    {
        get { return Mathf.FloorToInt(Baza.MaxHP * Level / 100f) + 10; }
    }
    public int Napad
    {
        get { return Mathf.FloorToInt((Baza.Napad * Level) / 100f) + 5; }
    }
    public int Obrana
    {
        get { return Mathf.FloorToInt((Baza.Obrana * Level) / 100f) + 5; }
    }
    public int SpNapad
    {
        get { return Mathf.FloorToInt((Baza.SpNapad * Level) / 100f) + 5; }
    }
    public int SpObrana
    {
        get { return Mathf.FloorToInt((Baza.SpObrana * Level) / 100f) + 5; }
    }
    public int Brzina
    {
        get { return Mathf.FloorToInt((Baza.Brzina * Level) / 100f) + 5; }
    }
    public bool UzmiStetu(Potez potez, Likovi napadac)
    {
        float modifier = Random.Range(0.85f, 1f);
        float a = (2 * napadac.Level + 10) / 250f;

        float d = a * potez.Baza.Moć * ((float)napadac.Napad / Obrana) + 2;
        int  steta = Mathf.FloorToInt(d * modifier);

        HP -= steta;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }

        return false;
    }

    public Potez DobijNasumičniPotez()
    {
        int r = Random.Range(0, Potezi.Count);
        return Potezi[r];
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Text imeTekst;
    [SerializeField] Text levelTekst;
    [SerializeField] HPBar hpBar;

    Likovi _likovi;

    public void PostaviPodatke(Likovi likovi)
    {
        _likovi = likovi;
        imeTekst.text = likovi.Baza.Ime;
        levelTekst.text = "Lvl " + likovi.Level;
        hpBar.PostaviHP((float)likovi.HP / likovi.MaxHP);
    }

    public IEnumerator AzurirajHP()
    {
        yield return hpBar.PostaviGlatkiHPBar((float)_likovi.HP / _likovi.MaxHP);
    }

}

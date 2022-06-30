using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusBorbe { Start, IgracevaAkcija, IgracevPotez, ProtivnikovPotez, Zauzet}

public class SustavBorbe : MonoBehaviour
{
    [SerializeField] BorbeniLik igračevLik;
    [SerializeField] BorbeniLik protivnikovLik;
    [SerializeField] HUD igračevHUD;
    [SerializeField] HUD protivnikovHUD;
    [SerializeField] DialogBoxBorba dialogBox;

    public event Action<bool> gotovaBorba;

    StatusBorbe status;
    int trenutnaAkcija;
    int trenutniPotez;

    SkupinaLikova skupinaLikova;
    Likovi neprijatelj;

    public void ZapočniBorbu(SkupinaLikova skupinaLikova, Likovi neprijatelj)
    {
        this.skupinaLikova = skupinaLikova;
        this.neprijatelj = neprijatelj;
       StartCoroutine(PokreniBorbu());
    }

    public IEnumerator PokreniBorbu()
    {
        igračevLik.Setup(skupinaLikova.OsvježeniLik());
        protivnikovLik.Setup(neprijatelj);
        igračevHUD.PostaviPodatke(igračevLik.Likovi);
        protivnikovHUD.PostaviPodatke(protivnikovLik.Likovi);

        dialogBox.PostaviNazivePoteza(igračevLik.Likovi.Potezi);

        yield return dialogBox.PisiDialog($"Divlji {protivnikovLik.Likovi.Baza.Ime} se pojavio");

        yield return new WaitForSeconds(1f);

        IgracevaAkcija();
    }

    internal void ZapočniBorbu()
    {
        throw new NotImplementedException();
    }

    void IgracevaAkcija()
    {
        status = StatusBorbe.IgracevaAkcija;
        StartCoroutine(dialogBox.PisiDialog("Odaberi akciju"));
        dialogBox.OmoguciOdabirAkcije(enabled);
    }

    void IgracevPotez()
    {
        status = StatusBorbe.IgracevPotez;
        dialogBox.OmoguciDialogText(false);
        dialogBox.OmoguciOdabirAkcije(false);
        dialogBox.OmoguciOdabirPoteza(true);
    }

    IEnumerator IzvrsiIgracevPotez()
    {
        status = StatusBorbe.Zauzet;
        var potez = igračevLik.Likovi.Potezi[trenutniPotez];
        potez.Izdržljivost--;
        yield return dialogBox.PisiDialog($"{igračevLik.Likovi.Baza.Ime} je iskoristio {potez.Baza.Ime}");
        igračevLik.AnimacijaNapada();
        yield return new WaitForSeconds(1f);

        protivnikovLik.AnimacijaUdarca();

        bool jeUmro = protivnikovLik.Likovi.UzmiStetu(potez, igračevLik.Likovi);
        yield return protivnikovHUD.AzurirajHP();

        if (jeUmro)
        {
            yield return dialogBox.PisiDialog($"{protivnikovLik.Likovi.Baza.Ime} umire");
            protivnikovLik.AnimacijaUmire();

            yield return new WaitForSeconds(2f);
            gotovaBorba(true);
        }
        else
        {
            StartCoroutine(ProtivnikovPotez());
        }
    }

    IEnumerator ProtivnikovPotez()
    {
        status = StatusBorbe.ProtivnikovPotez;

        var potez = protivnikovLik.Likovi.DobijNasumičniPotez();
        potez.Izdržljivost--;

        yield return dialogBox.PisiDialog($"{protivnikovLik.Likovi.Baza.Ime} je iskoristio {potez.Baza.Ime}");
        protivnikovLik.AnimacijaNapada();
        yield return new WaitForSeconds(1f);

        igračevLik.AnimacijaUdarca();

        bool jeUmro = igračevLik.Likovi.UzmiStetu(potez, igračevLik.Likovi);
        yield return igračevHUD.AzurirajHP();

        if (jeUmro)
        {
            yield return dialogBox.PisiDialog($"{igračevLik.Likovi.Baza.Ime} umire");
            igračevLik.AnimacijaUmire();

            yield return new WaitForSeconds(2f);
            gotovaBorba(false);
        }
        else
        {
            IgracevaAkcija();
        }
    }

    public void RučniUpdate()
    {
        if (status == StatusBorbe.IgracevaAkcija)
        {
            UpravljajOdabiruAkcija();
        }
        else if (status == StatusBorbe.IgracevPotez)
        {
            UpravljajOdabiruPoteza();
        }
    }

    void UpravljajOdabiruAkcija()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (trenutnaAkcija < 1)
                trenutnaAkcija++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (trenutnaAkcija > 0)
                trenutnaAkcija--;
        }

        dialogBox.AzurirajOdabirAkcije(trenutnaAkcija);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (trenutnaAkcija == 0)
            {
                // Bori se
                IgracevPotez();
            }
            else if (trenutnaAkcija == 1)
            {
                // Bježi
            }
        }
    }

    void UpravljajOdabiruPoteza()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (trenutniPotez < igračevLik.Likovi.Potezi.Count - 1)
                trenutniPotez++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (trenutniPotez > 0)
                trenutniPotez--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (trenutniPotez < igračevLik.Likovi.Potezi.Count - 2)
                trenutniPotez+=2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (trenutniPotez > 1)
                trenutniPotez-=2;
        }

        dialogBox.AzurirajOdabirPoteza(trenutniPotez, igračevLik.Likovi.Potezi[trenutniPotez]);

        if (Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.OmoguciOdabirPoteza(false);
            dialogBox.OmoguciDialogText(true);
            StartCoroutine(IzvrsiIgracevPotez());
        }
    }
}

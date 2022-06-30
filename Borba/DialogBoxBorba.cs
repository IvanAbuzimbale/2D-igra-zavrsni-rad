using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxBorba : MonoBehaviour
{
    [SerializeField] int slovaPoSekundi;
    [SerializeField] Color istaknutaBoja;

    [SerializeField] Text dialogText;
    [SerializeField] GameObject odabirAkcije;
    [SerializeField] GameObject odabirPoteza;
    [SerializeField] GameObject detaljiPoteza;

    [SerializeField] List<Text> akcijaText;
    [SerializeField] List<Text> potezText;

    [SerializeField] Text izdrzljivostText;
    [SerializeField] Text tipText;

    public void PostaviDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator PisiDialog(string dialog)
    {
        dialogText.text = "";
        foreach (var slovo in dialog.ToCharArray())
        {
            dialogText.text += slovo;
            yield return new WaitForSeconds(1f / slovaPoSekundi);
        }
    }

    public void OmoguciDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }
    
    public void OmoguciOdabirAkcije(bool enabled)
    {
        odabirAkcije.SetActive(enabled);
    }

    public void OmoguciOdabirPoteza(bool enabled)
    {
        odabirPoteza.SetActive(enabled);
        detaljiPoteza.SetActive(enabled);
    }

    public void AzurirajOdabirAkcije(int odabranaAkcija)
    {
        for (int i = 0; i < akcijaText.Count; i++)
        {
            if (i == odabranaAkcija)
                akcijaText[i].color = istaknutaBoja;
            else
                akcijaText[i].color = Color.black;
        }
    }

    public void AzurirajOdabirPoteza(int odabraniPotez, Potez potez)
    {
        for (int i = 0; i < potezText.Count; i++)
        {
            if (i == odabraniPotez)
            {
                potezText[i].color = istaknutaBoja;
            }
            else
            {
                potezText[i].color = Color.black;
            }
        }

        izdrzljivostText.text = $"Izdržljivost {potez.Izdržljivost}/{potez.Baza.Izdržljivost}";
        tipText.text = potez.Baza.Tipovi.ToString();
    }

    public void PostaviNazivePoteza(List<Potez> potezi)
    {
        for (int i = 0; i < potezText.Count; i++)
        {
            if (i < potezi.Count)
            {
                potezText[i].text = potezi[i].Baza.Ime;
            }
            else
            {
                potezText[i].text = "-";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BorbeniLik : MonoBehaviour
{
    [SerializeField] bool daLiJeIgrač;

    public Likovi Likovi { get; set; }

    public void Setup(Likovi lik)
    {
        Likovi = lik;
        if (daLiJeIgrač)
            GetComponent<Image>().sprite = Likovi.Baza.Sprite;
        else
            GetComponent<Image>().sprite = Likovi.Baza.Sprite;

        GetComponent<Image>().DOFade(1f, 0f);
        AnimacijaUlazakUBorbu();
    }

    public void AnimacijaUlazakUBorbu()
    {
        if (daLiJeIgrač == true)
            GetComponent<Image>().transform.localPosition = new Vector3(-500f, -50f);
        else
            GetComponent<Image>().transform.localPosition = new Vector3(500f, 131f);
        if (daLiJeIgrač == true)
            GetComponent<Image>().transform.DOLocalMoveX(-216f, 1f);
        else
            GetComponent<Image>().transform.DOLocalMoveX(217f, 1f);
    }

    public void AnimacijaNapada()
    {
        var sekvenca = DOTween.Sequence();
        if (daLiJeIgrač == true)
        {
           sekvenca.Append(GetComponent<Image>().transform.DOLocalMoveX(+50f, 0.25f));
            GetComponent<Image>().transform.DOLocalMoveX(-216f, 0.25f);
        }

        else
        {
            sekvenca.Append(GetComponent<Image>().transform.DOLocalMoveX(-50f, 0.25f));
            GetComponent<Image>().transform.DOLocalMoveX(217f, 0.25f);
        }

    }

    public void AnimacijaUdarca()
    {
        var sekvenca = DOTween.Sequence();
        sekvenca.Append(GetComponent<Image>().DOColor(Color.gray, 0.1f));
        sekvenca.Append(GetComponent<Image>().DOColor(GetComponent<Image>().color, 0.1f));
    }

    public void AnimacijaUmire()
    {
        var sekvenca = DOTween.Sequence();
        sekvenca.Append(GetComponent<Image>().transform.DOLocalMoveY(-150f, 0.5f));
        sekvenca.Join(GetComponent<Image>().DOFade(0f, 0.5f));
    }
}

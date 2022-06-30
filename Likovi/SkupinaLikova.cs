using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkupinaLikova : MonoBehaviour
{
    [SerializeField] List<Likovi> lik;

    public void Start()
    {
        foreach (var likovi in lik)
        {
            likovi.Init();
        }
    }

    public Likovi OsvježeniLik()
    {
        return lik.FirstOrDefault(x => x.HP > 0);
    }
}

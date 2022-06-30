using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodrucjeMape : MonoBehaviour
{
    [SerializeField] List<Likovi> neprijatelji;

    public Likovi NasumicniNeprijatelj()
    {
        var neprijatelj = neprijatelji[Random.Range(0, neprijatelji.Count)];
        neprijatelj.Init();
        return neprijatelj;
    }
}
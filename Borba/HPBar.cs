using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject zdravlje;

    public void PostaviHP(float hpNormal)
    {
        zdravlje.transform.localScale = new Vector3(hpNormal, 1f);
    }

    public IEnumerator PostaviGlatkiHPBar(float noviHP)
    {
        float trenutniHP = zdravlje.transform.localScale.x;
        float promijeniVrijednost = trenutniHP - noviHP;

        while (trenutniHP - noviHP > Mathf.Epsilon)
        {
            trenutniHP -= promijeniVrijednost * Time.deltaTime;
            zdravlje.transform.localScale = new Vector3(trenutniHP, 1f);
            yield return null;
        }
        zdravlje.transform.localScale = new Vector3(noviHP, 1f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KontrolaIgraca : MonoBehaviour
{
    public float brzinaIgraca;
    public LayerMask objekti;
    public LayerMask trava;

    public event Action napad;

    public bool kreceSe;
    private Vector2 input;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void RučniUpdate()
    {
        if (!kreceSe)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                input.y = 0;
            }

            if (input != Vector2.zero)
            {
                animator.SetFloat("KretnjaX", input.x);
                animator.SetFloat("KretnjaY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (Collider(targetPos))
                    StartCoroutine(Kretnja(targetPos));
            }
        }

        animator.SetBool("kreceLiSe", kreceSe);

        IEnumerator Kretnja(Vector3 targetPos)
        {
            kreceSe = true;
            while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, brzinaIgraca * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPos;

            kreceSe = false;

            ProvjeraBorbe();
        }
    }

    private bool Collider(Vector3 targetPos)
     {
       if (Physics2D.OverlapCircle(targetPos, 0.3f, objekti) != null)
            {
                return false;
            }
            return true;
    }

    private void ProvjeraBorbe()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, trava) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                animator.SetBool("kreceLiSe", false);
                napad();
            }
        }
    }
}
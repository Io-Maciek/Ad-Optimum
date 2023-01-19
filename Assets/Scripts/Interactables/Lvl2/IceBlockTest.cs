using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class IceBlockTest : Activatable
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override Result<object, object> SetTo(bool setValue, params object[] args)
    {
        if (setValue)
        {
            animator.SetBool("BlockEnter", true);
        }

        return Result<object>.Ok();
    }

}

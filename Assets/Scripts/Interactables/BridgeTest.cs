using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTest : Activatable
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override Result<object, object> SetTo(bool setValue, params object[] args)
    {
        if (!setValue)
        {
            animator.SetBool("isDown", true);
        }
        else
        {
            animator.SetBool("isDown", false);
        }

        return Result<object>.Ok();
    }
}

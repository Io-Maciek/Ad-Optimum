using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTest : Activatable
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override Result<string> SetTo(bool setValue, params object[] args)
    {
        if (!setValue)
        {
            
        }
        else
        {
            animator.Play("bridgeUp");
        }

        return Result<string>.Ok();
    }
}

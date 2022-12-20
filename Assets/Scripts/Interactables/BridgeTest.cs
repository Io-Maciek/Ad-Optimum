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
        //Debug.Log("Set to: " + setValue);
        if (!setValue)
        {
            animator.SetBool("isDown", true);
        }
        else
        {
            animator.SetBool("isDown", false);
        }

        return Result<string>.Ok();
    }
}

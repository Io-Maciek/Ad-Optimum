using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTest : Activatable
{
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }
    public override Result<string> SetTo(bool setValue, params object[] args)
    {
        if (!setValue)
        {
            transform.position = startPos;
        }
        else
        {
            transform.position += Vector3.up * 5;
        }

        return Result<string>.Ok();
    }
}

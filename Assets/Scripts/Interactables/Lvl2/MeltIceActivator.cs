using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class MeltIceActivator : Activator
{
    public bool WasActivated { get; private set; } = false;


    private void OnTriggerEnter(Collider other)
    {

        if (!WasActivated && other.GetComponent<Pochodnia>().IamFuming)
        {
            WasActivated = true;
            GetComponent<Animator>().SetBool("Melt", true);
        }

    }
}

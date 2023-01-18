using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class BlockingEnter : Activator
{
    public bool WasActivated { get; private set; } = false;



    private void OnTriggerEnter(Collider other)
    {
        if (!WasActivated && other.tag == "Player")
        {
            //GetComponent<Animator>().SetBool("isActivated", true);
            WasActivated = true;
            //GetComponent<AudioSource>().Play();
            LoveConnections[0].SetTo(true);
            LoveConnections[1].SetTo(true);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (IsActive && (other.tag == "Player" || other.GetComponent<BetterHolding>()))
    //    {
    //        GetComponent<Animator>().SetBool("isActivated", false);
    //        IsActive = false;
    //        LoveConnections[0].SetTo(false);
    //    }
    //}
}

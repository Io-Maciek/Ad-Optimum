using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : Activator
{
    public bool IsActive { get; private set; } = false;



    private void OnTriggerStay(Collider other)
    {
        if (!IsActive && (other.tag == "Player" || other.GetComponent<Holding>()))
        {
            GetComponent<Animator>().SetBool("isActivated", true);
            IsActive = true;
            LoveConnections[0].SetTo(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsActive && (other.tag == "Player" || other.GetComponent<Holding>()))
        {
            GetComponent<Animator>().SetBool("isActivated", false);
            IsActive = false;
            LoveConnections[0].SetTo(false);
        }
    }
}

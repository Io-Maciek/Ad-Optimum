using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButtonClick : Activator
{
    public bool IsActive { get; private set; } = false;
    public GameObject otherBtn;

    private void OnTriggerStay(Collider other)
    {
        if (!IsActive && (other.tag == "Player" || other.GetComponent<Holding>()))
        {
            GetComponent<Animator>().SetBool("isActivated", true);
            IsActive = true;
            GetComponent<AudioSource>().Play();
            if (otherBtn.GetComponent<Animator>().GetBool("isActivated"))
            {
                LoveConnections[0].SetTo(true);
            }
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

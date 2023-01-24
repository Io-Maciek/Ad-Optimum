using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class wstalesNarr : NarratorVoiceController
{
    public GameObject MyszkaObj;
    bool czyOdtworzono = false;


    public override void Start()
    {
        base.Start();
        if (_planszaInfo.ProgressValue == 0)
        {
            FindObjectOfType<PauzerScript>().enabled = false;
            var p = FindObjectOfType<Controller>();
            p.mouseMovement.enabled = false;
            p.movement.enabled = false;
        }
    }
    void Update()
    {
        if (!czyOdtworzono)
        {
            if (_planszaInfo.ProgressValue == 0)
            {
                Play();
                czyOdtworzono = true;
                StartCoroutine("wait");
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(Narracja.length - 4.5f);
        MyszkaObj.SetActive(true);
        var p = FindObjectOfType<Controller>();
        p.mouseMovement.enabled = true;
        p.movement.enabled = true;
        FindObjectOfType<PauzerScript>().enabled = true;
    }
}

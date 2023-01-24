using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class triggerDialog : NarratorVoiceController
{
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class wstalesNarr : NarratorVoiceController
{
    bool czyOdtworzono = false;
    void Update()
    {
        if (!czyOdtworzono)
        {
            if (_planszaInfo.ProgressValue == 0) { 
            Debug.Log(_planszaInfo.KontrolerAudio);
            Play();
            czyOdtworzono = true;
        }
    }
    }
}

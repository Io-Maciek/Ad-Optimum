using Assets.Scripts.Logic.GameSaves;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanszaInfo : MonoBehaviour
{
    public uint ProgressValue { get; private set; } = 0;
    public List<SavingTrigger> EYE_OF_THE_TRIGGER; 

    void Start()
    {
        SavingTrigger last_seen;
        try
        {
            if (ApplicationModelInfo.GameSave.ProgressValue == 0)
            {
                last_seen = null;
            }
            else
            {
                last_seen = EYE_OF_THE_TRIGGER.Where(x => x.ProgressValue == ApplicationModelInfo.GameSave.ProgressValue).First();
            }
        }
        catch (NullReferenceException)
        {
            ApplicationModelInfo.GameSave = new GameSave() { id = 999, ProgressValue = 0, SayMyName = "save_debug.io" };
            if (ApplicationModelInfo.GameSave.ProgressValue == 0)
            {
                last_seen = null;
            }
            else
            {
                last_seen = EYE_OF_THE_TRIGGER.Where(x => x.ProgressValue == ApplicationModelInfo.GameSave.ProgressValue).First();
            }
        }

        if (last_seen == null)
        {
            ApplicationModelInfo.GameSave.SceneID = (uint)SceneManager.GetActiveScene().buildIndex;
            ProgressValue = ApplicationModelInfo.GameSave.ProgressValue;
        }
        else
        {
            ApplicationModelInfo.GameSave.SceneID = (uint)SceneManager.GetActiveScene().buildIndex;
            ProgressValue = ApplicationModelInfo.GameSave.ProgressValue;
            GameObject.Find("Player").transform.position = last_seen.tp_home;
        }
        ApplicationModelInfo.GameSave.Save();
    }

    void Update()
    {
        
    }

    public void UpdateProgress(uint NewProgressValue)
    {
        if (NewProgressValue > ProgressValue)
        {
            ProgressValue = NewProgressValue;
            ApplicationModelInfo.GameSave.ProgressValue = ProgressValue;
            ApplicationModelInfo.GameSave.Save();
            Debug.Log($"Updated to {ProgressValue}");
        }
        else
        {
            //Debug.Log($"Progress not updated");
        }
    }
}

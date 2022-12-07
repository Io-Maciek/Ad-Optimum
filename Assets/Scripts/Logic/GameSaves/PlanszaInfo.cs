using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanszaInfo : MonoBehaviour
{
    public uint ProgressValue { get; private set; } = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateProgress(uint NewProgressValue)
    {
        if (NewProgressValue > ProgressValue)
        {
            ProgressValue = NewProgressValue;
            //Debug.Log($"Updated to {ProgressValue}");
        }
        else
        {
            //Debug.Log($"Progress not updated");
        }
    }
}

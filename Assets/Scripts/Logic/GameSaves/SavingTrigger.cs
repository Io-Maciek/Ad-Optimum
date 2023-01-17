using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Blue arrow in Unity Editor show where player will look after loading.
/// </summary>
public class SavingTrigger : MonoBehaviour
{
    public Vector3 tp_home { get; private set; }
    public Transform TpTransform;

    public uint ProgressValue = 0;

    public PlanszaInfo PlanszaInfo { get; private set; }

    private void Awake()
    {
        if (ProgressValue == 0)
        {
            Debug.LogWarning($"Field 'ProgressValue' for trigger '{gameObject.name}' is not set!");
        }


        PlanszaInfo = FindObjectOfType<PlanszaInfo>();
        if (PlanszaInfo == null)
        {
            Debug.LogError("Object 'PlanszaInfo' is not on this scene!");
        }


        if (TpTransform == null)
        {
            tp_home = transform.position;
        }
        else
        {
            tp_home = TpTransform.position;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlanszaInfo.UpdateProgress(ProgressValue);
        }
    }
}

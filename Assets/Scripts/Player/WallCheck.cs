using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool DotykaSciany { get; private set; } = false;
    private void OnTriggerStay(Collider other)
    {
        if (!DotykaSciany)
        {
            DotykaSciany = true;
            Debug.Log("ZACZYAM DOTYKAÆ");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (DotykaSciany)
        {
            DotykaSciany = false;
            Debug.Log("PRZESTAJE DOTYKAÆ");
        }
    }
}

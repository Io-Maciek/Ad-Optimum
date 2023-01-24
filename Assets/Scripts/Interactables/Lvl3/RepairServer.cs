using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairServer : MonoBehaviour
{
    GameObject server;
    public Material fixedMaterial;
    bool isRepair = false;

    private void Start()
    {
        server = this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isRepair && other.name == "tablet (1)")
        {
            server.GetComponent<MeshRenderer>().material = fixedMaterial;
            isRepair = true;
        }
    }

}

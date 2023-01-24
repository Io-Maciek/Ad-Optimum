using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDesk : MonoBehaviour
{
    public int ileNaprawione = 0;
    public GameObject desk;

    private void Update()
    {
        if(ileNaprawione == 11)
        {
            desk.GetComponent<MonitorOstatniSit>().HaveEnergy = true;
        }
    }
}

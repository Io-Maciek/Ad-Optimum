using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    Transform middle;
    Transform left;
    Transform right;

    public GameObject ekranDef;

    void Awake()
    {
        middle = transform.Find("main");
        left = transform.Find("left");
        right = transform.Find("right");

        var m = Instantiate(ekranDef, middle);
        //var r = Instantiate(ekranDef, right);
        //var l = Instantiate(ekranDef, left);
    }
}

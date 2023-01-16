using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject crosshair { get; private set; }
    void Awake()
    {
        crosshair = transform.Find("CrossHair").gameObject;
    }
}

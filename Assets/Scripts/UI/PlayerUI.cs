using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject crosshair { get; private set; }
    Text narracjaText;

    void Awake()
    {
        crosshair = transform.Find("CrossHair").gameObject;
        narracjaText = transform.Find("NarracjaText").GetComponent<Text>();
    }

    public void SetText(string text)
    {
        narracjaText.text = text;
    }

    public void SetOff()
    {
        narracjaText.text = "";
    }

}

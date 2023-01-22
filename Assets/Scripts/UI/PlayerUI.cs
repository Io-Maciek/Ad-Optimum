using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject crosshair { get; private set; }
    Text narracjaText;
    GameObject textParent;

    void Awake()
    {
        crosshair = transform.Find("CrossHair").gameObject;
        textParent = transform.Find("TextBg").gameObject;
        narracjaText = textParent.GetComponentInChildren<Text>();
        textParent.SetActive(false);
    }

    public void SetText(string text)
    {
        narracjaText.text = text;
        textParent.SetActive(true);
    }

    public void SetOff()
    {
        narracjaText.text = "";
        textParent.SetActive(false);
    }

}

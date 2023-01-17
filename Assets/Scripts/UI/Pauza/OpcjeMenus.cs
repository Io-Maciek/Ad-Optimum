using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcjeMenus : MonoBehaviour
{
    Slider muzykaSlider;
    Controller muzykaAudio;

    public GameObject poprzedniEkran { get; set; }
    void setFromMap()
    {
        muzykaAudio = FindObjectOfType<Controller>();
        Debug.Log(muzykaAudio);
    }

    void Awake()
    {
        muzykaSlider = transform.Find("muzykaSlider").GetComponent<Slider>();
        muzykaSlider.onValueChanged.AddListener(muzykaChange);
        muzykaSlider.value = PlayerPrefs.GetFloat("muzyka", 1.0f);
        setFromMap();

        transform.Find("btnGoBack").GetComponent<Button>().onClick.AddListener(goBack);
    }



    void goBack()
    {
        poprzedniEkran.SetActive(true);
        Destroy(gameObject);
    }

    void muzykaChange(float newVlue)
    {
        PlayerPrefs.SetFloat("muzyka", newVlue);
        if(muzykaAudio != null)
            muzykaAudio.MusicSet(newVlue);
    }
}

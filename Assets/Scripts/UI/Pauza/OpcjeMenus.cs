using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcjeMenus : MonoBehaviour
{
    Slider muzykaSlider;
    Slider fxSlider;
    Controller muzykaAudio;
    FX[] fxMusic;

    public GameObject poprzedniEkran { get; set; }
    void setFromMap()
    {
        muzykaAudio = FindObjectOfType<Controller>();
    }

    void Awake()
    {
        muzykaSlider = transform.Find("muzykaSlider").GetComponent<Slider>();
        muzykaSlider.onValueChanged.AddListener(muzykaChange);
        muzykaSlider.value = PlayerPrefs.GetFloat("muzyka", 1.0f);
        setFromMap();

        transform.Find("btnGoBack").GetComponent<Button>().onClick.AddListener(goBack);
        fxSlider = transform.Find("fxSlider").GetComponent<Slider>();
        fxSlider.onValueChanged.AddListener(fxChange);
        fxSlider.value = PlayerPrefs.GetFloat("fx", 1.0f);
        fxMusic = FindObjectsOfType<FX>();

        foreach (var fx in fxMusic)
        {
            fx.Set(fxSlider.value);
        }
    }


    void fxChange(float nValue)
    {
        PlayerPrefs.SetFloat("fx", nValue);
        foreach (var fx in FindObjectsOfType<FX>())
        {
            fx.Set(nValue);
        }
    }


    void goBack()
    {
        poprzedniEkran.SetActive(true);
        Destroy(gameObject);
    }

    void muzykaChange(float newVlue)
    {
        PlayerPrefs.SetFloat("muzyka", newVlue);
        if (muzykaAudio != null)
            muzykaAudio.MusicSet(newVlue);
        else
        {
            FindObjectOfType<AudioSource>().volume = 1.0f * newVlue;
        }
    }
}

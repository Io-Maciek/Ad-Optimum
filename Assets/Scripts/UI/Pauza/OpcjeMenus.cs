using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpcjeMenus : MonoBehaviour
{
    Slider muzykaSlider;
    Slider fxSlider;
    Slider narrator;
    Controller muzykaAudio;
    FX[] fxMusic;
    Toggle minimizeScreen;
    Dropdown dropdownResolution;

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

        EventSystem.current.SetSelectedGameObject(transform.Find("btnGoBack").gameObject);

        transform.Find("btnGoBack").GetComponent<Button>().onClick.AddListener(goBack);
        fxSlider = transform.Find("fxSlider").GetComponent<Slider>();
        fxSlider.onValueChanged.AddListener(fxChange);
        fxSlider.value = PlayerPrefs.GetFloat("fx", 1.0f);
        fxMusic = FindObjectsOfType<FX>();

        foreach (var fx in fxMusic)
        {
            fx.Set(fxSlider.value);
        }


        narrator = transform.Find("narratorSlider").GetComponent<Slider>();
        narrator.onValueChanged.AddListener(narratorChanged);
        narrator.value = PlayerPrefs.GetFloat("narrator", 1.0f);
        foreach (var item in FindObjectsOfType<NarratorVoice>())
        {
            item.Set(narrator.value);
        }


        transform.Find("btnPrzywroc").GetComponent<Button>().onClick.AddListener(domyslne);


        minimizeScreen = transform.Find("toggleFullscreen").GetComponent<Toggle>();
        minimizeScreen.isOn = PlayerPrefs.GetInt("fullscreen", 1) == 0 ? true : false;
        Screen.fullScreenMode = minimizeScreen.isOn ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;
        minimizeScreen.onValueChanged.AddListener(minimizeChanged);


        dropdownResolution = transform.Find("dropdownResolution").GetComponent<Dropdown>();
        foreach (var _res in Screen.resolutions.OrderByDescending(r => r.width).ThenByDescending(r => r.height).ThenByDescending(r => r.refreshRate))
        {
            Dropdown.OptionData option = new Dropdown.OptionData($"{_res.width} x {_res.height} {_res.refreshRate}FPS");
            dropdownResolution.options.Add(option);
        }
        int indexRes = PlayerPrefs.GetInt("resolution", 0);
        if (indexRes > dropdownResolution.options.Count)
            indexRes = 0;
        string[] resSetter = dropdownResolution.options[indexRes].text.Split(' ');
        int w = int.Parse(resSetter[0]);
        int h = int.Parse(resSetter[2]);
        int fps = int.Parse(resSetter[3].Substring(0, resSetter[3].IndexOf("FPS")));
        Screen.SetResolution(w, h,!minimizeScreen.isOn, fps);
        dropdownResolution.onValueChanged.AddListener(resolutionChange);
        dropdownResolution.value = indexRes;

    }

    void narratorChanged(float newVal)
    {
        PlayerPrefs.SetFloat("narrator", newVal);
        foreach (var item in FindObjectsOfType<NarratorVoice>())
        {
            item.Set(newVal);
        }
    }


    void resolutionChange(int index)
    {
        PlayerPrefs.SetInt("resolution", index);
        string[] resSetter = dropdownResolution.options[index].text.Split(' ');

        int w = int.Parse(resSetter[0]);
        int h = int.Parse(resSetter[2]);
        int fps = int.Parse(resSetter[3].Substring(0, resSetter[3].IndexOf("FPS")));
        Screen.SetResolution(w, h, !minimizeScreen.isOn, fps);
        dropdownResolution.onValueChanged.AddListener(resolutionChange);
    }


    void minimizeChanged(bool isOn)
    {
        PlayerPrefs.SetInt("fullscreen", isOn ? 0 : 1);
        Screen.fullScreenMode = isOn ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;
    }

    void domyslne()
    {
        muzykaChange(1f);
        muzykaSlider.value = 1.0f;

        fxChange(1f);
        fxSlider.value = 1.0f;

        minimizeChanged(false);
        minimizeScreen.isOn = false;

        resolutionChange(0);
        dropdownResolution.value = 0;

        narratorChanged(1.0f);
        narrator.value = 1.0f;
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
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);

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

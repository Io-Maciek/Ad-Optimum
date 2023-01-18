using Assets.Scripts.EasterEggs;
using Assets.Scripts.Logic.GameSaves;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanszaInfo : MonoBehaviour
{
    public uint ProgressValue { get; private set; } = 0;
    public List<SavingTrigger> EYE_OF_THE_TRIGGER; 

    public AudioSource KontrolerAudio { get; private set; }
    Controller p_controller;

    void Start()
    {
        KontrolerAudio = GetComponent<AudioSource>();
        SavingTrigger last_seen;
        try
        {
            if (ApplicationModelInfo.GameSave.ProgressValue == 0)
            {
                last_seen = null;
            }
            else
            {
                last_seen = EYE_OF_THE_TRIGGER.Where(x => x.ProgressValue == ApplicationModelInfo.GameSave.ProgressValue).First();
            }
        }
        catch (NullReferenceException)
        {
            ApplicationModelInfo.GameSave = new GameSave() { id = 999, ProgressValue = 0, SayMyName = "save_debug.io", SecretNumber = new bool[Sekret.ILOSC_SEKRETOW] };
            if (ApplicationModelInfo.GameSave.ProgressValue == 0)
            {
                last_seen = null;
            }
            else
            {
                last_seen = EYE_OF_THE_TRIGGER.Where(x => x.ProgressValue == ApplicationModelInfo.GameSave.ProgressValue).First();
            }
        }

        ApplicationModelInfo.GameSave.SceneID = (uint)SceneManager.GetActiveScene().buildIndex;
        ProgressValue = ApplicationModelInfo.GameSave.ProgressValue;
        if (last_seen != null)
        {
            GameObject.Find("Player").transform.position = last_seen.tp_home;
            GameObject.Find("Player").GetComponent<Controller>().CameraSet(last_seen.transform.localEulerAngles.y - 90);
        }
        ApplicationModelInfo.GameSave.Save();

        Sekret[] secrets = FindObjectsOfType<Sekret>();
        foreach (Sekret secret in secrets)
        {
            var wyczytano = ApplicationModelInfo.GameSave.SecretNumber[secret.sekret_id];
            if (wyczytano)
            {
                secret.seen = wyczytano;
                secret.Close();
            }

        }




        p_controller = FindObjectOfType<Controller>();
        p_controller.MusicSet(PlayerPrefs.GetFloat("muzyka", 1.0f));
        float fxVolue = PlayerPrefs.GetFloat("fx", 1.0f);
        foreach (var fx in FindObjectsOfType<FX>())
        {
            fx.Set(fxVolue);
        }

        bool isOn = PlayerPrefs.GetInt("fullscreen", 1) == 0;
        Screen.fullScreenMode = isOn? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;

        var resols = Screen.resolutions.OrderByDescending(r => r.width).ThenByDescending(r => r.height).ThenByDescending(r=>r.refreshRate).ToArray();
        int indexRes = PlayerPrefs.GetInt("resolution", 0);
        if (indexRes > resols.Length)
            indexRes = 0;
        var res = resols[indexRes];
        Screen.SetResolution(res.width, res.height, !isOn, res.refreshRate);
    }

    void Update()
    {
        
    }

    public void UpdateProgress(uint NewProgressValue)
    {
        if (NewProgressValue > ProgressValue)
        {
            ProgressValue = NewProgressValue;
            ApplicationModelInfo.GameSave.ProgressValue = ProgressValue;
            ApplicationModelInfo.GameSave.Save();
            Debug.Log($"Updated to {ProgressValue}");
            if (!FindObjectOfType<Controller>().playerUI.transform.Find("Loading").gameObject.activeSelf)
                StartCoroutine("_loadAnim");
        }
        else
        {
            ApplicationModelInfo.GameSave.Save();
            Debug.Log($"Progress just saved");
        }
    }

    IEnumerator _loadAnim()
    {
        FindObjectOfType<Controller>().playerUI.transform.Find("Loading").gameObject.SetActive(true);
        yield return new WaitForSeconds(3.37f);
        FindObjectOfType<Controller>().playerUI.transform.Find("Loading").gameObject.SetActive(false);
    }



    public void ChangeAudio(AudioClip newAudio)
    {
        KontrolerAudio.clip = newAudio;
        KontrolerAudio.Play();
    }

}

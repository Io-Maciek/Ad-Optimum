using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monitor : MonoBehaviour
{
    Transform middle;
    Transform left;
    Transform right;


    public bool debugSkip = false;
    public GameObject StartUpEkran;
    public GameObject LoginEkran;
    public List<Texture2D> bootImages;

    public GameObject sekretEndScreen;
    public List<Texture2D> secretBootImages;
    public AudioClip endingEpicMusic;

    public TextMesh[] textOdliczanie;


    public List<Texture2D> loginImages;

    void Awake()
    {
        middle = transform.Find("main");
        left = transform.Find("left");
        right = transform.Find("right");
    }

    GameObject ekranMiddle;
    public IEnumerator Init()
    {
        if (!debugSkip)
        {
            yield return new WaitForSeconds(1.25f);
            ekranMiddle = Instantiate(StartUpEkran, middle);


            foreach (var texture in bootImages.GetRange(0, bootImages.Count - 1))
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.75f, 1.5f));
                ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
            }

            yield return new WaitForSeconds(3.5f);
            ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", bootImages.Last());
            Destroy(ekranMiddle);
            yield return new WaitForSeconds(3.5f);
        }


        ekranMiddle = Instantiate(LoginEkran, middle);
        yield return new WaitForSeconds(.001f);
    }



    public IEnumerator EndNormal(GameObject ekran)
    {
        Destroy(ekranMiddle);
        yield return new WaitForSeconds(3.5f);
        ekranMiddle = Instantiate(ekran, middle); // login

        var temp = ekranMiddle.GetComponentsInChildren<InputChooser>();
        foreach (var item in temp)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in loginImages.GetRange(0,7))
        {
            ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", item);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.75f,1.25f));
        }

        foreach (var item in temp)
        {
            item.gameObject.SetActive(true);
            item.IsImportant = true;
        }




        start_timer = true;
        foreach (var item in textOdliczanie)
        {
            item.gameObject.SetActive(true);
        }
    }

    IEnumerator NextNormal()
    {
        GameInfo.SetEndToSeen(0);
        FindObjectOfType<PauzerScript>().enabled = false;
        Controller playerController = GetComponentInChildren<Controller>();

        start_timer = false;
        foreach (var item in ekranMiddle.GetComponentsInChildren<InputChooser>())
        {
            item.gameObject.SetActive(false);
            item.IsImportant = false;
        }

        playerController.mouseMovement.enabled = false;
        playerController.interaction.enabled = false;
        playerController.Camera.transform.LookAt(middle);
        playerController.playerUI.crosshair.SetActive(false);


        playerController.Camera.GetComponent<AudioSource>().Stop();
        playerController.Camera.GetComponent<AudioSource>().clip = endingEpicMusic;
        playerController.Camera.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.25f);
        ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", loginImages[12]);
        yield return new WaitForSeconds(2.5f);




        ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", loginImages[7]);//kamer.
        yield return new WaitForSeconds(0.25f);
        ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", loginImages[8]);//kamer..
        foreach (var item in textOdliczanie)
        {
            item.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", loginImages[9]);//kamer...
        yield return new WaitForSeconds(1f);
        ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", loginImages[10]);//kamer [x]
        yield return new WaitForSeconds(1.5f);
        ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", loginImages[11]);//dyskrecja
        yield return new WaitForSeconds(3f);
        ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", loginImages[12]);//czarny ekrn
        yield return new WaitForSeconds(1.5f);

        ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", loginImages[13]);//kam1
        


        var l = Instantiate(StartUpEkran, left);
        l.GetComponent<Renderer>().material.SetTexture("_MainTex", loginImages[14]);//kam2
        var l2= Instantiate(StartUpEkran, right);
        l2.GetComponent<Renderer>().material.SetTexture("_MainTex", loginImages[15]);//kam3


        // NARRACJA
        yield return new WaitForSeconds(1f);
        var n = GetComponents<NarratorVoiceController>().OrderBy(n => n.Narracja.name).ToArray();
        n[0].Play();
        yield return new WaitForSeconds(n[0].Narracja.length+1);
        n[1].Play();

        ///////////


        yield return new WaitForSeconds(5.75f - n[0].Narracja.length-1f);
        yield return playerController.CloseEye(1.0f);

        yield return new WaitForSeconds(playerController.Camera.GetComponent<AudioSource>().clip.length - playerController.Camera.GetComponent<AudioSource>().time);
        SceneManager.LoadSceneAsync(0);
    }




    bool start_timer = false;
    float time_max = 2* 60; // in seconds
    private void Update()
    {
        if (start_timer)
        {
            time_max -= Time.deltaTime;
            TimeSpan t = TimeSpan.FromSeconds(time_max);
            foreach (var item in textOdliczanie)
            {
                item.text = t.ToString(@"mm\:ss\:ff");
            }

            if (time_max <= 0f)
            {
                start_timer = false;
                Debug.Log("Zakonczenie: Smierc");

                StartCoroutine("_end_death");
            }
        }
    }

    IEnumerator _end_death()
    {
        GameInfo.SetEndToSeen(1);

        Controller playerController = GetComponentInChildren<Controller>();
        playerController.mouseMovement.enabled = false;
        playerController.movement.enabled = false;
        yield return playerController.CloseEye(1.0f);

        SceneManager.LoadSceneAsync(0);
    }


    public void EndSecret()
    {
        Destroy(ekranMiddle);
        StartCoroutine("_init_secret");
    }

    IEnumerator _init_secret()
    {
        FindObjectOfType<PauzerScript>().enabled = false;
        Controller playerController = GetComponentInChildren<Controller>();



        playerController.mouseMovement.enabled = false;
        playerController.interaction.enabled = false;
        playerController.anim.enabled = true;
        playerController.Camera.transform.LookAt(middle);
        playerController.Camera.GetComponent<Animator>().SetBool("setEnd", true);
        playerController.playerUI.gameObject.SetActive(false);
        GameInfo.SetEndToSeen(2);


        ekranMiddle = Instantiate(sekretEndScreen, middle);
        int index = 0;
        float waitin = 0f;//45.47 all
        IEnumerator _set(float _how_long)
        {
            waitin += _how_long;
            ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", secretBootImages[index++]);
            yield return new WaitForSeconds(_how_long);
        }


        yield return new WaitForSeconds(1);//optimum os
        yield return _set(3.5f);//wykryto niezgodnoœci
        yield return _set(3f);//skanowanie.
        yield return _set(1.25f);//skanowanie..
        yield return _set(1.9f);//skanowanie...
        yield return _set(3.5f);//skanowanie [x]
        yield return _set(1.5f);//bledy 0
        yield return _set(2.25f);//bledy 1
        yield return _set(3.5f);//bledy 2
        yield return _set(1.25f);//bledy 3
        yield return _set(1f);//bledy 4
        yield return _set(1f);//bledy 5
        yield return _set(0.75f);//bledy 7
        yield return _set(0.68f);//bledy 15
        yield return _set(0.44f);//bledy 64
        yield return _set(0.25f);//bledy 138
        yield return _set(0.13f);//bledy 999

        yield return _set(5f);//Err soon
        playerController.Camera.GetComponent<AudioSource>().Stop();
        playerController.Camera.GetComponent<AudioSource>().clip = endingEpicMusic;
        playerController.Camera.GetComponent<AudioSource>().Play();
        yield return _set(3.37f);//Ewak .
        yield return _set(2.73f);//Ewak ..
        yield return _set(4.37f);//Ewak ...
        yield return _set(4f);//Ewak [x]
        yield return _set(2f);//otwarcie drzwi

        var _eye = Instantiate(playerController.eye_prefab);
        _eye.GetComponent<Animator>().SetBool("close", true);


        yield return new WaitForSeconds(playerController.Camera.GetComponent<AudioSource>().clip.length- playerController.Camera.GetComponent<AudioSource>().time);
        SceneManager.LoadSceneAsync(0);
    }
}

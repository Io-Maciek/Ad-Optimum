using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
                yield return new WaitForSeconds(Random.Range(0.75f, 1.5f));
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



    public IEnumerator NextMiddle(GameObject ekran)
    {
        Destroy(ekranMiddle);
        yield return new WaitForSeconds(3.5f);
        ekranMiddle = Instantiate(ekran, middle);
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
        playerController.Camera.transform.LookAt(middle);
        playerController.Camera.GetComponent<Animator>().SetBool("setEnd", true);
        // paski cutscenki u góry i do³u
        // TODO wy³¹cz kursor + animacja ruchu do przodu do monitora
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
        yield return _set(3.37f);//Ewak .       //TODO literowka ewakYacyjnej
        yield return _set(2.73f);//Ewak ..
        yield return _set(4.37f);//Ewak ...
        yield return _set(4f);//Ewak [x]
        yield return _set(.1f);//otwarcie drzwi



        // TODO dokonczenie zakonczenia
    }
}

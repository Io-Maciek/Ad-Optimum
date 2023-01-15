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
        StartCoroutine("_init");
    }

    IEnumerator _init()
    {
        GameInfo.SetEndToSeen(2);
        yield return new WaitForSeconds(2.5f);
        ekranMiddle = Instantiate(sekretEndScreen, middle);

        foreach (var img in secretBootImages.GetRange(0, 16))
        {
            yield return new WaitForSeconds(Random.Range(0.75f, 1.5f));
            ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", img);
        }

        yield return new WaitForSeconds(.25f);

        foreach (var img in secretBootImages.GetRange(16, 6))
        {
            yield return new WaitForSeconds(Random.Range(0.75f, 1.5f));
            ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", img);
        }

        // TODO dokonczenie zakonczenia
    }
}

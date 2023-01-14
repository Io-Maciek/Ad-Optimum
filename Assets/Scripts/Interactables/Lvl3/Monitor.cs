using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    Transform middle;
    Transform left;
    Transform right;

    public GameObject StartUpEkran;
    public GameObject LoginEkran;
    public List<Texture2D> bootImages;

    void Awake()
    {
        middle = transform.Find("main");
        left = transform.Find("left");
        right = transform.Find("right");
    }

    GameObject ekranMiddle;
    public IEnumerator Init()
    {
        yield return new WaitForSeconds(1.25f);
        ekranMiddle = Instantiate(StartUpEkran, middle);


        foreach (var texture in bootImages.GetRange(0,bootImages.Count-1))
        {
            yield return new WaitForSeconds(Random.Range(0.75f, 1.5f));
            ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
        }

        yield return new WaitForSeconds(3.5f);
        ekranMiddle.GetComponent<Renderer>().material.SetTexture("_MainTex", bootImages.Last());
        Destroy(ekranMiddle);
        yield return new WaitForSeconds(3.5f);


        ekranMiddle = Instantiate(LoginEkran, middle);
        yield return new WaitForSeconds(.001f);
    }

}
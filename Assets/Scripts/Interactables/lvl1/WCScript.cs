using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WCScript : Interactable
{
    papier1 p1;
    papier2 p2;
    papier3 p3;
    papier4 p4;
    papier5 p5;
    papier6 p6;
    papier7 p7;

    public Transform[] PlaceForPapier;
    public GameObject PapierPrefab;
    public GameObject FusePrefab;

    protected override void Awake()
    {
        base.Awake();
        p1 = GetComponent<papier1>();
        p2 = GetComponent<papier2>();
        p3 = GetComponent<papier3>();
        p4 = GetComponent<papier4>();
        p5 = GetComponent<papier5>();
        p6 = GetComponent<papier6>();
        p7 = GetComponent<papier7>();
    }



    bool _got = false; 

    public override Result<object, string> Action(params object[] args)
    {

        if (!_got)
        {
            if (!p1._was_heard)
            {
                if (!p1.IS_PLAYING)
                {
                    int r = Random.Range(0, PlaceForPapier.Length);
                    Instantiate(PapierPrefab, PlaceForPapier[r]);
                }
                p1.Play();
            }
            else
                p2.Play();
        }
        else if(p3._was_heard)
        {
            if (!p4._was_heard)
                p4.Play();
            else if (!p5._was_heard)
                p5.Play();
            else if (!p6._was_heard)
            {
                IsImportant = false;
                if(!p6.IS_PLAYING)
                    StartCoroutine("_give");
                p6.Play();
            }
            else
                p7.Play();
        }


        return Result<string>.Ok();
    }


    private void OnTriggerStay(Collider other)
    {
        if(!(p1.IS_PLAYING || p2.IS_PLAYING))
        {
            if (other.gameObject.name == "Papier(Clone)" && other.GetComponent<BetterHolding>().isGrabbed)
            {
                _got = true;
                p3.Play();
                other.GetComponent<BetterHolding>().LetGo();
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator _give()
    {
        yield return new WaitForSeconds(p6.Narracja.length-.5f);
        var fuse = Instantiate(FusePrefab);
        fuse.transform.position = transform.position + transform.forward*1.5f + Vector3.up*3+ transform.right*2;
    }
}

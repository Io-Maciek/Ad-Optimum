using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsClicks : MonoBehaviour
{
    Button btnDoMenu;
    Button btnDoGry;
    Button btnOpcjeShow;
    public GameObject opcjeMenu;

    PauzerScript pauzer;
    void Start()
    {
        pauzer = FindObjectOfType<PauzerScript>();
        btnDoMenu = _get(nameof(btnDoMenu), doMenu);
        btnDoGry = _get(nameof(btnDoGry), doGry);
        btnOpcjeShow = _get(nameof(btnOpcjeShow), showOpcje);
    }

   
    void showOpcje()
    {
        var opcje = Instantiate(opcjeMenu, transform.parent);
        opcje.GetComponent<OpcjeMenus>().poprzedniEkran = gameObject;
        gameObject.SetActive(false);
    }

    void doMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    void doGry()
    {
        pauzer.SetPauze();
    }









    Button _get(string _button, UnityAction _onClick)
    {
        Button _btn = transform.Find(_button).GetComponent<Button>();
        _btn.onClick.AddListener(_onClick);
        return _btn;
    }
}

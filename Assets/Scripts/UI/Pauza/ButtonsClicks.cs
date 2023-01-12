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

    PauzerScript pauzer;
    void Start()
    {
        pauzer = FindObjectOfType<PauzerScript>();
        btnDoMenu = _get(nameof(btnDoMenu), doMenu);
        btnDoGry = _get(nameof(btnDoGry), doGry);
    }

    // Update is called once per frame
    void Update()
    {
        
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

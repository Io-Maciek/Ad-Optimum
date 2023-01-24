using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauzerScript : MonoBehaviour
{
    bool isPauzed = false;
    Controller playerController;
    public GameObject menuPauzyPrefab;


    GameObject _temp_menu;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            SetPauze();
        }
    }


    public void SetPauze()
    {
        switch (isPauzed)
        {
            case false:
                /*
                 * zatrzymywanie
                 */

                Time.timeScale = 0.0f;
                _temp_menu = Instantiate(menuPauzyPrefab);
                Cursor.lockState = CursorLockMode.Confined;
                //playerController.playerUI.gameObject.SetActive(false);
                FindObjectOfType<NarratorVoice>().audio.Pause();

                break;


            case true:
                /*
                 * wznawianie
                 */

                Time.timeScale = 1.0f;
                Destroy(_temp_menu);
                Cursor.lockState = CursorLockMode.Locked;
                //playerController.playerUI.gameObject.SetActive(true);
                FindObjectOfType<NarratorVoice>().audio.UnPause();

                break;
        }

        isPauzed = !isPauzed;
        playerController.Pauze(isPauzed);
        Cursor.visible = isPauzed;
    }
}

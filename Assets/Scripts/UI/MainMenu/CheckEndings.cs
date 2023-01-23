using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndings : MonoBehaviour
{
    public GameObject endingPanel;
    public GameObject[] endingBadges;
    public GameObject bgText;
    void Awake()
    {
        endingPanel.SetActive(false);
        foreach (var end in endingBadges)
        {
            end.SetActive(false);
        }
        bgText.SetActive(false);
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        GameInfo info = GameInfo.Read();
        if (info.Any())
        {
            bgText.SetActive(true);
            endingPanel.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                endingBadges[i].SetActive(info.Seen[i]);
            }
        }
    }
}

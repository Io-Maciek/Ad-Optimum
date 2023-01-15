using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndings : MonoBehaviour
{
    public GameObject endingPanel;
    public GameObject[] endingBadges;
    void Awake()
    {
        endingPanel.SetActive(false);
        foreach (var end in endingBadges)
        {
            end.SetActive(false);
        }
    }

    private void Start()
    {
        GameInfo info = GameInfo.Read();
        if (info.Any())
        {
            endingPanel.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                endingBadges[i].SetActive(info.Seen[i]);
            }
        }
    }
}

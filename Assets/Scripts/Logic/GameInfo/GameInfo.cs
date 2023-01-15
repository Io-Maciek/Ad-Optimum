using Assets.Scripts.EasterEggs;
using IoDeSer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameInfo
{
    public bool[] Seen { get; set; }

    static string _DOC = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    static string _GAME = Path.Combine(_DOC, "Ad Optimum");
    static string _FILE = Path.Combine(_GAME, "config.io");


    public GameInfo()
    {
        Seen = new bool[3];
    }


    /// <summary>
    /// Returns true, if any of the ending was seen and saved to file.
    /// </summary>
    public bool Any()
    {
        return Seen.Any(x => x == true);
    }


    public static GameInfo Read()
    {
        if (!Directory.Exists(_GAME))
        {
            Directory.CreateDirectory(_GAME);
        }

        GameInfo info;

        try
        {
            using (StreamReader sr = new StreamReader(_FILE))
            {
                info = IoFile.ReadFromFile(sr, typeof(GameInfo)) as GameInfo;
            }
        }
        catch (FileNotFoundException)
        {
            info = new GameInfo();
        }

        return info;
    }

    public static void SetEndToSeen(int nrZakonczenia)
    {
        if (!Directory.Exists(_GAME))
        {
            Directory.CreateDirectory(_GAME);
        }

        GameInfo previous = Read();
        previous.Seen[nrZakonczenia] = true;

        using (StreamWriter sr = new StreamWriter(_FILE))
        {
            IoFile.WriteToFile(previous, sr);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave
{
    public int id { get; set; }
    public string SayMyName { get; set; }
    public string SceneName { get; set; }
    [IoDeSer.IoItemName("RealImportantStuffMan_DontChangePLS")]
    public uint ProgressValue { get; set; }

    public override string ToString()
    {
        return $"{id} {SayMyName} {SceneName} {ProgressValue}";
    }
}

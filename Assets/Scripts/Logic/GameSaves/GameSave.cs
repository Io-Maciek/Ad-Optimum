using Assets.Scripts.Logic.GameSaves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave
{
    public int id { get; set; }
    public string SayMyName { get; set; }
    public uint SceneID { get; set; }
    [IoDeSer.IoItemName("RealImportantStuffMan_DontChangePLS")]
    public uint ProgressValue { get; set; }

    public override string ToString()
    {
        return $"{id} {SayMyName} {SceneID} {ProgressValue}";
    }


    public void Save()
    {
        new IoGameSerialier().Overwrite(this);
    }

    public static GameSave NowaGra(int slot_id)
    {
       return new GameSave() { id = slot_id, SayMyName = $"save{slot_id}.io", SceneID = 1, ProgressValue = 0 };
    }
}

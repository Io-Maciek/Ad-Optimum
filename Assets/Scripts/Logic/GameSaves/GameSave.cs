using Assets.Scripts.EasterEggs;
using Assets.Scripts.Logic.GameSaves;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSave
{
    public int id { get; set; }
    public string SayMyName { get; set; }
    public uint SceneID { get; set; }
    [IoDeSer.IoItemName("PoggersValue")]
    public uint ProgressValue { get; set; }

    [IoDeSer.IoItemName("Fun")]
    public bool[] SecretNumber { get; set; }



    public override string ToString()
    {
        return $"{id} {SayMyName} {SceneID} {ProgressValue} |{string.Join(" ", SecretNumber)}|";
    }


    public void Save()
    {
        new IoGameSerialier().Overwrite(this);
    }

    public static GameSave NowaGra(int slot_id)
    {
       return new GameSave() { id = slot_id, SayMyName = $"save{slot_id}.io", SceneID = 1, ProgressValue = 0, SecretNumber = new bool[Sekrety.ILOSC_SEKRETOW] };
    }

    public void AddSecret(int sekret_Id) => SecretNumber[sekret_Id] = true;
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using IoDeSer;

namespace Assets.Scripts.Logic.GameSaves
{
    public class IoGameSerialier : GameSavesSerialier
    {
        public override string pattern => "*.io";

        public override void Overwrite(GameSave save)
        {
            if (!Directory.Exists(_GAME))
            {
                Directory.CreateDirectory(_GAME);
            }

            using (StreamWriter sw = new StreamWriter(Path.Combine(_GAME, save.SayMyName)))
            {
                IoFile.WriteToFile(save, sw);
            }
        }

        public override List<GameSave> Read()
        {
            Result<string[]> res_files = GetFilesFromDocuments();

            return res_files.Match(
                () =>
                    {
                        UnityEngine.Debug.Log("NIE MA");
                        return new List<GameSave>();
                    },
                (files) =>
                    {
                        List<GameSave> result = new List<GameSave>();

                        foreach (var item in files)
                        {
                            GameSave save_file;
                            try
                            {
                                using (StreamReader sr = new StreamReader(item))
                                {
                                    save_file = IoFile.ReadFromFile(sr, typeof(GameSave)) as GameSave;
                                }
                                result.Add(save_file);
                            }
                            catch (Exception e)
                            {
                                UnityEngine.Debug.Log($"File '{item}' could not be read because this error was encountered:\n{e}");
                            }

                        }
                        return result;

                    }
            );
        }
    }
}

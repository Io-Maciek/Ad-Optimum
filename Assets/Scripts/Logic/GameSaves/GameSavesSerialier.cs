using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logic.GameSaves
{
    public abstract class GameSavesSerialier
    {
        static string _DOC = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        protected static string _GAME = Path.Combine(_DOC, "Ad Optimum");

        public abstract string pattern { get; }

        public Result<string[], string> GetFilesFromDocuments()
        {
            try
            {
                string[] files = Directory.GetFiles(_GAME, pattern);
                return Result<string[], string>.Ok(files);
            }
            catch (DirectoryNotFoundException)
            {
                return Result<string[],string>.Err("NoDir");
            }
        }

        public abstract List<GameSave> Read();
        public abstract void Overwrite(GameSave save);
    }
}

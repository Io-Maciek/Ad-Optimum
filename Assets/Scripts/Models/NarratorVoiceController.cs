using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    /// <summary>
    /// Class that handles playing audio clip of narration. Requires some audio clip <see cref="Narracja"/>.
    /// <para>Audio clip is inserted into <see cref="PlanszaInfo.KontrolerAudio"/> in method <see cref="Play"/> and overwrites current playing audio.</para>
    /// <para>Can be played only one if <see cref="TylkoPierwszyRaz"/> is set to true.</para>
    /// </summary>
    public abstract class NarratorVoiceController : MonoBehaviour
    {
        // somehow do caption

        public AudioClip Narracja;
        public bool TylkoPierwszyRaz;
        bool _was_heard = false;

        protected PlanszaInfo _planszaInfo;
        public virtual void Start()
        {
            _planszaInfo = FindObjectOfType<PlanszaInfo>();
            if (Narracja == null)
            {
                Debug.LogWarning("Narration Audio clip is missing!");
            }
        }


        /// <summary>
        /// Changes audio for new. Can start once only if <see cref="TylkoPierwszyRaz"/> is set to true, otherwise will start playing everytime this method is called.
        /// </summary>
        /// <returns>
        /// Number indicating the state of new audio clip.
        /// <para> 1 - previous audio was cut off</para>
        /// <para> 0 - started playing without issue</para>
        /// <para> -1 - audio was heard before and did not start playing</para>
        /// </returns>
        public int Play()
        {
            if (TylkoPierwszyRaz)
            {
                if (!_was_heard)
                {
                    var czyPrzerwaloPoprzedni = _planszaInfo.KontrolerAudio.isPlaying;
                    _planszaInfo.ChangeAudio(Narracja);
                    _was_heard = true;
                    return czyPrzerwaloPoprzedni ? 1 : 0;
                }
            }
            else
            {
                var czyPrzerwaloPoprzedni = _planszaInfo.KontrolerAudio.isPlaying;
                _planszaInfo.ChangeAudio(Narracja);
                return czyPrzerwaloPoprzedni ? 1 : 0;
            }

            return -1;
        }
    }
}

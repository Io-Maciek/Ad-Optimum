using System;
using System.Collections;
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

        public string KtoMowi;
        public string Text;
        [Space]
        public bool TylkoPierwszyRaz;

        public bool _was_heard { get; private set; } = false;
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
            if (!IS_PLAYING)
            {
                if (TylkoPierwszyRaz)
                {
                    if (!_was_heard)
                    {
                        var czyPrzerwaloPoprzedni = _planszaInfo.KontrolerAudio.isPlaying;
                        _planszaInfo.ChangeAudio(Narracja);
                        _planszaInfo.p_controller.playerUI.SetText($"{KtoMowi}: {Text}");
                        StartCoroutine("_off");
                        return czyPrzerwaloPoprzedni ? 1 : 0;
                    }
                }
                else
                {
                    var czyPrzerwaloPoprzedni = _planszaInfo.KontrolerAudio.isPlaying;
                    _planszaInfo.ChangeAudio(Narracja);
                    _planszaInfo.p_controller.playerUI.SetText($"{KtoMowi}: {Text}");
                    StartCoroutine("_off");
                    return czyPrzerwaloPoprzedni ? 1 : 0;
                }
            }

            return -1;
        }


        public bool IS_PLAYING { get; private set; } = false;
        IEnumerator _off()
        {
            IS_PLAYING = true;
            yield return new WaitForSeconds(Narracja.length + 1);
            _planszaInfo.p_controller.playerUI.SetOff();
            IS_PLAYING = false;
            _was_heard = true;
        }
    }
}

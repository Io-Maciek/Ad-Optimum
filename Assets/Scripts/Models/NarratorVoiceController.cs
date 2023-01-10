using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public abstract class NarratorVoiceController : MonoBehaviour
    {
        // somehow do caption
        public AudioClip Narracja;
        public bool TylkoPierwszyRaz;
        bool _was_heard = false;

        public PlanszaInfo _planszaInfo;
        private void Start()
        {
            _planszaInfo = FindObjectOfType<PlanszaInfo>();
        }

        public void Play()
        {
            if (TylkoPierwszyRaz)
            {
                if (!_was_heard)
                {
                    _planszaInfo.ChangeAudio(Narracja);
                    _was_heard = true;
                }
            }
            else
            {
                _planszaInfo.ChangeAudio(Narracja);
            }
        }
    }
}

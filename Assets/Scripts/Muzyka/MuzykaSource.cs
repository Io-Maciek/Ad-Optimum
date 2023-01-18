using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Muzyka
{
    public abstract class MuzykaSource : MonoBehaviour
    {
        public new AudioSource audio { get; private set; }
        public float defaultVolume { get; private set; }

        void Awake()
        {
            audio = GetComponent<AudioSource>();
            if (audio == null)
                Debug.LogError($"This element '{gameObject.name}' does not have audio source!");
            else
                defaultVolume = audio.volume;
        }


        public void Set(float newVolume)
        {
            audio.volume = defaultVolume * newVolume;
        }
    }
}

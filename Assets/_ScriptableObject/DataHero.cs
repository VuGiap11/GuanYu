using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{

    [CreateAssetMenu(fileName = "Data", menuName = "HeroData")]
    public class DataHero : ScriptableObject
    {
        public Image avatar;
        public string nameHero;
        public int countAtk;
        public int countSkill;
        public AudioClip attackAudio;
        public AudioClip attackhitAudio;
        public AudioClip skillAudio;
        public AudioClip skillhitAudio;
    }
}

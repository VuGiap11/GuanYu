using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuanYu
{
    public class BtnOnOff : MonoBehaviour
    {
        [SerializeField] private GameObject btn;
        public void OnOffObj(bool btnObj)
        {
            btn.SetActive(btnObj);
            SoundManager.instance.PressButtonAudio();
        }
    }
}

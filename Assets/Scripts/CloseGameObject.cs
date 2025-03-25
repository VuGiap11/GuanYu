using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuanYu
{
    public class CloseGameObject : MonoBehaviour
    {
        [SerializeField] private GameObject btn;
        public void OffObj()
        {
            if (!btn.activeInHierarchy)
            {
                SoundManager.instance.PressButtonAudio();
                return;
            }
            else
            {
                //SoundManager.instance.CloseButtonAudio();
                SoundManager.instance.PressButtonAudio();
                btn.SetActive(false);
            }
        }

    }
}
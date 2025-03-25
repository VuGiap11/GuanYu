using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GuanYu
{
    public class ChestIteam : MonoBehaviour
    {
        public ChestData ChestData;
        [SerializeField] private GameObject IcoinOffChest;
        [SerializeField] private GameObject IcoinOnChest;
        [SerializeField] private TextMeshProUGUI numberTextOff;
        [SerializeField]private TextMeshProUGUI numberTextOn;

        public void SetFocus(bool focus)
        {
            IcoinOnChest.SetActive(focus);
            IcoinOffChest.SetActive(!focus);
            if (focus)
            {
                numberTextOn.text = ("X"+ChestData.number).ToString();
            }
            else
            {
                numberTextOff.text = ("X" + ChestData.number).ToString();
            }
        }
        public void ShowChest()
        {
            SoundManager.instance.PressButtonAudio();
            ChestController.instance.BtnChestMenuClick(this);
        }

        public void ShowNumber()
        {

        }
    }
}
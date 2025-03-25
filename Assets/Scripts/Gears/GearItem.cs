using GuanYu.Hero;
using GuanYu.User;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class GearItem : MonoBehaviour
    {
        public Image iconGearImg, btnImg;
        public Button button;
        //public TextMeshProUGUI levelTxt;
        public GearData data;
        public TextMeshProUGUI numberText;
        public GameObject numberObj;


        // SET DU LIEU KHI INNIT GEAR
        public void SetUpData(GearData data)
        {
            //levelTxt.text = "lv" + data.Level.ToString();
            this.data = data;
            //iconGearImg.sprite = HeroAssets.Instance.GetSprite(data.GearDataCf.Name);
            SetUiGearItem();
            if (this.data.number <= 1)
            {
                numberObj.SetActive(false);
            }
            else
            {
                numberObj.SetActive(true);
            }
            //SetRarity((int)data.GearDataCf.Rarity);
            //btnImg.sprite = HeroAssets.Instance.GetSprite(data.Rarity.ToString());
        }

        private void SetRarity(int index) // xác ??nh ?? hi?m
        {
            switch (index)
            {
                case 0:
                    btnImg.sprite = HeroAssets.Instance.GetSprite("reward_item_frame_cyan");
                    break;

                case 1:
                    btnImg.sprite = HeroAssets.Instance.GetSprite("reward_item_frame_green");
                    break;
                case 2:
                    btnImg.sprite = HeroAssets.Instance.GetSprite("reward_item_frame_orange");
                    break;
                case 3:
                    btnImg.sprite = HeroAssets.Instance.GetSprite("reward_item_frame_purple");
                    break;
                case 4:
                    btnImg.sprite = HeroAssets.Instance.GetSprite("reward_item_frame_red");
                    break;
                case 5:
                    btnImg.sprite = HeroAssets.Instance.GetSprite("reward_item_frame_blue");
                    break;
            }

        }
        public void SetUiGearItem()
        {
            if (numberText == null) return;
            numberText.text = data.number.ToString();
        }
        public void AddGear()
        {
            //GearController.instance.AddGrear(this, this.data);
            UserManager.Instance.SaveHeroData();
            GearDataCtrl.Instance.SaveDataGear();
        }

        //public void ShowInforGear()
        //{
        //    GearController.instance.ShowDetailGear(this,this.data);
        //}
        public bool CheckIdGear(int index)
        {
            for (int i = 0; i < UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Count; i++)
            {
                if (index == UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}


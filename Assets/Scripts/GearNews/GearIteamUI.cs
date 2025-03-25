
using DG.Tweening;
using GuanYu.Hero;
using GuanYu.User;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class GearIteamUI : MonoBehaviour
    {
        public Image icoinGearImg, backGroundImg;
        public GearData data;
        public TextMeshProUGUI numberGearText;
        public GameObject numberGearObj;

        public void SetUpData(GearData data)
        {
            this.data = data;
            GearDataCf gearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(data.id);
            icoinGearImg.sprite = HeroAssets.Instance.GetSprite(gearDataCf.Name);
            SetNumBerUIText();
            if (this.data.number <= 1)
            {
                numberGearObj.SetActive(false);
            }
            else
            {
                numberGearObj.SetActive(true);
            }
            //SetRarity(gearDataCf.ID);
            SetRarity(gearDataCf.Rarity);
        }
        private void SetNumBerUIText()
        {
            if (numberGearText == null) return;
            numberGearText.text = data.number.ToString();
        }
        public void SetRarity(Rarity rarity) // xác ??nh ?? hi?m
        {
            switch (rarity)
            {
                case Rarity.Common:
                    backGroundImg.sprite = HeroAssets.Instance.GetSprite(Contans.BGRarityCommon);
                    break;

                case Rarity.Prenium:
                    backGroundImg.sprite = HeroAssets.Instance.GetSprite(Contans.BGRarityPrenium);
                    break;
                case Rarity.Rare:
                    backGroundImg.sprite = HeroAssets.Instance.GetSprite(Contans.BGRarityRare);
                    break;
                case Rarity.Epic:
                    backGroundImg.sprite = HeroAssets.Instance.GetSprite(Contans.BGRarityEpic);
                    break;
                case Rarity.Legendary:
                    backGroundImg.sprite = HeroAssets.Instance.GetSprite(Contans.BGRarityLegendary);
                    break;
                case Rarity.Mythical:
                    backGroundImg.sprite = HeroAssets.Instance.GetSprite(Contans.BGRarityMythical);
                    break;
            }

        }
    
        public void ShowInforGear()
        {
            SoundManager.instance.PressButtonAudio();
            GearController.instance.ShowDetailGear( this.data, this);
        }
        public void AddGear()
        {
                GearController.instance.AddGrear(this, this.data);
                UserManager.Instance.SaveHeroData();
                GearDataCtrl.Instance.SaveDataGear();
                //this.gameObject.SetActive(false);
        }
    }
}

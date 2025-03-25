using GuanYu.Hero;
using GuanYu.User;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class GearSlotUI : MonoBehaviour
    {
        public int indexslot;
        public Image icoinGear;
        public int id;
        //public GearData data;
        public GearIteamUI gearIteamUI;
        public void InitGearSlot()
        {
            GearDataCf gearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(this.id);
            if (UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Count == 0)
            {
                this.icoinGear.sprite = HeroAssets.Instance.GetSprite("character_select_icon_add");
                this.id = -1;
                //this.data = null;
                this.gearIteamUI = null;
            }
            for (int k = 0; k < GearController.instance.lsGearItems.Count; k++)
            {
                var gearItem = GearController.instance.lsGearItems[k];
                GearDataCf gearDataCfCheck = GearDataCtrl.Instance.GetGearDataCfByIndex(GearController.instance.lsGearItems[k].data.id);
                //Debug.Log("chạy đến đây");
                for (int j = 0; j < UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Count; j++)
                {
                    var heroGearId = UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros[j];
                    if (gearItem.data.id == heroGearId && indexslot == (int)gearDataCfCheck.WeaponType)
                    {
                        //this.data = GearController.instance.lsGearItems[k].data;
                        this.icoinGear.sprite = HeroAssets.Instance.GetSprite(gearDataCfCheck.Name);
                        this.gearIteamUI = gearItem; // CẦN THÊM KHI THAY ĐỔI
                        this.id = GearController.instance.lsGearItems[k].data.id;
                        return;
                    }
                    else
                    {
                        this.icoinGear.sprite = HeroAssets.Instance.GetSprite("character_select_icon_add");
                        //data = new GearData();
                        //data = null;
                        this.gearIteamUI = null;
                        this.id = -1;
                    }
                }
            }
        }
        public void ReturnGear()
        {
            SoundManager.instance.PressButtonAudio();
            if (this.gearIteamUI == null) return;
            this.icoinGear.sprite = HeroAssets.Instance.GetSprite("character_select_icon_add");
            this.gearIteamUI.data.number++;
            if (this.gearIteamUI.data.number == 1)
            {
                this.gearIteamUI.gameObject.SetActive(true);
                this.gearIteamUI.numberGearObj.SetActive(false);
            }
            else if (this.gearIteamUI.data.number > 1)
            {
                this.gearIteamUI.numberGearObj.SetActive(true);
                this.gearIteamUI.numberGearText.text = this.gearIteamUI.data.number.ToString();
            }
            UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Remove(id);
            this.gearIteamUI.data.slot = -1;
            this.gearIteamUI = null;
            this.id = -1;
            UserManager.Instance.SaveHeroData();
            GearDataCtrl.Instance.SaveDataGear();
        }
    }
}
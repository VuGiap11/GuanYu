
using GuanYu.Hero;
using GuanYu.User;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class GearSlot : MonoBehaviour
    {
        public int indexslot;
        public Image newImage;
        public GearData data;
        public GearItem gearItem;
        //defaultImage
        public void ReturnGear()
        {
            if (gearItem == null) return;
            newImage.sprite = HeroAssets.Instance.GetSprite("character_select_icon_add");
            //data = new GearData();
            data = null;
            gearItem.gameObject.SetActive(true);
            UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Remove(gearItem.data.id);
            gearItem.data.slot = -1;
            gearItem = null;
            UserManager.Instance.SaveHeroData();
            GearDataCtrl.Instance.SaveDataGear();
        }

        //public void InitGearSlot(int Id)
        //{
        //    for(int i= 0; i <GearController.instance.lsGearItems.Count; i++)
        //    {
        //        if (Id == GearController.instance.lsGearItems[i].data.ID)
        //        {
        //            this.data = GearController.instance.lsGearItems[i].data;
        //            this.newImage.sprite = HeroAssets.Instance.GetSprite(GearController.instance.lsGearItems[i].data.Name);
        //            break;
        //        }
        //    }

        //}
        public void InitGearSlot()
        {
            //Debug.Log("bắt đầu chạy vào init");
            if (UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Count == 0)
            {
                this.newImage.sprite = HeroAssets.Instance.GetSprite("character_select_icon_add");
                //data = new GearData();
                data = null;
                this.gearItem = null;
            }
            //for (int k = 0; k < GearController.instance.lsGearItems.Count; k++)
            //{
            //    //Debug.Log("bắt đầu chạy vào init----22222");
            //    var gearItem = GearController.instance.lsGearItems[k];
            //    //Debug.Log("chạy đến đây");
            //    for (int j = 0; j < UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Count; j++)
            //    {
            //        var heroGearId = UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros[j];
            //        if (gearItem.data.id == heroGearId && indexslot == (int)GearController.instance.lsGearItems[k].data.GearDataCf.Rarity)
            //        {
            //            this.data = GearController.instance.lsGearItems[k].data;
            //            this.newImage.sprite = HeroAssets.Instance.GetSprite(GearController.instance.lsGearItems[k].data.GearDataCf.Name);
            //            this.gearItem = gearItem;
            //            return;
            //        }
            //        else
            //        {

            //            this.newImage.sprite = HeroAssets.Instance.GetSprite("character_select_icon_add");
            //            //data = new GearData();
            //            data = null;
            //            this.gearItem = null;
            //        }
            //    }
            //}
            for (int k = 0; k < GearController.instance.lsGearItems.Count; k++)
            {

                ////Debug.Log("bắt đầu chạy vào init----22222");
                //var gearItem = GearController.instance.lsGearItems[k];
                ////Debug.Log("chạy đến đây");
                //for (int j = 0; j < UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Count; j++)
                //{
                //    var heroGearId = UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros[j];
                //    if (gearItem.data.id == heroGearId && indexslot == (int)GearController.instance.lsGearItems[k].data.GearDataCf.WeaponType)
                //    {
                //        this.data = GearController.instance.lsGearItems[k].data;
                //        this.newImage.sprite = HeroAssets.Instance.GetSprite(GearController.instance.lsGearItems[k].data.GearDataCf.Name);
                //        //this.gearItem = gearItem; // CẦN THÊM KHI THAY ĐỔI
                //        return;
                //    }
                //    else
                //    {

                //        this.newImage.sprite = HeroAssets.Instance.GetSprite("character_select_icon_add");
                //        //data = new GearData();
                //        data = null;
                //        this.gearItem = null;
                //    }
                //}
            }

        }
    }



}

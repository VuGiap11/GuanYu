using GuanYu.Hero;
using GuanYu.User;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuanYu
{
    public enum Rarity
    {
        Common = 0,
        Prenium = 1,
        Rare = 2,
        Epic = 3,
        Legendary = 4,
        Mythical = 5
    }
    public enum WeaponType
    {
        //attack = 0,
        //amor = 1,
        //shooe = 2,
        //ring = 3
        armor = 0,
        Helmet = 1,
        Shield = 2, // khiên
        Boot =3, // giày
        Glove = 4, // gang tay
        WeaponDamage = 5 // v? khí t?ng thêm damage
    }
    [Serializable]
    public class GearDataCf
    {
        public int ID;
        public string Name;
        public int ATK;
        public int Def;
        public int Hp;
        public int Speed;
        public int Star;
        public Rarity Rarity;
        public int Level;
        public WeaponType WeaponType;
    }
    [Serializable]
    public class GearDataCfs
    {
        public List<GearDataCf> GearDatas;
    }

    [Serializable]
    public class GearData
    {
        public int id;
        public int number;
        public int slot;
        //public GearDataCf GearDataCf;
        public GearData(int Id)
        {
            this.id = Id;
            number = 1;
            slot = -1;
            //this.GearDataCf = HeroAssets.Instance.GearDataCfs.GearDatas[this.id];
        }
        //public void UpdateGearStart()
        //{

        //    //for (int i = 0; i < GearHeros.Count; i++)
        //    //{
        //    //    this.Hp += GearController.instance.lsGearItems[GearHeros[i]].data.Hp;
        //    //    this.Atk += GearController.instance.lsGearItems[GearHeros[i]].data.ATK;
        //    //    this.Def += GearController.instance.lsGearItems[GearHeros[i]].data.Def;
        //    //    this.Speed += GearController.instance.lsGearItems[GearHeros[i]].data.Speed;
        //    //}
        //    //for (int i = 0; i < GearController.instance.lsGearItems[GearHeros[i]].Count; i++)
        //    //{

        //    //}
        //}

    }
    [Serializable]
    public class GearDatas
    {
        public List<GearData> gearDatas;
    }

    public class GearDataCtrl : MonoBehaviour
    {
        public static GearDataCtrl Instance;
        public GearDatas GearDatas;
        public GearData gearData;// để check xem gear có bị truufng khi mở hộp không ?
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            //LoadDataGear();
        }
        private void Start()
        {
            //LoadDataGear();
        }

        public GearData GetGearDataByIndex(int id)
        {
            return this.GearDatas.gearDatas.Find(a => { return a.id == id; });
        }

        public GearDataCf GetGearDataCfByIndex(int id)
        {
            return HeroAssets.Instance.GearDataCfs.GearDatas.Find(a => { return a.ID == id; });
        }
        public void LoadDataGear()
        {
            string jsonData = PlayerPrefs.GetString("GearDatas");
            Debug.Log(jsonData);
            if (!string.IsNullOrEmpty(jsonData))
            {

                this.GearDatas = JsonUtility.FromJson<GearDatas>(jsonData);
                Debug.LogWarning("No saved 1 ");
                Debug.LogWarning("luu được geardata");
            }
            else
            {
                Debug.LogWarning("chưa luu được geardata");
                Debug.LogWarning("No saved ");
                for (int i = 0; i < UserManager.Instance.UserData.GearOnShop.Count; i++)
                {
                    GearData gear = new GearData(UserManager.Instance.UserData.GearOnShop[i]);
                    GearDatas.gearDatas.Add(gear);
                }
                SaveDataGear();
            }
        }

        public void SaveDataGear()
        {
            string jsonData = JsonUtility.ToJson(this.GearDatas);
            PlayerPrefs.SetString("GearDatas", jsonData);
        }
        [ContextMenu("OpenBox")]
        public void OpenBox()
        {
            int id = 8;
            //for (int i = 0; i < GearDatas.gearDatas.Count; i++)
            //{
            //    if (GearDatas.gearDatas[i].id == id)
            //    {
            //        GearDatas.gearDatas[i].number++;
            //    }
            //}
            if (CheckGearOnBox(id))
            {
                gearData.number++;
            }
            SaveDataGear();
            GearController.instance.InitGear();
        }
        
        public bool CheckGearOnBox(int id)
        {
            for (int i = 0; i < GearDatas.gearDatas.Count; i++)
            {
                if (GearDatas.gearDatas[i].id == id)
                {
                    gearData = GearDatas.gearDatas[i];
                    return true;
                }
            }
            GearDatas.gearDatas.Add(new GearData(id));
            return false;
        }

        public void CheckGearOnChest(int id)
        {
            for (int i = 0; i < GearDatas.gearDatas.Count; i++)
            {
                if (GearDatas.gearDatas[i].id == id)
                {
                    GearDatas.gearDatas[i].number++;
                    return;
                }
            }
            GearDatas.gearDatas.Add(new GearData(id));
            UserManager.Instance.UserData.GearOnShop.Add(id);
        }
    }

}
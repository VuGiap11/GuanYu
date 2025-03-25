using GuanYu.Hero;
using GuanYu.UI;
using GuanYu.User;
using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;


namespace GuanYu
{
    public class GearController : MonoBehaviour
    {
        public static GearController instance;
        //public GameObject grearPrefab;
        [SerializeField] private GameObject gearIteamPrfab;
        public GameObject AvaHero;
        public Transform CharAva;
        public Transform content;
        //public List<GearItem> lsGearItems = new List<GearItem>();
        public List<GearIteamUI> lsGearItems = new List<GearIteamUI>();
        public List<GearSlotUI> GearSlots = new List<GearSlotUI>();
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI levelText;
        private bool isPawn = false;

        public GameObject GearObject;
        public GameObject HeroSharedUpdateObj;
        [SerializeField] private List<GameObject> Stars;
        public HeroShardUpdate heroShardUpdate;
        [SerializeField] private GameObject noticeObj;
        private List<GearIteamUI> gearIteamsSpeed = new List<GearIteamUI>();

        [Header("new")]
        [SerializeField] private SkeletonGraphic skeletonAnimation;



        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            //InitGear();
        }
        private void OnDisable()
        {
            isPawn = false;
            if (heroShardUpdate.gameObject.activeSelf)
            {
                heroShardUpdate.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            InitGear();
        }
        [ContextMenu("InitGear")]
        public void InitGear()
        {
            if (!isPawn)
            {
                SpawnAvatarHeroOnGear();
                SpwanGearOnShop();
                isPawn = true;
            }
            //if (gameObject.activeSelf) return;
            //SpawnGearOnHero();
        }
        //public void SpwanGearOnShop()
        //{
        //    MyFunction.ClearChild(content);
        //    lsGearItems.Clear();
        //    for (int i = 0; i < GearDataCtrl.Instance.GearDatas.gearDatas.Count; i++)
        //    {
        //        GameObject go = Instantiate(grearPrefab, content);
        //        lsGearItems.Add(go.GetComponent<GearItem>());
        //        go.GetComponent<GearItem>().SetUpData(GearDataCtrl.Instance.GearDatas.gearDatas[i]);
        //        if (go.GetComponent<GearItem>().data.slot >= 0)
        //        {
        //            go.SetActive(false);
        //        }
        //    }
        //    SpawnGearOnHero();
        //}
        public void SpwanGearOnShop()
        {
            MyFunction.ClearChild(content);
            lsGearItems.Clear();
            for (int i = 0; i < GearDataCtrl.Instance.GearDatas.gearDatas.Count; i++)
            {
                GameObject go = Instantiate(gearIteamPrfab, content);
                lsGearItems.Add(go.GetComponent<GearIteamUI>());
                go.GetComponent<GearIteamUI>().SetUpData(GearDataCtrl.Instance.GearDatas.gearDatas[i]);
                if (go.GetComponent<GearIteamUI>().data.slot >= 0)
                {
                    go.SetActive(false);
                }
            }
            SpawnGearOnHero();
        }
        private bool CheckGearAdderinHero(int ID)
        {
            for (int i = 0; i < UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Count; i++)
            {
                if (ID == UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros[i])
                {
                    return true;
                }
            }
            return false;
        }
        private void SpawnGearOnHero()
        {
            for (int i = 0; i < GearSlots.Count; i++)
            {
                GearSlots[i].InitGearSlot();
            }
        }
        // sinh ra nhaan vaajt trong grear
        public void SpawnAvatarHeroOnGear()
        {
            MyFunction.ClearChild(CharAva);
            HeroData heroData = UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate];
            HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(heroData.Index);
            GameObject avaPreb = HeroAssets.Instance.GetHeroUIPrefabByIndex(heroDataCf.Index);
            if (avaPreb != null)
            {
                AvaHero = avaPreb;
                AvaHero.transform.SetParent(this.CharAva, false);
                AvaHero.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            if (skeletonAnimation != null)
            {
                skeletonAnimation.skeletonDataAsset = avaPreb.GetComponent<EffectCtrl>().skeletonGraphicUpdate;
                skeletonAnimation.Initialize(true);
            }

            nameText.text = heroDataCf.Name;
            levelText.text = "Lv. " + heroData.LevelHero.ToString();
            for (int i = 0; i < Stars.Count; i++)
            {
                // Stars[i].SetActive(i < UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].star);
                Stars[i].SetActive(i < heroData.star);
            }
            SetNoticeUpdate();
        }
        //public void AddGrear(GearItem gearItem, GearData data)
        //{
        //    //GearDataCf GearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(data.id);
        //    for (int i = 0; i < GearSlots.Count; i++)
        //    {
        //        //if ((int)gearItem.data.Rarity == GearSlots[i].indexslot)
        //        if ((int)gearItem.data.GearDataCf.WeaponType == GearSlots[i].indexslot)
        //        {

        //            if (GearSlots[i].gearItem == null)
        //            {
        //                if (UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Count > 5) return;
        //                GearSlots[i].newImage.sprite = HeroAssets.Instance.GetSprite(gearItem.data.GearDataCf.Name);
        //                GearSlots[i].data = data;
        //                GearSlots[i].gearItem = gearItem;
        //                data.slot = 1;
        //                UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Add(data.GearDataCf.ID);
        //                gearItem.gameObject.SetActive(false);
        //            }
        //            else
        //            {
        //                UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Remove(GearSlots[i].data.GearDataCf.ID);
        //                GearItem gear = GearSlots[i].gearItem;
        //                GearSlots[i].gearItem = gearItem;
        //                gearItem = gear;
        //                GearSlots[i].newImage.sprite = HeroAssets.Instance.GetSprite(GearSlots[i].gearItem.data.GearDataCf.Name);
        //                GearSlots[i].data = data;
        //                UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Add(data.GearDataCf.ID);
        //                gearItem.gameObject.SetActive(true);
        //                GearSlots[i].gearItem.gameObject.SetActive(false);
        //            }

        //        }
        //    }
        //}
        //public void AddGrear(GearIteamUI gearItem, GearData data)
        //{
        //    //GearDataCf GearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(data.id);
        //    //for (int i = 0; i < GearSlots.Count; i++)
        //    //{
        //    //    //if ((int)gearItem.data.Rarity == GearSlots[i].indexslot)
        //    //    if ((int)GearDataCf.WeaponType == GearSlots[i].indexslot)
        //    //    {

        //    //        if (GearSlots[i].gearIteamUI == null)
        //    //        {
        //    //            if (UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Count > 5) return;
        //    //            GearSlots[i].icoinGear.sprite = HeroAssets.Instance.GetSprite(GearDataCf.Name);
        //    //            GearSlots[i].data = data;
        //    //            GearSlots[i].gearIteamUI = gearItem;
        //    //            data.slot = 1;
        //    //            UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Add(GearDataCf.ID);
        //    //            gearItem.gameObject.SetActive(false);
        //    //        }
        //    //        else
        //    //        {
        //    //            UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Remove(GearSlots[i].data.GearDataCf.ID);
        //    //            GearIteamUI gear = GearSlots[i].gearIteamUI;
        //    //            GearSlots[i].gearIteamUI = gearItem;
        //    //            gearItem = gear;
        //    //            GearSlots[i].icoinGear.sprite = HeroAssets.Instance.GetSprite(GearSlots[i].gearIteamUI.data.GearDataCf.Name);
        //    //            GearSlots[i].data = data;
        //    //            UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Add(data.GearDataCf.ID);
        //    //            gearItem.gameObject.SetActive(true);
        //    //            GearSlots[i].gearIteamUI.gameObject.SetActive(false);
        //    //        }

        //    //    }
        //    //}
        //}

        public void AddGrear(GearIteamUI gearItem, GearData data)
        {
            GearDataCf GearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(data.id);
            int index = CheckIndexGearOnUI((int)GearDataCf.WeaponType);
            //Debug.Log(index);
            if (index < 0) return;
            data.number--;
            gearItem.numberGearObj.SetActive(true);
            gearItem.numberGearText.text = data.number.ToString();
            if (data.number <= 0)
            {
                gearItem.gameObject.SetActive(false);
                data.slot = 1;
            }
            else if (data.number == 1)
            {
                gearItem.numberGearObj.SetActive(false);
            }
            if (GearSlots[index].gearIteamUI == null)
            {
                UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Add(GearDataCf.ID);

                //data.number--;
                //gearItem.numberGearObj.SetActive(true);
                //gearItem.numberGearText.text = data.number.ToString();
                GearSlots[index].id = GearDataCf.ID;
                GearSlots[index].gearIteamUI = gearItem;
                GearSlots[index].icoinGear.sprite = HeroAssets.Instance.GetSprite(GearDataCf.Name);
                //if (data.number <= 0)
                //{
                //    gearItem.gameObject.SetActive(false);
                //    data.slot = 1;
                //}
                //else if(data.number == 1)
                //{
                //    gearItem.numberGearObj.SetActive(false);
                //}
                UserManager.Instance.SaveHeroData();
            }
            else
            {
                UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Remove(GearSlots[index].id);
                //data.number--;
                //if (data.number <= 0)
                //{
                //    gearItem.gameObject.SetActive(false);
                //    data.slot = 1;

                //}
                //else
                //{
                //    GearSlots[index].gearIteamUI.gameObject.SetActive(true);
                //    GearSlots[index].gearIteamUI.numberGearObj.SetActive(true);
                //    GearSlots[index].gearIteamUI.numberGearText.text =
                //}
                GearIteamUI gear = GearSlots[index].gearIteamUI;
                GearSlots[index].gearIteamUI = gearItem;
                gearItem = gear;
                GearSlots[index].icoinGear.sprite = HeroAssets.Instance.GetSprite(GearDataCf.Name);
                GearSlots[index].id = GearDataCf.ID;

                UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].GearHeros.Add(GearDataCf.ID);
                gearItem.data.number++;
                gearItem.data.slot = -1;
                if (gearItem.data.number == 1)
                {
                    gearItem.gameObject.SetActive(true);
                    gearItem.numberGearObj.SetActive(false);
                }
                else if (gearItem.data.number >= 2)
                {
                    gearItem.numberGearObj.SetActive(true);
                    gearItem.numberGearText.text = gearItem.data.number.ToString();
                }
                else
                {
                    gearItem.gameObject.SetActive(false);
                }




            }
        }
        private int CheckIndexGearOnUI(int index)
        {
            for (int i = 0; i < GearSlots.Count; i++)
            {
                if (GearSlots[i].indexslot == index)
                {
                    return i;
                }
            }
            return -1;
        }
        //public void ShowDetailGear(GearItem gearItem, GearData data)
        //{
        //    GearObject.SetActive(true);
        //    GearObject.GetComponent<GearDetail>().ShowInfor(gearItem,data);
        //}
        public void ShowDetailGear(GearData data, GearIteamUI gearIteamUI)
        {
            GearObject.SetActive(true);
            //GearObject.GetComponent<GearDetail>().ShowInfor(gearIteam, data);
            GearObject.GetComponent<GearDetailUI>().ShowInforGear(data, gearIteamUI);
        }

        public void NextCharacter()
        {
            SoundManager.instance.PressButtonAudio();
            UserManager.Instance.indexHeroUpdate++;
            if (UserManager.Instance.indexHeroUpdate > UserManager.Instance.HeroDatas.heroDatas.Count - 1)
            {
                UserManager.Instance.indexHeroUpdate = 0;
                Debug.Log(UserManager.Instance.indexHeroUpdate);
            }
            Debug.Log(UserManager.Instance.indexHeroUpdate);
            SpawnAvatarHeroOnGear();
            SpawnGearOnHero();
            GetGear();
        }

        public void PreviousCharacter()
        {
            SoundManager.instance.PressButtonAudio();
            UserManager.Instance.indexHeroUpdate--;
            if (UserManager.Instance.indexHeroUpdate < 0)
            {
                UserManager.Instance.indexHeroUpdate = UserManager.Instance.HeroDatas.heroDatas.Count - 1;
            }
            Debug.Log(UserManager.Instance.indexHeroUpdate);
            SpawnAvatarHeroOnGear();
            SpawnGearOnHero();
            GetGear();
        }

        public void ShowHeroShardUpdate()
        {
            SoundManager.instance.PressButtonAudio();
            HeroSharedUpdateObj.SetActive(true);
            HeroSharedUpdateObj.GetComponent<HeroShardUpdate>().InitHeroShard(UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate]);
        }

        public void SetNoticeUpdate()
        {
            if (UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].HeroShard >= 7)
            {
                noticeObj.SetActive(true);
            }
            else
            {
                noticeObj.SetActive(false);
            }
        }

        // gắn gear nhanh
        public void AddGearHighSpeed()
        {
            SoundManager.instance.PressButtonAudio();
            for (int i = 0; i < this.GearSlots.Count; i++)
            {
                this.GearSlots[i].ReturnGear();
            }
            GetGear();
            if (this.gearIteamsSpeed == null || this.gearIteamsSpeed.Count == 0) return;
            for (int i = 0; i < this.gearIteamsSpeed.Count; i++)
            {
                this.gearIteamsSpeed[i].AddGear();
            }
        }
        public void RomoveGearHighSpeed()
        {
            SoundManager.instance.PressButtonAudio();
            for (int i = 0; i < this.GearSlots.Count; i++)
            {
                this.GearSlots[i].ReturnGear();
            }
        }
        public void GetGear()
        {
            this.gearIteamsSpeed.Clear();
            GetGearArmor();
            GetGearHelmet();
            GetGearShield();
            GetGearBoot();
            GetGearGlove();
            GetGearWeaponDamage();
        }
        public void GetGearArmor()
        {
            List<GearIteamUI> gearIteams = new List<GearIteamUI>();
            for (int i = 0; i < this.lsGearItems.Count; i++)
            {
                if (this.lsGearItems[i].data.slot > 0) continue;
                GearDataCf gearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(this.lsGearItems[i].data.id);
                if (gearDataCf.WeaponType == WeaponType.armor)
                {
                    gearIteams.Add(this.lsGearItems[i]);
                }
            }
            if (gearIteams == null || gearIteams.Count == 0) { return; }
            GearIteamUI gear = gearIteams[0];
            foreach (GearIteamUI number in gearIteams)
            {
                GearDataCf gearDataCheckZeroCf = GearDataCtrl.Instance.GetGearDataCfByIndex(gear.data.id);
                GearDataCf gearDataCheckCf = GearDataCtrl.Instance.GetGearDataCfByIndex(number.data.id);
                if (gearDataCheckCf.Def > gearDataCheckZeroCf.Def)
                {
                    gear = number;
                }
            }
            gearIteamsSpeed.Add(gear);
        }
        public void GetGearHelmet()
        {
            List<GearIteamUI> gearIteams = new List<GearIteamUI>();
            for (int i = 0; i < this.lsGearItems.Count; i++)
            {
                if (this.lsGearItems[i].data.slot > 0) continue;
                GearDataCf gearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(this.lsGearItems[i].data.id);
                if (gearDataCf.WeaponType == WeaponType.Helmet)
                {
                    gearIteams.Add(this.lsGearItems[i]);
                }
            }
            if (gearIteams == null || gearIteams.Count == 0) { return; }
            GearIteamUI gear = gearIteams[0];
            foreach (GearIteamUI number in gearIteams)
            {
                GearDataCf gearDataCheckZeroCf = GearDataCtrl.Instance.GetGearDataCfByIndex(gear.data.id);
                GearDataCf gearDataCheckCf = GearDataCtrl.Instance.GetGearDataCfByIndex(number.data.id);
                if (gearDataCheckCf.Hp > gearDataCheckZeroCf.Hp)
                {
                    gear = number;
                }
            }
            gearIteamsSpeed.Add(gear);
        }
        public void GetGearShield()
        {
            List<GearIteamUI> gearIteams = new List<GearIteamUI>();
            for (int i = 0; i < this.lsGearItems.Count; i++)
            {
                if (this.lsGearItems[i].data.slot > 0) continue;
                GearDataCf gearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(this.lsGearItems[i].data.id);
                if (gearDataCf.WeaponType == WeaponType.Shield)
                {
                    gearIteams.Add(this.lsGearItems[i]);
                }
            }
            if (gearIteams == null || gearIteams.Count == 0) { return; }
            GearIteamUI gear = gearIteams[0];
            foreach (GearIteamUI number in gearIteams)
            {
                GearDataCf gearDataCheckZeroCf = GearDataCtrl.Instance.GetGearDataCfByIndex(gear.data.id);
                GearDataCf gearDataCheckCf = GearDataCtrl.Instance.GetGearDataCfByIndex(number.data.id);
                if (gearDataCheckCf.Def > gearDataCheckZeroCf.Def)
                {
                    gear = number;
                }
            }
            gearIteamsSpeed.Add(gear);
        }
        public void GetGearBoot()
        {
            List<GearIteamUI> gearIteams = new List<GearIteamUI>();
            for (int i = 0; i < this.lsGearItems.Count; i++)
            {
                if (this.lsGearItems[i].data.slot > 0) continue;
                GearDataCf gearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(this.lsGearItems[i].data.id);
                if (gearDataCf.WeaponType == WeaponType.Boot)
                {
                    gearIteams.Add(this.lsGearItems[i]);
                }
            }
            if (gearIteams == null || gearIteams.Count == 0) { return; }
            GearIteamUI gear = gearIteams[0];
            foreach (GearIteamUI number in gearIteams)
            {
                GearDataCf gearDataCheckZeroCf = GearDataCtrl.Instance.GetGearDataCfByIndex(gear.data.id);
                GearDataCf gearDataCheckCf = GearDataCtrl.Instance.GetGearDataCfByIndex(number.data.id);
                if (gearDataCheckCf.Speed > gearDataCheckZeroCf.Speed)
                {
                    gear = number;
                }
            }
            gearIteamsSpeed.Add(gear);
        }
        public void GetGearGlove()
        {
            List<GearIteamUI> gearIteams = new List<GearIteamUI>();
            for (int i = 0; i < this.lsGearItems.Count; i++)
            {
                if (this.lsGearItems[i].data.slot > 0) continue;
                GearDataCf gearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(this.lsGearItems[i].data.id);
                if (gearDataCf.WeaponType == WeaponType.Glove)
                {
                    gearIteams.Add(this.lsGearItems[i]);
                }
            }
            if (gearIteams == null || gearIteams.Count == 0) { return; }
            GearIteamUI gear = gearIteams[0];
            foreach (GearIteamUI number in gearIteams)
            {
                GearDataCf gearDataCheckZeroCf = GearDataCtrl.Instance.GetGearDataCfByIndex(gear.data.id);
                GearDataCf gearDataCheckCf = GearDataCtrl.Instance.GetGearDataCfByIndex(number.data.id);
                if (gearDataCheckCf.ATK > gearDataCheckZeroCf.ATK)
                {
                    gear = number;
                }
            }
            gearIteamsSpeed.Add(gear);
        }
        public void GetGearWeaponDamage()
        {
            List<GearIteamUI> gearIteams = new List<GearIteamUI>();
            for (int i = 0; i < this.lsGearItems.Count; i++)
            {
                if (this.lsGearItems[i].data.slot > 0) continue;
                GearDataCf gearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(this.lsGearItems[i].data.id);
                if (gearDataCf.WeaponType == WeaponType.WeaponDamage)
                {
                    gearIteams.Add(this.lsGearItems[i]);
                }
            }
            if (gearIteams == null || gearIteams.Count == 0) { return; }
            GearIteamUI gear = gearIteams[0];
            foreach (GearIteamUI number in gearIteams)
            {
                GearDataCf gearDataCheckZeroCf = GearDataCtrl.Instance.GetGearDataCfByIndex(gear.data.id);
                GearDataCf gearDataCheckCf = GearDataCtrl.Instance.GetGearDataCfByIndex(number.data.id);
                if (gearDataCheckCf.ATK > gearDataCheckZeroCf.ATK)
                {
                    gear = number;
                }
            }
            gearIteamsSpeed.Add(gear);
        }
    }
}

using GuanYu.Battle;
using GuanYu.Hero;
using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;

namespace GuanYu.User
{
    public class DataPlayerController
    {
        public int gold;
        public int star;
        public int point;
        public int levelCampain;
        public int NumberWin;
        public int NumberLose;
        public string Name;
        public DataPlayerController(int gold, int star, int point, int levelCampain, int numberWin, int numberLose, string name)
        {
            this.gold = gold;
            this.star = star;
            this.point = point;
            this.levelCampain = levelCampain;
            this.NumberWin = numberWin;
            this.NumberLose = numberLose;
            this.Name = name;
        }
    }
    [Serializable]
    public class HeroDatas
    {
        public List<HeroData> heroDatas;
    }

    [System.Serializable]
    public class UserData
    {
        public int levelBoss =0;
        public int indexRewardDay = 0;
        public List<int> HeroTeams = new List<int> { 0, 1, -1, -1, -1 };  // intdex của hero trong team
        public List<int> HeroBought = new List<int> { 0, 1 }; // những tướng đã được mua 
        public List<int> GearOnShop = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };// Những gear đã được mởi
    }

    public class UserManager : MonoBehaviour
    {
        public const int MaxSlot = 5;

        public DataPlayerController DataPlayerController;
        public HeroDatas HeroDatas;
        // tính lượng damage cuối cùng 
        public UserData UserData;
        public int indexHeroUpdate = 0;

        public static UserManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            //this.LoadData();
        }

        public HeroData GetHeroData(int index)
        {
            return HeroDatas.heroDatas.Find(a => { return a.Index == index; });

        }

        public HeroDataCf SetdataCf(HeroModel heroModel)
        {
            HeroDataCf data = new HeroDataCf();
            if (heroModel.isHero)
            {
                data = SetHeroDataCf(heroModel.index);
            }
            else
            {
                data = SetEnemyDataCf(heroModel);
            }
            return data;
        }
        public HeroDataCf SetHeroDataCf(int index)
        {
            HeroData heroData = HeroAssets.Instance.GetHeroDataByInDex(index);
            HeroDataCf heroDataCf = HeroAssets.Instance.GetHeroDataCfByIndex(index);
            //Debug.Log("hp" + heroDataCf.Hp);
            HeroDataCf data = new HeroDataCf();
            data.Type = heroDataCf.Type;
            data.SkillType = heroDataCf.SkillType;
            //data.Skill = heroDataCf.Skill;
            data.IndexSkill = heroDataCf.IndexSkill;
            data.Name = heroDataCf.Name;
            data.Index = heroDataCf.Index;
            data.Hp = heroDataCf.Hp;
            data.Def = heroDataCf.Def;
            data.Atk = heroDataCf.Atk;
            data.Speed = heroDataCf.Speed;
            data.GoldLevel = heroDataCf.GoldLevel;
            data.RoleHeroType = heroDataCf.RoleHeroType;
            data.RarityHero = heroDataCf.RarityHero;
            if (heroData.GearHeros != null && heroData.GearHeros.Count > 0)
            {
                for (int i = 0; i < heroData.GearHeros.Count; i++)
                {
                    GearData gearData = GearDataCtrl.Instance.GetGearDataByIndex(heroData.GearHeros[i]);
                    GearDataCf gearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(gearData.id);
                    //data.Hp = (heroDataCf.Hp + gearDataCf.Hp)*heroData.LevelHero;
                    //data.Def = (heroDataCf.Def + gearDataCf.Def) * heroData.LevelHero;
                    //data.Atk = (heroDataCf.Atk + gearDataCf.ATK) * heroData.LevelHero;
                    //data.Speed = (heroDataCf.Speed + gearDataCf.Speed) * heroData.LevelHero;
                    data.Hp += gearDataCf.Hp;
                    data.Def += gearDataCf.Def;
                    data.Atk += gearDataCf.ATK;
                    data.Speed += gearDataCf.Speed;
                }
                data.Hp += (int)((heroData.LevelHero + heroData.star) * 100f);
                data.Def += (int)((heroData.LevelHero + heroData.star) * 100f);
                data.Atk += (int)((heroData.LevelHero + heroData.star) * 100f);
                data.Speed += (int)((heroData.LevelHero + heroData.star) * 100f);
            }
            else //if(heroData.GearHeros == null || heroData.GearHeros.Count == 0)
            {
                //data.Hp = heroDataCf.Hp * heroData.LevelHero;
                //data.Def = heroDataCf.Def * heroData.LevelHero;
                //data.Atk = heroDataCf.Atk * heroData.LevelHero;
                //data.Speed = heroDataCf.Speed * heroData.LevelHero;
                data.Hp = (int)(heroDataCf.Hp + (heroData.LevelHero + heroData.star) * 100f);
                data.Def = (int)(heroDataCf.Def + (heroData.LevelHero + heroData.star) * 100f);
                data.Atk = (int)(heroDataCf.Atk + (heroData.LevelHero + heroData.star) * 100f);
                data.Speed = (int)(heroDataCf.Speed + (heroData.LevelHero + heroData.star) * 100f);
            }
            //Debug.Log(data.Hp);
            return data;
        }
        public HeroDataCf SetEnemyDataCf(HeroModel heroModel)
        {
            int level;
            //if (BattleController.instance.campainType == CampainType.Campain)
            //{
            //    level = DataPlayerController.levelCampain -1;
            //}
            //else
            //{
            //    if (DataPlayerController.point <= 100)
            //    {
            //        level = 0;
            //    }else
            //    {
            //        level = (int)DataPlayerController.point / 100;
            //    }
            //}
            if (heroModel.typeHero == -1)
            {
                level = DataPlayerController.levelCampain - 1;
            }
            else if (heroModel.typeHero == 0)
            {
                if (DataPlayerController.point <= 100)
                {
                    level = 0;
                }
                else
                {
                    level = (int)DataPlayerController.point / 100;
                }
            }
            else
            {
                level = UserData.levelBoss;
            }

            HeroDataCf enemyDataCf = HeroAssets.Instance.GetEnemyDataCfByIndex(heroModel.index);
            HeroDataCf data = new HeroDataCf();
            data.Index = enemyDataCf.Index;
            data.Type = enemyDataCf.Type;
            data.SkillType = enemyDataCf.SkillType;
            //data.Skill = enemyDataCf.Skill;
            data.IndexSkill = enemyDataCf.IndexSkill;
            data.Name = enemyDataCf.Name;
            data.isBoss = enemyDataCf.isBoss;

            data.Hp = (int)(enemyDataCf.Hp + enemyDataCf.Hp * level * 0.1f);
            data.Def = (int)(enemyDataCf.Def + enemyDataCf.Def * level * 0.1f);
            data.Atk = (int)(enemyDataCf.Atk + enemyDataCf.Atk * level * 0.1f);
            data.Speed = (int)(enemyDataCf.Speed + enemyDataCf.Speed * level * 0.1f);
            return data;
        }

        public void LoadData()
        {
            this.UserData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString("UserManager:UserData", JsonUtility.ToJson(new UserData()).ToString()));

            string jsonDataPlayerController = PlayerPrefs.GetString("dataPlayerController");

            if (!string.IsNullOrEmpty(jsonDataPlayerController))
            {
                DataPlayerController = JsonUtility.FromJson<DataPlayerController>(jsonDataPlayerController);
            }
            else
            {
                DataPlayerController = new DataPlayerController(10000, 5, 100, 1, 0, 0, "Player");
                SaveData();
                Debug.LogWarning("No saved player data found.");
            }

            string jsonData = PlayerPrefs.GetString("PlayerData");
            Debug.Log(jsonData);
            if (!string.IsNullOrEmpty(jsonData))
            {

                HeroDatas = JsonUtility.FromJson<HeroDatas>(jsonData);
                Debug.LogWarning("No saved 1 ");
            }
            else
            {
                Debug.LogWarning("No saved ");
                for (int i = 0; i < UserData.HeroBought.Count; i++)
                {
                    HeroData hero = new HeroData(UserData.HeroBought[i]);
                    HeroDatas.heroDatas.Add(hero);
                }
                SaveHeroData();
            }
        }
        public void SaveData()
        {
            PlayerPrefs.SetString("UserManager:UserData", JsonUtility.ToJson(this.UserData).ToString());
            string jsonDataPlayerController = JsonUtility.ToJson(DataPlayerController);
            PlayerPrefs.SetString("dataPlayerController", jsonDataPlayerController);
        }

        public void SaveHeroData()
        {

            string jsonData = JsonUtility.ToJson(this.HeroDatas);
            PlayerPrefs.SetString("PlayerData", jsonData);
        }

        public bool SetTeam(int index)
        {
            foreach (int item in this.UserData.HeroTeams)
            {
                if (item == index) return false;
            }
            for (int i = 0; i < this.UserData.HeroTeams.Count; i++)
            {
                if (this.UserData.HeroTeams[i] < 0)
                {
                    this.UserData.HeroTeams[i] = index;
                    this.SaveData();
                    return true;
                }
            }
            if (this.UserData.HeroTeams.Count >= MaxSlot)
            {
                return false;
            }
            //this.UserData.HeroTeams.Add(index);
            //this.SaveData();
            return true;
        }

        public int ReturnTeam(int index)
        {
            int a = 0;
            for (int i = 0; i < this.UserData.HeroTeams.Count; i++)
            {
                if (this.UserData.HeroTeams[i] == index)
                {
                    a = i;
                    break;
                }
            }
            return a;
        }

        public bool Unselect(int slot)
        {
            if (slot >= this.UserData.HeroTeams.Count) return false;
            this.UserData.HeroTeams[slot] = -1;
            this.SaveData();
            return true;
        }

        //public void SelectTranning(int index)
        //{
        //    this.UserData.HeroTraining = index; this.SaveData();
        //}


        // kiểm tra theo cách cũ
        //public bool IsInTeam(int index)
        //{
        //    return UserData.HeroTeams.IndexOf(index) < 0 ? false : true;
        //}

        public bool IsInTeam(int index)
        {
            for (int i = 0; i < UserData.HeroTeams.Count; i++)
            {
                if (UserData.HeroTeams[i] == index)
                    return true;
            }
            return false;
        }
    }
}


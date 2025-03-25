
using GuanYu.User;
using NTPackage.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GuanYu.Hero
{

    [System.Serializable]
    public class RankData
    {

        public string Name;
        public int point;

    }
    [System.Serializable]
    public class RankDatas
    {
        public List<RankData> rankdatas;
    }

    [System.Serializable]
    public class HeroDataCfs
    {
        public List<HeroDataCf> HeroDatas;
    }

    [Serializable]
    public class AchivementDataCf
    {
        public int ID;
        public string Name;
        public string NameTask;
        public List<int> Level;
        public List<int> Gift;
        //public AchivementDataCf(AchivementDataCf other)
        //{
        //    if (other == null)
        //    {
        //        throw new ArgumentNullException(nameof(other));
        //    }
        //    ID = other.ID;
        //    Name = other.Name;
        //    NameTask = other.NameTask;
        //    Level = new List<int>(other.Level); // Tạo một bản sao của danh sách Level
        //}
    }

    [Serializable]
    public class AchivementDataCfs
    {
        public List<AchivementDataCf> AchivementDatas;
    }
    [Serializable]
    public class NamePlayerData
    {
        public string Name;
        public int Id;
    }

    [Serializable]
    public class NamePlayerDataCfs
    {
        public List<NamePlayerData> NamePlayerDatas;
    }

    public class HeroAssets : MonoBehaviour
    {
        [Header("Text")]
        public TextAsset rankDataText;
        public TextAsset HeroDataText;
        public TextAsset EnemyDataText;
        public TextAsset GearDataText;
        public TextAsset NamePlayerText;
        public TextAsset ChestDataText;
        [SerializeField] private TextAsset AcivementText;

        public NTDictionary<HeroDataCf> HeroDataCfDic;
        public HeroDataCfs EnemyDataCfs;
        public GearDataCfs GearDataCfs;
        public HeroDataCfs heroDataCfs;
        public NamePlayerDataCfs NamePlayerDataCfs;
        public ChestDataCfs ChestDataCfs;
        public List<GameObject> HeroPreb;

        public RankDatas RankDatas;
        public ChestDatas ChestDatas;
        //public List<GearData> gearDatas;
        public static HeroAssets Instance;
        public List<Sprite> imageWeapon;
        public NTDictionary<Sprite> imageWeaponDic = new NTDictionary<Sprite>();
        public AchivementDataCfs AchivementDataCfs;

        public List<HeroDataCf> EnemyDatas;
        public List<HeroDataCf> BossDatas;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            LoadImage();
            LoadData();

        }

        private void Start()
        {
            //LoadData();
        }
        private void LoadImage()
        {
            imageWeapon = Resources.LoadAll<Sprite>("Equipment").ToList();
            foreach (Sprite image in imageWeapon)
            {
                try
                {
                    imageWeaponDic.Add(image.name, image);
                }
                catch (System.Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
        public Sprite GetSprite(string name)
        {
            //Debug.Log(name);
            return imageWeaponDic.Get(name);
        }
        public void LoadData()
        {
            //HeroDataCfs heroDataCfs = JsonUtility.FromJson<HeroDataCfs>(this.HeroDataText.text);
            this.heroDataCfs = JsonUtility.FromJson<HeroDataCfs>(this.HeroDataText.text);
            this.HeroDataCfDic = new NTDictionary<HeroDataCf>();
            foreach (HeroDataCf item in this.heroDataCfs.HeroDatas)
            {
                this.HeroDataCfDic.Add(item.Index.ToString(), item);
            }
            this.EnemyDataCfs = JsonUtility.FromJson<HeroDataCfs>(this.EnemyDataText.text);
            ChooseEnemyData();
            this.GearDataCfs = JsonUtility.FromJson<GearDataCfs>(this.GearDataText.text);
            this.RankDatas = JsonUtility.FromJson<RankDatas>(this.rankDataText.text);
            this.NamePlayerDataCfs = JsonUtility.FromJson<NamePlayerDataCfs>(this.NamePlayerText.text);
            this.AchivementDataCfs = JsonUtility.FromJson<AchivementDataCfs>(this.AcivementText.text);
            this.ChestDataCfs = JsonUtility.FromJson<ChestDataCfs>(this.ChestDataText.text);
            SortRank();
            string jsonChestData = PlayerPrefs.GetString("ChestData");
            Debug.Log(jsonChestData);
            if (!string.IsNullOrEmpty(jsonChestData))
            {

                ChestDatas = JsonUtility.FromJson<ChestDatas>(jsonChestData);
                Debug.LogWarning("No saved 1 ");
            }  
            else
            {
                Debug.LogWarning("No saved ");
                for (int i = 0; i < 5; i++)
                {
                    ChestData chestData = new ChestData(i);
                    ChestDatas.datas.Add(chestData);
                }
                SaveChestData();
            }
        }
        public void ChooseEnemyData()
        {
            for (int i = 0; i < this.EnemyDataCfs.HeroDatas.Count; i++)
            {
                if (this.EnemyDataCfs.HeroDatas[i].isBoss)
                {
                    BossDatas.Add(this.EnemyDataCfs.HeroDatas[i]);
                }
                else
                {
                    EnemyDatas.Add(this.EnemyDataCfs.HeroDatas[i]);
                }
            }
        }
        public void SaveChestData()
        {
            string jsonChestData = JsonUtility.ToJson(this.ChestDatas);
            PlayerPrefs.SetString("ChestData", jsonChestData);
        }

        public GearData GearDatabyIndex(int index)
        {
            return GearDataCtrl.Instance.GearDatas.gearDatas.Find(a => { return a.id == index; });
        }
        public HeroDataCf GetHeroDataCfByIndex(int index)
        {
            return this.HeroDataCfDic.Get(index.ToString());

        }
        public HeroData GetHeroDataByInDex(int index)
        {
            return UserManager.Instance.HeroDatas.heroDatas.Find(a => { return a.Index == index; });
        }

        public ChestDataCf GetChestDataCfById(int id)
        {
            return this.ChestDataCfs.ChestDatas.Find(a => { return a.Id == id; });
        }

        public HeroDataCf GetEnemyDataCfByIndex(int index)
        {
            return this.EnemyDataCfs.HeroDatas.Find(a => { return a.Index == index; });
            //return this.EnemyDatas.Find(a=>{ return a.Index == index; });
        }
        public HeroDataCf GetBossDataCfByIndex(int index)
        {
            //return this.EnemyDataCfs.HeroDatas.Find(a => { return a.Index == index; });
            return this.BossDatas.Find(a => { return a.Index == index; });
        }
        public GameObject GetHeroUIPrefabByIndex(int index)
        {
            GameObject avaPreb = Resources.Load<GameObject>("HeroUI/No" + index);
            if (avaPreb != null)
            {
                return Instantiate(avaPreb);
            }
            Debug.LogError("Not found:" + index);
            return null;
        }

        public GameObject GetEnemyUIPrefabByIndex(int index)
        {
            GameObject avaPreb = Resources.Load<GameObject>("EnemyUI/No" + index);
            if (avaPreb != null)
            {
                return Instantiate(avaPreb);
            }
            Debug.LogError("Not found:" + index);
            return null;
        }
        public GameObject GetAvatarHeroPrefabByIndex(int index)
        {
            GameObject avaPreb = Resources.Load<GameObject>("HeroUI/No" + index).GetComponent<HeroAvatar>().datahero.avatar.gameObject;
            if (avaPreb != null)
            {
                Debug.Log("spwan ra avartar");
                return Instantiate(avaPreb);
            }
            Debug.LogError("Not found:" + index);
            return null;
        }

        public GameObject GetAvatar(HeroModel heromodel)
        {
            GameObject avaPreb = heromodel.No.GetComponent<HeroAvatar>().datahero.avatar.gameObject;
            if (avaPreb != null)
            {
                return Instantiate(avaPreb);
            }
            return null;
        }

        public void SortRank()
        {
            int n = RankDatas.rankdatas.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (RankDatas.rankdatas[j].point < RankDatas.rankdatas[j + 1].point)
                    {
                        RankData temp = RankDatas.rankdatas[j];
                        RankDatas.rankdatas[j] = RankDatas.rankdatas[j + 1];
                        RankDatas.rankdatas[j + 1] = temp;
                    }
                }
            }
        }
        [ContextMenu("AddPlaye")]
        public void AddPlayer()
        {
            // RankData rankData = new RankData(UserManager.Instance.DataPlayerController.Name,UserManager.Instance.DataPlayerController.point);
            RankData rankData = new RankData
            {
                //Name = "abc",
                //point = 6000
                Name = UserManager.Instance.DataPlayerController.Name,
                point = UserManager.Instance.DataPlayerController.point,
            };

            this.RankDatas.rankdatas.Add(rankData);
            SortRank();
            //UIManager.instance.battleUI.LoadArena();
            Debug.Log(rankData.point);

        }

        public RankData RankDataPlayer()
        {
            RankData data = new RankData();
            for (int i = 0; i < RankDatas.rankdatas.Count; i++)
            {
                if (RankDatas.rankdatas[i].Name == UserManager.Instance.DataPlayerController.Name)
                {
                    data = RankDatas.rankdatas[i];
                    return data;
                }
            }
            return data;
        }
        public int GetIndexHeroUpdate(int index)
        {
            for (int i = 0; i < UserManager.Instance.HeroDatas.heroDatas.Count; i++)
            {
                if (UserManager.Instance.HeroDatas.heroDatas[i].Index == index)
                {
                    return i;
                }
            }
            return -1;
        }

        //  public void SaveDateAchivement()
        //{
        //    AchivementDatas achivementDatas = new AchivementDatas();
        //    achivementDatas.achivementDatas = this.AchivementDatasDict.ToList();
        //    string jsonDataAchivement = JsonUtility.ToJson(achivementDatas);
        //    PlayerPrefs.SetString("AchivementDatas", jsonDataAchivement);
        //}
    }
}
using GuanYu.Hero;
using GuanYu.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GuanYu
{
    public enum AchivementType
    {
        FindMonters,
        FindEquipment,
        WinBattles,
        DefeatMonster,
        ReachTrophiesMilestone,
        EndlesWavesDefeated
    }

    [Serializable]
    public class AchivementData
    {
        public int ID;
        public int levelClaim;
        public int number; // số của những phần tử đã vượt qua trong nhiệm vụ
        public bool isDone;
        public AchivementData(int id)
        {
            this.ID = id;
            this.levelClaim = 0;
            this.number = 0;
            this.isDone = false;
        }
    }

    [Serializable]
    public class AchivementDatas
    {
        public List<AchivementData> achivementDatas;
    }

    public class AchivementDataCtrl : MonoBehaviour
    {
        public static AchivementDataCtrl Instance;
        public AchivementDatas AchivementDatas;

        public void DoneAchiveMent(AchivementType achivementType)
        {
            AchivementData achivementData = GetAchivementDataByID((int)achivementType);
            AchivementDataCf achivementDataCf = GetAchivementDataCfByID((int)achivementType);
            achivementData.number += 1;
            if (achivementData.number > achivementDataCf.Level[achivementData.levelClaim])
            {
                achivementData.number = achivementDataCf.Level[achivementData.levelClaim];
            }
            SaveDateAchivement();
        }
        public AchivementDataCf GetAchivementDataCfByID(int ID)
        {
            return HeroAssets.Instance.AchivementDataCfs.AchivementDatas.Find(a => { return a.ID == ID; });
        }
        public void TEST()
        {
            string.Format("find {0} Monters", 0);
        }
        private void Awake()
        {
            if (Instance == null) Instance = this;
        }
        private void Start()
        {
            LoadDataAchivement();
        }
       
        public void LoadDataAchivement()
        {

            string jsonDataAchivement = PlayerPrefs.GetString("AchivementDatas");
            Debug.Log(jsonDataAchivement);
            if (!string.IsNullOrEmpty(jsonDataAchivement))
            {

                this.AchivementDatas = JsonUtility.FromJson<AchivementDatas>(jsonDataAchivement);
                Debug.LogWarning("No saved 1 ");
            }
            else
            {
                Debug.LogWarning("No saved ");
                for (int i = 0; i < HeroAssets.Instance.AchivementDataCfs.AchivementDatas.Count; i++)
                {
                    AchivementData achivement = new AchivementData(i);
                    AchivementDatas.achivementDatas.Add(achivement);
                    Debug.Log("so achivement" + i);
                }
                SaveDateAchivement();
            }
        }

        public void SaveDateAchivement()
        {
            string jsonDataAchivement = JsonUtility.ToJson(this.AchivementDatas);
            PlayerPrefs.SetString("AchivementDatas", jsonDataAchivement);
        }

        public AchivementData GetAchivementDataByID(int index)
        {
            return this.AchivementDatas.achivementDatas.Find(a => { return a.ID == index; });
        }

        public bool Notice()
        {
            for (int i = 0; i < this.AchivementDatas.achivementDatas.Count; i++)
            {
                AchivementDataCf AchivementDataCf = GetAchivementDataCfByID(this.AchivementDatas.achivementDatas[i].ID);
                if (this.AchivementDatas.achivementDatas[i].number >= AchivementDataCf.Level[this.AchivementDatas.achivementDatas[i].levelClaim])
                {
                    return true;
                }
            }
            return false;
        }

    }
}

using System.Collections.Generic;
using UnityEngine;

namespace GuanYu.Battle
{
    //    {
    //        [System.Serializable]
    //    public class HeroDataCampain
    //    {
    //        public int Index;
    //        public int Lv;
    //        public int Exp;
    //    }

    //[System.Serializable]
    //public class BattleWave
    //{
    //    public List<WaveData> WaveDatas;
    //}

    //[System.Serializable]
    //public class WaveData
    //{
    //    public List<HeroDataCampain> HeroDatas;
    //    public int Gold;
    //}

    [System.Serializable]
    public class HeroDataCampain
    {
        public List<int> Index;
        public int Gold;
    }
    [System.Serializable]
    public class WaveData
    {
        public List<HeroDataCampain> WaveDatas;
    }
    [System.Serializable]
    public class LevelData
    {
        public List<WaveData> LevelDatas;
    }

    [System.Serializable]
    public class HeroDataArena
    {
        public List<int> Index;
    }
    [System.Serializable]
    public class StateDataArena
    {
       public List<HeroDataArena> HeroDataArenas;
    }

    public class BattleCf : MonoBehaviour
    {
        //public BattleWave BattleWave;
        //public TextAsset WaveDataText;

        //public static BattleCf Instance;

        //private void Awake()
        //{
        //    if (Instance == null)
        //        Instance = this;
        //}
        //private void Start()
        //{
        //    LoadData();
        //}
        //public void LoadData()
        //{
        //    this.BattleWave = JsonUtility.FromJson<BattleWave>(WaveDataText.text);

        //}
        public static BattleCf Instance;

        public LevelData levelData;
        public TextAsset LevelDataText;
        public TextAsset HeroDataArenaText;
        public StateDataArena stateDataArena;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            LoadData();
        }

        private void Start()
        {
            //LoadData();
        }
        public void LoadData()
        {
            this.levelData = JsonUtility.FromJson<LevelData>(LevelDataText.text);
            this.stateDataArena = JsonUtility.FromJson<StateDataArena>(HeroDataArenaText.text);
        }
    }
}
using System;
using System.Collections.Generic;

namespace GuanYu
{
    [Serializable]
    public enum ChestType
    {
        Common = 0, //ruowung báu phổ thông
        Prenium = 1, // rương báu cao cấp
        Rare = 2,  //rương báu siêu cấp
        Epic = 3, //rương báu bậc thầy
        //Legendary = 4 //rương báu vương giả
    }
    [Serializable]
    public class ChestData
    {
        public int Id;
        //public string skin;
        public int number;
        public ChestData(int Id)
        {
            this.Id = Id;
            this.number = 15;
        }
    }
    [Serializable]
    public class ChestDatas
    {
        public List<ChestData> datas;
    }

    [Serializable]
    public class ChestDataCf
    {
        public int Id;
        public string skin;
        public string name;
        public ChestType type;
    }
    [Serializable]
    public class ChestDataCfs
    {
        public List<ChestDataCf> ChestDatas;
    }
}


using System.Collections.Generic;

namespace GuanYu.Hero
{
    // loai tướng
    [System.Serializable]
    public enum RoleHeroType 
    {
        Ad,
        Support,
        Mid,
        Jungle,
        Top
    }

    [System.Serializable]
    public enum HeroType
    {
        Melee,
        Row,
        //Bow,
        //Idle
    }
    [System.Serializable]
    public enum Skill
    {
        IdleSkill,
        MoveSkill
    }
    [System.Serializable]
    public enum SkillType
    {
        TwoEnemystandinginthefirstrow,
        ThreeEnemystandinginthesencondrow,
        AllEnemy
    }
    //[System.Serializable]

    //public enum HeroLocation
    //{
    //    OnUI,
    //    OnTeam,
    //    OnBattle
    //}

    //[System.Serializable]
    //public class HeroData
    //{
    //    public int Index;
    //    public string Name;
    //    public int LevelHero;
    //    public int slot;
    //    public float CurExp;
    //    public List<int> GearHeros;
    //    public int Hp;
    //    public int Atk;
    //    public int Def;
    //    public int Speed;
    //    public HeroData(int index)
    //    {
    //        this.Index = index;
    //        this.LevelHero = 1;
    //        this.slot = -1;
    //        this.CurExp = 0;
    //        this.GearHeros = new List<int>();
    //        this.Name = HeroAssets.Instance.HeroDataCfs.HeroDatas[index].Name.ToString();
    //    }

    //    public void UpdateStart()
    //    {
    //        this.Hp = HeroAssets.Instance.HeroDataCfs.HeroDatas[this.Index].Hp;
    //        this.Atk = HeroAssets.Instance.HeroDataCfs.HeroDatas[this.Index].Atk;
    //        this.Def = HeroAssets.Instance.HeroDataCfs.HeroDatas[this.Index].Def;
    //        this.Speed = HeroAssets.Instance.HeroDataCfs.HeroDatas[this.Index].Speed;

    //        for (int i = 0; i < GearController.instance.GearSlots.Count; i++)
    //        {
    //            //this.Hp += GearController.instance.lsGearItems[GearHeros[i]].data.GearDataCf.Hp;
    //            //this.Atk += GearController.instance.lsGearItems[GearHeros[i]].data.GearDataCf.ATK;
    //            //this.Def += GearController.instance.lsGearItems[GearHeros[i]].data.GearDataCf.Def;
    //            //this.Speed += GearController.instance.lsGearItems[GearHeros[i]].data.GearDataCf.Speed;
    //            this.Hp += GearController.instance.GearSlots[i].data.GearDataCf.Hp;
    //            this.Atk += GearController.instance.GearSlots[i].data.GearDataCf.ATK;
    //            this.Def += GearController.instance.GearSlots[i].data.GearDataCf.Def;
    //            this.Speed += GearController.instance.GearSlots[i].data.GearDataCf.Speed;
    //        }
    //    }
    //}

    [System.Serializable]
    public class HeroData
    {
        public int Index;
        public int LevelHero;
        public int slot;
        public float CurExp;
        public List<int> GearHeros;
        public int HeroShard; // mảnh tướng 
        public int star;
        //public HeroDataCf heroDataCf;
        public HeroData(int index)
        {
            this.Index = index;
            this.LevelHero = 1;
            this.slot = -1;
            this.CurExp = 0;
            this.GearHeros = new List<int>();
            this.HeroShard = 0;
            this.star = 1;
            //this.heroDataCf = HeroAssets.Instance.GetHeroDataCfByIndex(index);
        }
    }
    [System.Serializable]
    public class HeroDataCf
    {
        public int Index;
        public string Name;
        public int Atk;
        public int Def;
        public int Hp;
        public int Speed;
        public HeroType Type; // Melee,Row,Bow,Idle,
        public SkillType SkillType; // đánh hàng 1 hàng 2 hoặc all
        public int IndexSkill;
        //public Skill Skill; // đứng yên or move
        public Rarity RarityHero;
        public List<int> GoldLevel; // 0 => 5
        public RoleHeroType RoleHeroType;
        public bool isBoss;
    }
}

using GuanYu.Hero;
using GuanYu.User;
using System.Collections.Generic;
using UnityEngine;

namespace GuanYu
{
    [System.Serializable]
    public enum CampainType
    {
        //Arena = 0,
        //Campain = 1,
        //Idle = 3,
        Home = 0,
        Hero = 1,
        Campain = 2,
        Gear = 3,
        Arena = 4
    }
    public class BattleController : MonoBehaviour
    {
        public static BattleController instance;
        public BattleCampain battleCampain;
        public BattleArena battleArena;
        public HeroModel HeroModelPre;
        public List<HeroModel> Heros = new List<HeroModel>();
        public List<HeroModel> Enemies = new List<HeroModel>();
        public List<HeroModel> TurnHeros = new List<HeroModel>();
        public List<HeroModel> TargetSkills = new List<HeroModel>();

        public List<HeroModel> ListTurnHeroSkip = new List<HeroModel>();
        //public bool isAttacking = false;
        [SerializeField] private GameObject Point;
        public int indexTurn = 0;  // luowjt hero danh
        private int indexTurnSkip = 0;
        public Vector3 TargetPos; // khoảng cách di chuyển đến 
        public bool isCampain = false;
        public bool isWin = false;
        public bool isEndGame = false;
        public int wave = 0;
        public int SpeedGame = 1;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {

        }
        //public void SpwanHero(List<Transform> HeroPos)
        //{
        //    //this.isAttacking = false;
        //    for (int i = 0; i < HeroPos.Count; i++)
        //    {
        //        MyFunction.ClearChild(HeroPos[i]);
        //    }
        //    Heros.Clear();
        //    if (UserManager.Instance.UserData.HeroTeams.Count > HeroPos.Count) return;
        //    for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
        //    {

        //        if (UserManager.Instance.UserData.HeroTeams[i] < 0) continue;
        //        HeroModel hero = Instantiate(HeroModelPre, HeroPos[i]);
        //        hero.isHero = true;
        //        Heros.Add(hero);
        //        hero.transform.SetParent(HeroPos[i], false);
        //        hero.InitHeroBattle(UserManager.Instance.UserData.HeroTeams[i], HeroPos[i].transform);
        //    }
        //}
        public void UpdateHp(HeroModel heroModel)
        {
            if (heroModel.typeHero == -1)
            {
                battleCampain.GetHpEnemy();
            }
            else if (heroModel.typeHero == 0)
            {
                UIManager.instance.arenaUI.GetHpHeroArena(this.battleArena.GetHpHeroArena(), this.battleArena.GetCurHpHeroArena());
                UIManager.instance.arenaUI.GetHpEnemyArena(this.battleArena.GetHpEnemyArena(), this.battleArena.GetCurHpEnemyArena());
            }
        }
        public void Getturn()
        {
            int indexturn = 0;
            while (indexturn < 8)
            {
                indexturn++;
                for (int i = 0; i < this.Heros.Count; i++)
                {
                    if (Heros[i].CurHP <= 0) continue;
                    this.TurnHeros.Add(Heros[i]);
                }
                for (int i = 0; i < this.Enemies.Count; i++)
                {
                    if (Enemies[i].CurHP <= 0) continue;
                    this.TurnHeros.Add(Enemies[i]);
                }
            }
        }



        public void RemoveTurn(HeroModel heroModel)
        {
            if (heroModel.typeHero != 0) return;
            //for (int i = TurnHeros.Count - 1; i >= indexTurn; i--)
            //{
            //    if (TurnHeros[i] == heroModel)
            //    {
            //        TurnHeros.RemoveAt(i);
            //    }
            //}
            for (int i = battleArena.TurnHeroArenas.Count - 1; i > battleArena.indexTurn; i--)
            {
                if (battleArena.TurnHeroArenas[i] == heroModel)
                {
                    battleArena.TurnHeroArenas.RemoveAt(i);
                }
            }
        }

        public void RemoveTurnSkip(HeroModel heroModel)
        {
            for (int i = this.ListTurnHeroSkip.Count - 1; i >= indexTurnSkip; i--)
            {
                if (this.ListTurnHeroSkip[i] == heroModel)
                {
                    this.ListTurnHeroSkip.RemoveAt(i);
                }
            }
        }
        //public void Combatt()
        //{
        //    StopAllCoroutines();
        //    if (HeroAttack != null)
        //    {
        //        StopCoroutine(HeroAttack);
        //    }
        //    HeroAttack = StartCoroutine(Attack());
        //    Debug.Log("combat");
        //}

        //public void StopComBat()
        //{
        //    if (HeroAttack != null)
        //    {
        //        StopCoroutine(HeroAttack);
        //    }
        //}
        //Coroutine HeroAttack;

        ////public bool IsNextTurn;
        //IEnumerator Attack()
        //{
        //    isEndGame = false;
        //    indexTurn = 0;
        //    yield return new WaitForSeconds(0.5f);
        //    for (int i = 0; i < this.TurnHeros.Count; i++)
        //    {
        //        //yield return new WaitUntil(() => IsNextTurn); this.IsNextTurn = false;
        //        if (this.TurnHeros[i] == null || this.TurnHeros[i].CurHP <= 0) continue;
        //        this.TurnHeros[i].Attack();
        //        yield return new WaitUntil(() => !IsAttackingFinished());
        //        CheckWinLoseCombat(TurnHeros[i]);
        //        if (isEndGame) break;
        //        yield return new WaitForSeconds(0.3f);
        //        indexTurn++;
        //        if (indexTurn >= this.TurnHeros.Count - 10)
        //        {
        //            Getturn();
        //        }
        //    }
        //}
        //public bool IsAttackingFinished()
        //{
        //    //return isAttacking;
        //    for (int i = 0; i < this.Heros.Count; i++)
        //    {
        //        if (Heros[i].isDone == true) return true;
        //    }
        //    for (int i = 0; i < this.Enemies.Count; i++)
        //    {
        //        if (Enemies[i].isDone == true) return true;
        //    }
        //    return false;
        //}

        //public List<HeroModel> GetTarget(HeroModel heroModel)
        //{
        //    HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(heroModel);
        //    this.TargetSkills.Clear();
        //    //List<HeroModel> heroCheck = heroModel.isHero ? Enemies : Heros;
        //    List<HeroModel> heroCheck = new List<HeroModel>();
        //    if (heroModel.typeHero == -1)
        //    {
        //        heroCheck = heroModel.isHero ? Enemies : Heros;
        //    }
        //    else 
        //    {
        //        heroCheck = heroModel.isHero ? Enemies : Heros;
        //    }
        //    if (heroModel.HeroSpin.State == Hero.AnimationState.Attack)
        //    {
        //        for (int i = 0; i < heroCheck.Count; i++)
        //        {
        //            if (heroCheck[i] == null || heroCheck[i].CurHP <= 0) continue;
        //            this.TargetSkills.Add(heroCheck[i]);
        //            if (heroModel.isHero)
        //            {
        //                this.TargetPos = new Vector3(this.TargetSkills[0].transform.position.x - Contans.DistanceATK, this.TargetSkills[0].transform.position.y, this.TargetSkills[0].transform.position.z);
        //            }
        //            else
        //            {
        //                this.TargetPos = new Vector3(this.TargetSkills[0].transform.position.x + Contans.DistanceATK, this.TargetSkills[0].transform.position.y, this.TargetSkills[0].transform.position.z);
        //            }
        //            return this.TargetSkills;
        //        }
        //    }
        //    else if (heroModel.HeroSpin.State == Hero.AnimationState.Skill)
        //    {
        //        if (campainType == CampainType.Campain)
        //        {
        //            TargetPos = heroModel.isHero ? battleCampain.skillHeroPos.position : battleCampain.skillEnemyPos.position;
        //        }
        //        else if (campainType == CampainType.Arena)
        //        {
        //            TargetPos = heroModel.isHero ? battleArena.skillHeroPos.position : battleArena.skillEnemyPos.position;
        //        }
        //        switch (heroDataCf.SkillType)
        //        {

        //            case SkillType.AllEnemy:
        //                for (int i = 0; i < heroCheck.Count; i++)
        //                {
        //                    if (heroCheck[i] != null && heroCheck[i].CurHP > 0)
        //                    {
        //                        this.TargetSkills.Add(heroCheck[i]);
        //                    }
        //                }
        //                break;
        //            case SkillType.TwoEnemystandinginthefirstrow:
        //                for (int i = 0; i < heroCheck.Count; i++)
        //                {
        //                    if (i < 2 && heroCheck[i].CurHP > 0)
        //                    {
        //                        this.TargetSkills.Add(heroCheck[i]);
        //                    }
        //                    if (i >= 2 && heroCheck[0].CurHP <= 0 && heroCheck[1].CurHP <= 0 && heroCheck[i].CurHP > 0)
        //                    {
        //                        this.TargetSkills.Add(heroCheck[i]);
        //                    }
        //                }
        //                break;
        //            case SkillType.ThreeEnemystandinginthesencondrow:
        //                bool check = false;
        //                for (int i = 2; i < heroCheck.Count; i++)
        //                {
        //                    if (heroCheck[i].CurHP > 0)
        //                    {
        //                        this.TargetSkills.Add(heroCheck[i]);
        //                        check = true;
        //                    }
        //                }
        //                if (!check)
        //                {
        //                    for (int i = 0; i < 2 && i < heroCheck.Count; i++)
        //                    {
        //                        if (heroCheck[i].CurHP > 0)
        //                        {
        //                            this.TargetSkills.Add(heroCheck[i]);
        //                        }
        //                    }
        //                }
        //                break;
        //        }
        //    }
        //    return this.TargetSkills;
        //}
        public List<HeroModel> GetTarget(HeroModel heroModel)
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(heroModel);
            List<HeroModel> TargetSkills = new List<HeroModel>();
            List<HeroModel> heroCheck = new List<HeroModel>();
            if (heroModel.typeHero == -1)
            {
                heroCheck = heroModel.isHero ? battleCampain.EnemyCampains : battleCampain.HeroCampains;
            }
            else if (heroModel.typeHero == 0)
            {
                heroCheck = heroModel.isHero ? battleArena.EnemyArenas : battleArena.HeroArenas;
            }
            else
            {
                heroCheck = heroModel.isHero ? BosController.instance.Boss : BosController.instance.HeroBoss; 
            }
            if (heroModel.HeroSpin.State == Hero.AnimationState.Attack)
            {
                for (int i = 0; i < heroCheck.Count; i++)
                {
                    if (heroCheck[i] == null || heroCheck[i].CurHP <= 0) continue;
                    TargetSkills.Add(heroCheck[i]);
                    return TargetSkills;
                }
            }
            else if (heroModel.HeroSpin.State == Hero.AnimationState.Skill)
            {
                switch (heroDataCf.SkillType)
                {
                    case SkillType.AllEnemy:
                        for (int i = 0; i < heroCheck.Count; i++)
                        {
                            if (heroCheck[i] != null && heroCheck[i].CurHP > 0)
                            {
                                TargetSkills.Add(heroCheck[i]);
                            }
                        }
                        break;
                    case SkillType.TwoEnemystandinginthefirstrow:
                        for (int i = 0; i < heroCheck.Count; i++)
                        {
                            if (i < 2 && heroCheck[i].CurHP > 0)
                            {
                                TargetSkills.Add(heroCheck[i]);
                            }
                            if (i >= 2 && heroCheck[0].CurHP <= 0 && heroCheck[1].CurHP <= 0 && heroCheck[i].CurHP > 0)
                            {
                                TargetSkills.Add(heroCheck[i]);
                            }
                        }
                        break;
                    case SkillType.ThreeEnemystandinginthesencondrow:
                        bool check = false;
                        for (int i = 2; i < heroCheck.Count; i++)
                        {
                            if (heroCheck[i].CurHP > 0)
                            {
                                TargetSkills.Add(heroCheck[i]);
                                check = true;
                            }
                        }
                        if (!check)
                        {
                            for (int i = 0; i < 2 && i < heroCheck.Count; i++)
                            {
                                if (heroCheck[i].CurHP > 0)
                                {
                                    TargetSkills.Add(heroCheck[i]);
                                }
                            }
                        }
                        break;
                }
            }
            //return TargetSkills;
            return TargetSkills.Count > 0 ? TargetSkills : null;
        }

        public Vector3 TargetPosition(HeroModel heroModel)
        {
            Vector3 Pos;
            List<HeroModel> targets = GetTarget(heroModel);
            //if (targets.Count <= 0 || targets == null) return Pos = null;
            if (targets == null || targets.Count == 0)
            {  
                return Vector3.zero; // hoặc có thể trả về một giá trị mặc định khác
            }
            if (heroModel.HeroSpin.State == Hero.AnimationState.Attack)
            {
                if (heroModel.isHero)
                {
                    Pos = new Vector3(targets[0].transform.position.x - Contans.DistanceATK, targets[0].transform.position.y, targets[0].transform.position.z);
                }
                else
                {
                    Pos = new Vector3(targets[0].transform.position.x + Contans.DistanceATK, targets[0].transform.position.y, targets[0].transform.position.z);
                }
                return Pos;
            }
            else
            {
                if (heroModel.typeHero == -1)
                {
                    Pos = heroModel.isHero ? battleCampain.skillHeroPos.position : battleCampain.skillEnemyPos.position;
                }
                else
                {
                    Pos = heroModel.isHero ? battleArena.skillHeroPos.position : battleArena.skillEnemyPos.position;
                }
                return Pos;
            }
        }
        //private bool CheckWinLose(HeroModel heroModel)
        //{
        //    List<HeroModel> HeroCheck = heroModel.isHero ? Heros : Enemies;
        //    for (int i = 0; i < HeroCheck.Count; i++)
        //    {
        //        if (HeroCheck[i].CurHP > 0) return false;
        //    }
        //    return true;
        //}

        //public void CheckWinLoseCombat(HeroModel heroModel)
        //{
        //    List<HeroModel> HeroCheck = heroModel.isHero ? Heros : Enemies;
        //    for (int i = 0; i < HeroCheck.Count; i++)
        //    {
        //        if (HeroCheck[i].CurHP > 0) return ;
        //    }
        //    if (!heroModel.isHero) 
        //    {
        //        if (campainType == CampainType.Campain)
        //        {
        //            battleCampain.NextWave();
        //        }else
        //        {
        //            UIManager.instance.battleUI.Win();
        //        }
        //    }else
        //    {
        //        if (campainType == CampainType.Campain)
        //        {
        //            battleCampain.ResetWave();
        //        }
        //        else
        //        {
        //            UIManager.instance.battleUI.Lose();
        //        }
        //    }
        //}
        //public void CheckWinLoseCombat(HeroModel heroModel)
        //{
        //    List<HeroModel> HeroCheck = new List<HeroModel>();
        //    if (heroModel.typeHero == -1)
        //    {
        //        HeroCheck = heroModel.isHero ? battleCampain.EnemyCampains : battleCampain.HeroCampains;
        //    }else
        //    {
        //        HeroCheck = heroModel.isHero ? battleArena.EnemyArenas : battleArena.HeroArenas;
        //    }
        //    //HeroCheck = heroModel.isHero ? Enemies : Heros;
        //    for (int i = 0; i < HeroCheck.Count; i++)
        //    {
        //        if (HeroCheck[i].CurHP > 0) return;
        //    }

        //    if (heroModel.isHero)
        //    {
        //        isWin = true;
        //        if (campainType == CampainType.Campain)
        //        {
        //            battleCampain.NextWave();
        //        }
        //        else
        //        {
        //            UIManager.instance.arenaUI.Win();
        //            AchivementDataCtrl.Instance.DoneAchiveMent(AchivementType.WinBattles);
        //            Debug.Log("a");
        //        }
        //    }
        //    else
        //    {
        //        isWin = false;
        //        if (campainType == CampainType.Campain)
        //        {
        //            battleCampain.ResetWave();
        //        }
        //        else
        //        {
        //            UIManager.instance.arenaUI.Lose();
        //        }
        //    }
        //    //StopComBat(); 
        //    isEndGame = true;
        //}
        public int SetNumberEnemyDie()
        {
            int number = 0;
            for (int i = 0; i < this.Enemies.Count; i++)
            {
                if (this.Enemies[i].CurHP <= 0)
                {
                    number++;
                }
            }
            return number;
        }

        public bool checkHeroOnTeam()
        {
            for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
            {
                if (UserManager.Instance.UserData.HeroTeams[i] >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        public void SpawnPoint(HeroModel heroModel, Vector3 pointPos, int dmg, Transform holdPoint)
        {
            GameObject point = Instantiate(Point, pointPos, Quaternion.identity);
            point.transform.SetParent(holdPoint, true);
            point.transform.localScale = new Vector3(2f, 2f, 2f);
            Vector3 TargetPos = new Vector3(pointPos.x, pointPos.y + 0.5f, pointPos.z);
            point.GetComponent<PointMove>().InitPoint(heroModel, dmg);
            point.GetComponent<PointMove>().MovePoint(TargetPos);

        }
    }
}

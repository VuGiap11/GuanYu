using GuanYu.Hero;
using GuanYu.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuanYu
{
    public class BattleArena : MonoBehaviour
    {
        public List<HeroModel> HeroArenas = new List<HeroModel>();
        public List<HeroModel> EnemyArenas = new List<HeroModel>();
        public List<HeroModel> TurnHeroArenas = new List<HeroModel>();
        public List<HeroModel> ListTurnHeroSkip = new List<HeroModel>();
        [SerializeField] private List<Transform> HeroPosArena;
        [SerializeField] private List<Transform> EnemyPosArena;
        [SerializeField] private HeroModel EnemyPre;
        [SerializeField] private HeroModel HeroPre;
        public Transform skillHeroPos;
        public Transform skillEnemyPos;
        public float moveTime = 0.2f;
        public int indexTurn = 0;

        public bool isEndGame = false;
        private int indexTurnSkip = 0;

        public void SpawnBattleArena()
        {
            this.indexTurn = 0;
            isEndGame = false;
            SpawnHero();
            SpawnEnemy();
            UIManager.instance.arenaUI.SetAvatarArena();
            this.TurnHeroArenas.Clear();
            Getturn();
            UIManager.instance.arenaUI.SetTurnAvar();
        }
        public void Combatt()
        {
            if (HeroAttack != null)
            {
                StopCoroutine(HeroAttack);
            }
            HeroAttack = StartCoroutine(Attack());
            //Debug.Log("combat");
        }

        Coroutine HeroAttack;
        IEnumerator Attack()
        {
            indexTurn = 0;
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < this.TurnHeroArenas.Count; i++)
            {
                if (this.TurnHeroArenas[i] == null || this.TurnHeroArenas[i].CurHP <= 0) continue;
                this.TurnHeroArenas[i].Attack();
                yield return new WaitUntil(() => !this.TurnHeroArenas[i].Setdone());
                yield return new WaitForSeconds(1f / BattleController.instance.SpeedGame);
                //yield return new WaitUntil(() => !IsAttackingFinished());
                CheckWinLoseCombat(TurnHeroArenas[i]);
                //yield return new WaitForSeconds(0.5f / BattleController.instance.SpeedGame);
                if (isEndGame) break;
                //yield return new WaitForSeconds(0.3f / BattleController.instance.SpeedGame);
                this.indexTurn++;
                //Debug.Log("indexTurn" + indexTurn);
                if (indexTurn >= this.TurnHeroArenas.Count - 30)
                {
                    Getturn();
                }
            }
        }
        public void CheckWinLoseCombat(HeroModel heroModel)
        {
            List<HeroModel> HeroCheck = heroModel.isHero ? EnemyArenas : HeroArenas;
            for (int i = 0; i < HeroCheck.Count; i++)
            {
                if (HeroCheck[i].CurHP > 0) return;
            }
            isEndGame = true;
            if (heroModel.isHero)
            {
                UIManager.instance.arenaUI.Win();
                AchivementDataCtrl.Instance.DoneAchiveMent(AchivementType.WinBattles);
                AchivementDataCtrl.Instance.DoneAchiveMent(AchivementType.ReachTrophiesMilestone);
            }
            else
            {
                UIManager.instance.arenaUI.Lose();
            }
            RankData rankData = new RankData();
            rankData = HeroAssets.Instance.RankDataPlayer();
            rankData.point = UserManager.Instance.DataPlayerController.point;
            HeroAssets.Instance.SortRank();
        }

        public void RemoveTurn(HeroModel heroModel)
        {
            for (int i = TurnHeroArenas.Count - 1; i >= indexTurn; i--)
            {
                if (TurnHeroArenas[i] == heroModel)
                {
                    TurnHeroArenas.RemoveAt(i);
                }
            }
        }
        public bool IsAttackingFinished()
        {
            //for (int i = 0; i < this.HeroArenas.Count; i++)
            //{
            //    HeroArenas[i].isDone = HeroArenas[i].Setdone();
            //    if (HeroArenas[i].isDone == true) return true;
            //}
            //for (int i = 0; i < this.EnemyArenas.Count; i++)
            //{
            //    EnemyArenas[i].isDone = EnemyArenas[i].Setdone();
            //    if (EnemyArenas[i].isDone == true) return true;
            //}
            foreach (var hero in HeroArenas)
            {
                hero.isDone = hero.Setdone();
                if (hero.isDone) return true;
            }

            foreach (var enemy in EnemyArenas)
            {
                enemy.isDone = enemy.Setdone();
                if (enemy.isDone) return true;
            }

            return false;
        }
        public void Getturn()
        {
            int indexturn = 0;
            while (indexturn < 8)
            {
                indexturn++;
                for (int i = 0; i < this.HeroArenas.Count; i++)
                {
                    if (this.HeroArenas[i].CurHP <= 0) continue;
                    this.TurnHeroArenas.Add(HeroArenas[i]);
                }
                for (int i = 0; i < this.EnemyArenas.Count; i++)
                {
                    if (EnemyArenas[i].CurHP <= 0) continue;
                    this.TurnHeroArenas.Add(EnemyArenas[i]);
                }
            }
        }
        public void SpawnHero()
        {
            for (int i = 0; i < this.HeroPosArena.Count; i++)
            {
                MyFunction.ClearChild(HeroPosArena[i]);
            }
            this.HeroArenas.Clear();
            if (UserManager.Instance.UserData.HeroTeams.Count > HeroPosArena.Count) return;
            for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
            {

                if (UserManager.Instance.UserData.HeroTeams[i] < 0) continue;
                HeroModel hero = Instantiate(HeroPre, HeroPosArena[i]);
                hero.isHero = true;
                hero.typeHero = 0;
                this.HeroArenas.Add(hero);
                hero.transform.SetParent(HeroPosArena[i], false);
                hero.InitHeroBattle(UserManager.Instance.UserData.HeroTeams[i], HeroPosArena[i].transform);
                //hero.heroModelType = HeroModelType.HeroArena;
            }
            UIManager.instance.arenaUI.GetHpHeroArena(GetHpHeroArena(), GetCurHpHeroArena());
        }
        public void SpawnEnemy()
        {
            this.EnemyArenas.Clear();
            for (int j = 0; j < EnemyPosArena.Count; j++)
            {
                MyFunction.ClearChild(EnemyPosArena[j]);
            }
            List<int> randomNumbers = GetRandomNumbers();
            for (int i = 0; i < randomNumbers.Count; i++)
            {
                HeroModel hero = Instantiate(EnemyPre, this.EnemyPosArena[i]);
                hero.isHero = false;
                hero.typeHero = 0;
                this.EnemyArenas.Add(hero);
                hero.transform.SetParent(this.EnemyPosArena[i].transform, false);
                hero.InitEnemyBattle(randomNumbers[i], this.EnemyPosArena[i].transform);
                //hero.heroModelType = HeroModelType.HeroArena;
            }
            UIManager.instance.arenaUI.GetHpEnemyArena(GetHpEnemyArena(), GetCurHpEnemyArena());
            //BattleController.instance.UpdateHp();
        }

        private List<int> GetRandomNumbers()
        {
            List<int> numbers = new List<int>();
            //for (int i = 0; i < HeroAssets.Instance.EnemyDataCfs.HeroDatas.Count; i++)
            //{
            //    numbers.Add(HeroAssets.Instance.EnemyDataCfs.HeroDatas[i].Index);
            //}
            for (int i = 0; i < HeroAssets.Instance.EnemyDatas.Count; i++)
            {
                numbers.Add(HeroAssets.Instance.EnemyDatas[i].Index);
            }
            List<int> results = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                int randomIndex = Random.Range(0, numbers.Count);
                results.Add(numbers[randomIndex]);
                numbers.RemoveAt(randomIndex);
            }
            return results;
        }

        public void ResetHero()
        {
            for (int i = 0; i < this.HeroArenas.Count; i++)
            {
                this.HeroArenas[i].ResetHero();
            }
            for (int i = 0; i < this.EnemyArenas.Count; i++)
            {
                this.EnemyArenas[i].ResetHero();
            }
        }

        public void GetTurnSkip()
        {
            int indexturn = 0;
            while (indexturn < 8)
            {
                indexturn++;
                for (int i = 0; i < this.HeroArenas.Count; i++)
                {
                    if (HeroArenas[i].CurHP <= 0) continue;
                    this.ListTurnHeroSkip.Add(HeroArenas[i]);
                }
                for (int i = 0; i < this.EnemyArenas.Count; i++)
                {
                    if (EnemyArenas[i].CurHP <= 0) continue;
                    this.ListTurnHeroSkip.Add(EnemyArenas[i]);
                }
            }
        }

        public void SkipGame()
        {
            SoundManager.instance.PressButtonAudio();
            StopAllCoroutines();
            this.ListTurnHeroSkip.Clear();
            ResetHero();
            GetTurnSkip();
            isEndGame = false;
            int turnNumber = 0;
            this.indexTurnSkip = 0;
            for (int i = 0; i < this.ListTurnHeroSkip.Count; i++)
            {
                turnNumber++;
                this.indexTurnSkip++;
                this.ListTurnHeroSkip[i].AttackOneHit();
                this.ListTurnHeroSkip[i].DoAttackOneHit();
                CheckWinLoseCombat(this.ListTurnHeroSkip[i]);
                UIManager.instance.arenaUI.CheckAvatarOnOff();
                if (isEndGame) break;
                if (this.indexTurnSkip >= ListTurnHeroSkip.Count - 15)
                {
                    GetTurnSkip();
                }
            }
        }

        public int GetHpEnemyArena()
        {
            int hp = 0;
            for (int i = 0; i < this.EnemyArenas.Count; i++)
            {
                HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this.EnemyArenas[i]);
                hp += heroDataCf.Hp;
            }
            return hp;
        }
        public int GetCurHpEnemyArena()
        {
            int hp = 0;
            for (int i = 0; i < this.EnemyArenas.Count; i++)
            {
                hp += this.EnemyArenas[i].CurHP;
            }
            return hp;
        }
        public int GetHpHeroArena()
        {
            int hp = 0;
            for (int i = 0; i < this.HeroArenas.Count; i++)
            {
                HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this.HeroArenas[i]);
                hp += heroDataCf.Hp;
            }
            return hp;
        }
        public int GetCurHpHeroArena()
        {
            int hp = 0;
            for (int i = 0; i < this.HeroArenas.Count; i++)
            {
                hp += this.HeroArenas[i].CurHP;
            }
            return hp;
        }
    }
}
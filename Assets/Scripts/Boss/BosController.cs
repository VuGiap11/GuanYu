
using GuanYu.Hero;
using GuanYu.UI;
using GuanYu.User;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class BosController : MonoBehaviour
    {
        public static BosController instance;
        [SerializeField] private GameObject TurnBossObj;
        [SerializeField] private Transform starPos;
        [SerializeField] private Transform endPos;
        private float timeMove = 0.5f;
        public List<HeroModel> HeroBoss = new List<HeroModel>();
        [SerializeField] private List<Transform> HeroPosBoss;
        [SerializeField] private HeroModel HeroPre;
        [SerializeField] private HeroModel BossPre;
        [SerializeField] private GameObject DetailObj;
        [SerializeField] private GameObject BattleBoss;
        public List<HeroModel> Boss = new List<HeroModel>();
        public List<HeroModel> TurnHeroBoss = new List<HeroModel>();
        [SerializeField] private Transform BossPoss;
        public Transform heroPosSkill;
        public Transform bossPosSkill;
        [SerializeField] private TextMeshProUGUI roundText;
        [SerializeField] private TextMeshProUGUI HpText;
        [SerializeField] private HpBar HpBarBoss;
        [SerializeField] private HpBar SkillBarBoss;
        [SerializeField] private GameObject WinObj;
        [SerializeField] private GameObject LoseObj;
        [SerializeField] private GameObject NoticeObj;
        public int bossFightCount = 0;
        private int round = 0;
        private bool isEndGame = false;
        [SerializeField] private Button buttonAtkBoss;
        [SerializeField] private string timeAtkBoss;
        [SerializeField] private TextMeshProUGUI TimeText;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void OnEnable()
        {
            DetailObj.SetActive(true);
            BattleBoss.SetActive(false);
            LoadDataBoss();
            string a = PlayerPrefs.GetString(Contans.NextTimeResetBoss);
            Debug.Log("a" + a);
        }

        public void GoCombatPanel()
        {
            DetailObj.SetActive(false);
            BattleBoss.SetActive(true);
            bossFightCount++;
            UserManager.Instance.UserData.levelBoss =0;
            UserManager.Instance.SaveData();
            SaveData();
            CombatBoss();

        }
        public void ExitPanel()
        {
            NoticeObj.SetActive(true);
        }
        public void ExitCombat()
        {
            DetailObj.SetActive(true);
            BattleBoss.SetActive(false);
            NoticeObj.SetActive(false);
            WinObj.SetActive(false);
            LoseObj.SetActive(false);
            CheckSlotAtkBoss();
        }
        //[ContextMenu("MoveObj")]
        //public void MoveObj(Action callback = null)
        //{
        //    TurnBossObj.transform.DOMove(endPos.position, timeMove)
        //     .SetEase(Ease.Linear)
        //      .OnStart(() => { })
        //    .SetDelay(0f)
        //    .OnComplete(() =>
        //    {
        //        DOVirtual.DelayedCall(4f, delegate
        //        {
        //            if (callback != null) { callback.Invoke(); }
        //            BackObj();
        //        });
        //    });
        //}
        //void BackObj()
        //{
        //    //TurnBossObj.SetActive(false);
        //    //TurnBossObj.transform.DOMove(starPos.position, timeMove)
        //    // .SetEase(Ease.Linear)
        //    //  .OnStart(() => { })
        //    //.SetDelay(0f)
        //    //.OnComplete(() =>
        //    //{
        //    //    TurnBossObj.SetActive(true);
        //    //});
        //    TurnBossObj.transform.position = starPos.position;
        //    TurnBossObj.SetActive(true);
        //}
        [ContextMenu("CombatBoss")]
        public void CombatBoss()
        {
            round = 0;
            SpawnHero();
            SpawnEnemy();
            ComBat();
        }
        [ContextMenu("SpawnHero")]
        public void SpawnHero()
        {
            for (int i = 0; i < this.HeroPosBoss.Count; i++)
            {
                MyFunction.ClearChild(HeroPosBoss[i]);
            }
            this.HeroBoss.Clear();
            if (UserManager.Instance.UserData.HeroTeams.Count > HeroPosBoss.Count) return;
            for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
            {

                if (UserManager.Instance.UserData.HeroTeams[i] < 0) continue;
                HeroModel hero = Instantiate(HeroPre, HeroPosBoss[i]);
                hero.isHero = true;
                hero.typeHero = 1;
                this.HeroBoss.Add(hero);
                hero.transform.SetParent(HeroPosBoss[i], false);
                hero.InitHeroBattle(UserManager.Instance.UserData.HeroTeams[i], HeroPosBoss[i]);
                //hero.heroModelType = HeroModelType.HeroArena;
            }
            //UIManager.instance.arenaUI.GetHpHeroArena(GetHpHeroArena(), GetCurHpHeroArena());
        }
        [ContextMenu("SpawnEnemy")]
        public void SpawnEnemy()
        {
            isEndGame = false;
            this.Boss.Clear();
            MyFunction.ClearChild(this.BossPoss);
            HeroModel boss = Instantiate(BossPre, this.BossPoss);
            boss.isHero = false;
            boss.typeHero = 1;
            this.Boss.Add(boss);
            boss.transform.SetParent(this.BossPoss, false);
            //Boss.transform.position = Vector3.zero;
            boss.InitBoss();
            boss.InitEnemyBattle(HeroAssets.Instance.BossDatas[0].Index, this.BossPoss);
            HeroDataCf heroDataCf = UserManager.Instance.SetEnemyDataCf(boss);
            UpdateHpBoss(heroDataCf.Hp, boss.CurHP);
            UpdateManaBoss(heroDataCf.IndexSkill, boss.CurindexSkill);
        }

        public void UpdateHpBoss(int Hp, int curHp)
        {
            this.HpBarBoss.UpdateHpBar(Hp, curHp);
            this.HpText.text = curHp.ToString() + "/" + Hp.ToString();
        }
        public void UpdateManaBoss(int Mana, int curMana)
        {
            this.SkillBarBoss.UpdateHpBar(Mana, curMana);
        }
        public void GetTurnHeroBos()
        {
            this.TurnHeroBoss.Clear();
            for (int i = 0; i < this.HeroBoss.Count; i++)
            {
                if (this.HeroBoss[i].CurHP <= 0) continue;
                this.TurnHeroBoss.Add(HeroBoss[i]);
            }
            for (int i = 0; i < this.Boss.Count; i++)
            {
                if (Boss[i].CurHP <= 0) continue;
                this.TurnHeroBoss.Add(Boss[i]);
            }
        }
        [ContextMenu("Combat")]
        public void ComBat()
        {
            StopAllCoroutines();
            GetTurnHeroBos();
            if (AttackBoss != null)
            {
                StopCoroutine(AttackBoss);
            }
            AttackBoss = StartCoroutine(Attack());
        }
        Coroutine AttackBoss;
        IEnumerator Attack()
        {
            yield return new WaitForSeconds(0.5f);
            round++;
            if (round >= 21)
            {
                Win();
                yield break;
            }
            InitDetail(round);
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < this.TurnHeroBoss.Count; i++)
            {
                if (this.TurnHeroBoss[i] == null || this.TurnHeroBoss[i].CurHP <= 0) continue;
                this.TurnHeroBoss[i].Attack();
                yield return new WaitUntil(() => !IsAttackingFinished());
                CheckWinLoseCombat(TurnHeroBoss[i]);
                yield return new WaitForSeconds(0.5f);
                if (isEndGame) yield break;
            }
            ComBat();
        }

        private void Win()
        {
            UserManager.Instance.DataPlayerController.gold += 1000;
            UserManager.Instance.SaveData();
            HomeController.instance.Init();
            WinObj.SetActive(true);
            LoseObj.SetActive(false);
        }
        private void Lose()
        {
            WinObj.SetActive(false);
            LoseObj.SetActive(true);
        }
        void CheckWinLoseCombat(HeroModel heroModel)
        {
            List<HeroModel> HeroCheck = heroModel.isHero ? Boss : HeroBoss;
            for (int i = 0; i < HeroCheck.Count; i++)
            {
                if (HeroCheck[i].CurHP > 0) return;
            }
          
            if (heroModel.isHero)
            {
                ResetState();
            }
            else
            {
                isEndGame = true;
                Lose();
            }
        }

        public void ResetState()
        {
            UserManager.Instance.UserData.levelBoss +=5;
            UserManager.Instance.SaveData();
            SpawnEnemy();
            ComBat();
        }
        public void InitDetail(int round)
        {
            roundText.text = "Round " + round.ToString();
        }
        public bool IsAttackingFinished()
        {
            for (int i = 0; i < this.HeroBoss.Count; i++)
            {
                HeroBoss[i].isDone = HeroBoss[i].Setdone();
                if (HeroBoss[i].isDone == true) return true;
            }
            for (int i = 0; i < this.Boss.Count; i++)
            {
                Boss[i].isDone = Boss[i].Setdone();
                if (Boss[i].isDone == true) return true;
            }
            return false;
        }

        public void NextState()
        {

        }
        // Hàm tải dữ liệu từ PlayerPrefs khi khởi động
        void LoadData()
        {
            //bossFightCount = PlayerPrefs.GetInt("BossFightCount", 0);
            string savedResetTime = PlayerPrefs.GetString(Contans.NextTimeResetBoss, "");

            // Nếu có thời gian reset đã lưu, đọc nó, nếu không thì đặt mặc định vào 0h00 ngày hôm sau
            if (DateTime.TryParse(savedResetTime, out DateTime loadedResetTime))
            {
                nextResetTime = loadedResetTime;
            }
            else
            {
                nextResetTime = DateTime.Today.AddDays(1);
            }
        }

        public DateTime nextResetTime;
        public void LoadDataBoss()
        {
            if (PlayerPrefs.HasKey(Contans.NextTimeResetBoss))
            {
                if (!DateTime.TryParse(PlayerPrefs.GetString(Contans.NextTimeResetBoss), out nextResetTime))
                {
                    // Nếu parse thất bại, đặt giá trị mặc định
                    nextResetTime = DateTime.Today.AddDays(1);
                    Debug.LogWarning("Failed to parse nextResetTime, using default value.");
                }
                DateTime currentTime = DateTime.Now;
                if (currentTime >= nextResetTime)
                {
                    ResetBossFightCount();
                }
                else
                {
                    bossFightCount = PlayerPrefs.GetInt("BossFightCount");
                    Debug.Log("bossFightCount" + bossFightCount);
                }
            }
            else
            {
                nextResetTime = DateTime.Today.AddDays(1);
                bossFightCount = PlayerPrefs.GetInt("BossFightCount", 0);
                SaveData();
                string b = PlayerPrefs.GetString(Contans.NextTimeResetBoss);
                Debug.Log("b" + b);
            }
            CheckSlotAtkBoss();

        }
        void ResetBossFightCount()
        {
            bossFightCount = 0;
            nextResetTime = DateTime.Today.AddDays(1).Date;
            SaveData();
        }
        void SaveData()
        {
            PlayerPrefs.SetInt("BossFightCount", bossFightCount);
            PlayerPrefs.SetString(Contans.NextTimeResetBoss, nextResetTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        private bool DoneAtkBoss;
        public void CheckSlotAtkBoss()
        {
            if (this.bossFightCount >= Contans.bossTurnCount)
            {
                buttonAtkBoss.interactable = false;
                nextResetTime = DateTime.Today.AddDays(1);

                DoneAtkBoss = true;
                this.bossFightCount = 0;
                bossFightCount = PlayerPrefs.GetInt("BossFightCount", 0);
                SaveData();

            }
            else
            {
                buttonAtkBoss.interactable = true;
                DoneAtkBoss = false;
                TimeText.text = "Atk "+(Contans.bossTurnCount-bossFightCount).ToString();
            }
        }
        void Update()
        {
            if(DoneAtkBoss)
            {
                TimeText.text = GetTimeUntilMidnight();
            }
            //string timeLeft = GetTimeUntilMidnight();
            //Debug.Log(timeLeft);
        }
        string GetTimeUntilMidnight()
        {
            DateTime now = DateTime.Now;
            DateTime midnightTomorrow = now.Date.AddDays(1);
            TimeSpan timeRemaining = midnightTomorrow - now;
            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                                 timeRemaining.Hours,
                                                 timeRemaining.Minutes,
                                                 timeRemaining.Seconds);

            return formattedTime;
        }

        // tính lại số onofff btn

    }
}

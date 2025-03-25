using DG.Tweening;
using GuanYu.Battle;
using GuanYu.Hero;
using GuanYu.User;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GuanYu
{
    public class BattleCampain : MonoBehaviour
    {
        public List<HeroModel> HeroCampains = new List<HeroModel>();
        public List<HeroModel> EnemyCampains = new List<HeroModel>();
        public List<HeroModel> TurnHeroCampains = new List<HeroModel> { };
        [SerializeField] private List<Transform> HeroPosCampain;
        [SerializeField] private List<Transform> EnemyPosCampain;
        [SerializeField] private HeroModel EnemyPre;
        [SerializeField] private HeroModel HeroModelPre;
        public Transform skillHeroPos;
        public Transform skillEnemyPos;

        public RectTransform GoldPosTarget;// VIJ TRIS TRONG CANVAS THI DUNG RECTRANFORM
        public GoldMove Gold;
        public List<GoldMove> Golds;
        public Transform GoldParent;
        [SerializeField] private GameObject fxObj;
        [SerializeField] Transform FxHolder;
        [SerializeField] Transform FxPos;

        public int wave = 0;
        public int indexTurn = 0;

        private bool isStartGame = false;
        private bool isWin = false;

        public Arrow arrowPrefab;
        [SerializeField] Transform holderArrow;
        public void SpwanEnemy()
        {
            EnemyCampains.Clear();
            for (int j = 0; j < EnemyPosCampain.Count; j++)
            {
                MyFunction.ClearChild(EnemyPosCampain[j]);
            }
            for (int i = 0; i < BattleCf.Instance.levelData.LevelDatas[UserManager.Instance.DataPlayerController.levelCampain - 1]
                .WaveDatas[wave].Index.Count; i++)
            {
                HeroModel enemy = Instantiate(EnemyPre, EnemyPosCampain[i]);
                enemy.isHero = false;
                enemy.typeHero = -1;
                EnemyCampains.Add(enemy);
                enemy.transform.SetParent(EnemyPosCampain[i].transform, false);
                enemy.InitEnemyBattle(BattleCf.Instance.levelData.LevelDatas[UserManager.Instance.DataPlayerController.levelCampain - 1]
                    .WaveDatas[wave].Index[i], EnemyPosCampain[i]);
                //enemy.heroModelType = HeroModelType.HeroCampain;
            }
            UIManager.instance.campainUI.GetHpEnemy(GetHpEnemyCampain(), GetCurHpEnemyCampain());
        }

        public void GetHpEnemy()
        {
            UIManager.instance.campainUI.GetHpEnemy(GetHpEnemyCampain(), GetCurHpEnemyCampain());
        }
        public void SpawnHero()
        {
            //BattleController.instance.SpwanHero(this.HeroPosCampain);
            for (int i = 0; i < HeroPosCampain.Count; i++)
            {
                MyFunction.ClearChild(HeroPosCampain[i]);
            }
            HeroCampains.Clear();
            if (UserManager.Instance.UserData.HeroTeams.Count > HeroPosCampain.Count) return;
            for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
            {

                if (UserManager.Instance.UserData.HeroTeams[i] < 0) continue;
                HeroModel hero = Instantiate(HeroModelPre, HeroPosCampain[i]);
                hero.isHero = true;
                hero.typeHero = -1;
                this.HeroCampains.Add(hero);
                hero.transform.SetParent(HeroPosCampain[i], false);
                hero.InitHeroBattle(UserManager.Instance.UserData.HeroTeams[i], HeroPosCampain[i].transform);
                //hero.heroModelType = HeroModelType.HeroCampain;
            }
        }

        public void Campain()
        {
            //if (!BattleController.instance.checkHeroOnTeam())
            //{
            //    UIManager.instance.Warning();
            //    return;
            //}
            if (!isStartGame || !GameManager.instance.CheckSameList(GameManager.instance.heroPanel.ListHeroStartGame, UserManager.Instance.UserData.HeroTeams))
            {
                isStartGame = true;
                wave = 0;
                UIManager.instance.campainUI.InitLevel(UserManager.Instance.DataPlayerController.levelCampain, wave);
                ResetGame();
                SpawnHeroBattleCampain();
                GameManager.instance.heroPanel.ListHeroStartGame = new List<int>(UserManager.Instance.UserData.HeroTeams);
                Debug.Log("hay chon tươung");
            }

        }
        private void ResetGame()
        {
            MyFunction.ClearChild(GoldParent);
            Golds.Clear();
            MyFunction.ClearChild(UIManager.instance.campainUI.HolderCampain);
        }
        public void SpawnHeroBattleCampain()
        {
            if (!BattleController.instance.checkHeroOnTeam())
            {
                NoHeroOnTeam();
                UIManager.instance.Warning();
                return;
            }
            StopAllCoroutines();
            SpawnHero();
            SpwanEnemy();
            TurnHeroCampains.Clear();
            GetturnCampain();
            DOVirtual.DelayedCall(0.5f, delegate
            {
                Combatt();
            });
        }

        private void NoHeroOnTeam()
        {
            TurnHeroCampains.Clear();
            for (int i = 0; i < HeroPosCampain.Count; i++)
            {
                MyFunction.ClearChild(HeroPosCampain[i]);
            }
            HeroCampains.Clear();
            EnemyCampains.Clear();
            for (int j = 0; j < EnemyPosCampain.Count; j++)
            {
                MyFunction.ClearChild(EnemyPosCampain[j]);
            }
            StopCoroutine(HeroAttack);
        }
        public void Combatt()
        {
            //StopAllCoroutines();
            if (HeroAttack != null)
            {
                StopCoroutine(HeroAttack);
            }
            HeroAttack = StartCoroutine(Attack());
            //Debug.Log("combat");
        }
        Coroutine HeroAttack;

        //public bool IsNextTurn;
        IEnumerator Attack()
        {
            //isEndGame = false;
            int indexTurn = 0;
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < this.TurnHeroCampains.Count; i++)
            {
                //yield return new WaitUntil(() => IsNextTurn); this.IsNextTurn = false;
                if (this.TurnHeroCampains[i] == null || this.TurnHeroCampains[i].CurHP <= 0) continue;
                this.TurnHeroCampains[i].Attack();
                //yield return new WaitUntil(() => !IsAttackingFinished());
                yield return new WaitUntil(() => !this.TurnHeroCampains[i].Setdone());
                yield return new WaitForSeconds(1f / BattleController.instance.SpeedGame);
                CheckWinLoseCombat(TurnHeroCampains[i]);
                indexTurn++;
                if (indexTurn >= this.TurnHeroCampains.Count - 18)
                {
                    GetturnCampain();
                }
            }
        }
        public bool IsAttackingFinished()
        {
            //return isAttacking;
            for (int i = 0; i < this.HeroCampains.Count; i++)
            {
                HeroCampains[i].isDone = HeroCampains[i].Setdone();
                if (HeroCampains[i].isDone == true) return true;
            }
            for (int i = 0; i < this.EnemyCampains.Count; i++)
            {
                EnemyCampains[i].isDone = EnemyCampains[i].Setdone();
                if (EnemyCampains[i].isDone == true) return true;
            }
            return false;
        }

        public void RemoveTurn(HeroModel heroModel)
        {
            for (int i = this.TurnHeroCampains.Count - 1; i >= indexTurn; i--)
            {
                if (this.TurnHeroCampains[i] == heroModel)
                {
                    this.TurnHeroCampains.RemoveAt(i);
                }
            }
        }
        public void CheckWinLoseCombat(HeroModel heroModel)
        {
            List<HeroModel> HeroCheck = heroModel.isHero ? EnemyCampains : HeroCampains;
            for (int i = 0; i < HeroCheck.Count; i++)
            {
                if (HeroCheck[i].CurHP > 0) return;
            }

            if (heroModel.isHero)
            {
                isWin = true;
                AchivementDataCtrl.Instance.DoneAchiveMent(AchivementType.EndlesWavesDefeated);
                NextWave();
            }
            else
            {
                isWin = false;
                ResetWave();
            }
            MoveGold();
            //StopComBat(); 
        }
        public void GetturnCampain()
        {
            int indexturn = 0;
            while (indexturn < 8)
            {
                indexturn++;
                for (int i = 0; i < this.HeroCampains.Count; i++)
                {
                    if (HeroCampains[i].CurHP <= 0) continue;
                    this.TurnHeroCampains.Add(HeroCampains[i]);
                }
                for (int i = 0; i < this.EnemyCampains.Count; i++)
                {
                    if (EnemyCampains[i].CurHP <= 0) continue;
                    this.TurnHeroCampains.Add(EnemyCampains[i]);
                }
            }
        }
        public void NextWave()
        {
            StopAllCoroutines();
            wave++;
            if (wave > 6)
            {
                UserManager.Instance.DataPlayerController.levelCampain++;
                wave = 0;
            }

            UserManager.Instance.SaveData();

            UIManager.instance.campainUI.InitLevel(UserManager.Instance.DataPlayerController.levelCampain, wave);
            SpwanEnemy();
            this.TurnHeroCampains.Clear();
            GetturnCampain();
            DOVirtual.DelayedCall(1.2f, delegate
            {
                Combatt();
            });

        }
        public void ResetWave()
        {
            //Debug.Log("resetwave");
            //BattleController.instance.StopComBat();
            wave = 0;

            UIManager.instance.campainUI.InitLevel(UserManager.Instance.DataPlayerController.levelCampain, BattleController.instance.wave);
            SpawnHeroBattleCampain();
            UserManager.Instance.SaveData();

        }
        public void SpawnGold(Vector3 pos)
        {
            int randomInt = Random.Range(4, 10);
            for (int i = 0; i < randomInt; i++)
            {
                float scale = Random.Range(0.3f, 1f);
                float posnewx = Random.Range(-0.8f, 0.8f);
                float posnewy = Random.Range(-0.5f, 0.5f);
                GoldMove gold = Instantiate(Gold);
                Golds.Add(gold);
                Vector3 position = new Vector3(pos.x + posnewx, pos.y + posnewy, pos.z);
                gold.transform.position = position;
                gold.targetGoldPos = this.GoldPosTarget;
                gold.transform.SetParent(GoldParent, true);
                gold.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
        public void MoveGold()
        {
            //StartCoroutine(GoldMove());
            if(goldMove != null) StopCoroutine(goldMove);
            goldMove = StartCoroutine(GoldMove());
        }
        Coroutine goldMove;
        public IEnumerator GoldMove()
        {
            yield return new WaitForSeconds(0.05f);
            for (int i = 0; i < Golds.Count; i++)
            {
                if (Golds[i] == null) continue;
                //if (i == Golds.Count - 1)
                //{
                //    Golds[i].isDone = true;
                //}
                Golds[i].Move();
            }
            yield return new WaitUntil(() => Golds.All(g => g != null && g.isMoveGold));

            // Khi tất cả đều đã di chuyển xong, gọi hàm C().
            DoneStade();

        }

        public void DoneStade()
        {
            SoundManager.instance.SpawnGoldSound();
            MyFunction.ClearChild(this.GoldParent);
            Golds.Clear();
            if (!isWin) return;
            else
            {
                AddGold();
                //SpawnFX();
            }

        }
        public void AddGold()
        {
            UserManager.Instance.DataPlayerController.gold += BattleCf.Instance.levelData.LevelDatas[UserManager.Instance.DataPlayerController.levelCampain - 1]
            .WaveDatas[BattleController.instance.wave].Gold;
            UIManager.instance.campainUI.InitGold();
            UserManager.Instance.SaveData();
        }
        //public void SpawnFX()
        //{
        //    MyFunction.ClearChild(FxHolder);
        //    GameObject fx = Instantiate(fxObj, FxPos);
        //    fx.transform.position = FxPos.position;
        //    fx.transform.SetParent(FxHolder, true);
        //}

        public int GetHpEnemyCampain()
        {
            int Hp = 0;
            for (int i = 0; i < this.EnemyCampains.Count; i++)
            {
                HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this.EnemyCampains[i]);
                Hp += heroDataCf.Hp;
            }
            return Hp;
        }
        public int GetCurHpEnemyCampain()
        {
            int Hp = 0;
            for (int i = 0; i < this.EnemyCampains.Count; i++)
            {
                Hp += this.EnemyCampains[i].CurHP;
            }
            return Hp;
        }

        private void Update()
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    //SpawnArrowAtClick();
            //    //SpawnArrowAuto();
            //    Debug.Log("arrow");
            //}
        }

        [SerializeField] public Transform startPosArr;
        //public void SpawnArrowAtClick()
        //{
        //    Vector3 target;
        //    HeroModel heroModel = GettargetArrow();
        //    Vector3 mousePosition = Input.mousePosition;
        //    mousePosition.z = Camera.main.nearClipPlane + 1f;
        //    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //    Arrow arrow = Instantiate(arrowPrefab, worldPosition, Quaternion.identity);
        //    arrow.transform.SetParent(holderArrow, true);
        //    arrow.transform.localScale = new Vector3(1f, 1f, 1f);
        //    if (heroModel != null)
        //    {
        //        target = new Vector3(heroModel.transform.position.x , heroModel.transform.position.y + 0.4f, heroModel.transform.position.z);
        //    }
        //    else
        //    {
        //        target = skillEnemyPos.position;
        //    }
        //    arrow.MoveToPosition(worldPosition, target, 300);
        //    Debug.Log("arrowmove");
        //}
        public void SpawnArrowAuto()
        {
            Vector3 target;
            HeroModel heroModel = GettargetArrow();
            Arrow arrow = Instantiate(arrowPrefab, startPosArr.position, Quaternion.identity);
            arrow.transform.SetParent(holderArrow, true);
            arrow.transform.localScale = new Vector3(1f, 1f, 1f);
            if (heroModel != null)
            {
                target = new Vector3(heroModel.transform.position.x, heroModel.transform.position.y + 0.4f, heroModel.transform.position.z);
            }
            else
            {
                target = skillEnemyPos.position;
            }
            arrow.MoveToPosition(startPosArr.position, target, 40,() =>
            {
                if (heroModel != null)
                {
                    heroModel.TakeDamageArr(80);
                }
                else
                {
                    Debug.Log("heroModel không được gán!");
                }
            });
            Debug.Log("arrowmove");
        }
        public HeroModel GettargetArrow()
        {
            List<HeroModel> aliveHeroes = this.EnemyCampains.FindAll(hero => hero.CurHP > 0);
            if (aliveHeroes.Count == 0)
            {
                return null;
            }
            int randomIndex = Random.Range(0, aliveHeroes.Count);
            return aliveHeroes[randomIndex];
        }

        public float totalDuration = 500f; // Tổng thời gian bắn (giây)

        private float timeSinceLastShot = 0f; // Thời gian đã trôi qua từ lần bắn cuối
        private float elapsedTime = 0f; // Tổng thời gian đã chạy
        public float shootInterval = 0.4f;
        public bool isSpawn = false;
        private void FixedUpdate()
        {
            //    if (isSpawn)
            //    {
            //        elapsedTime += Time.deltaTime;

            //        // Ngừng bắn sau khi hết thời gian tổng
            //        if (elapsedTime > totalDuration)
            //        {
            //            isSpawn = false;
            //            return;
            //        }
            //        // Tăng thời gian từ lần bắn cuối
            //        timeSinceLastShot += Time.deltaTime;

            //        // Kiểm tra xem đã đến lúc bắn chưa
            //        if (timeSinceLastShot >= shootInterval)
            //        {
            //            SpawnArrowAuto();
            //            timeSinceLastShot = 0f; // Reset bộ đếm
            //        }
            //    }
        }
        public void SpamArrow()
        {
            isSpawn = true;
            //if (spamArrow != null) ; StopCoroutine("ShootArrows");
            //spamArrow = StartCoroutine("ShootArrows");
        }
        //Coroutine spamArrow;
        //private IEnumerator ShootArrows()
        //{
        //    int duration = 5000; 
        //    float shootInterval = 0.4f; 

        //    int totalShots = Mathf.FloorToInt(duration / shootInterval); 

        //    for (int i = 0; i < totalShots; i++)
        //    {
        //        SpawnArrowAuto();
        //        yield return new WaitForSeconds(shootInterval);
        //    }
        //}
    }
}
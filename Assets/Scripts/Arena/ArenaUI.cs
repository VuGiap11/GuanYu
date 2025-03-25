using DG.Tweening;
using GuanYu.Hero;
using GuanYu.UI;
using GuanYu.User;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class ArenaUI : MonoBehaviour
    {
        [Header("Rank")]
        [SerializeField] private TextMeshProUGUI winNumberText;
        [SerializeField] private TextMeshProUGUI loseNumberText;
        [SerializeField] private TextMeshProUGUI cupNumberText;
        [SerializeField] private Transform rankHolder;
        [SerializeField] private ChangeRank changeRankPrefab;
        [Header("PvP")]
        [SerializeField] private GameObject PvPObj;
        [SerializeField] private GameObject LoadingObj;
        [SerializeField] private GameObject ButtonObj;
        //[SerializeField] private GameObject TopObj;
        [SerializeField] private GameObject buttomObj;
        [SerializeField] private GameObject RankObj;
        [Header("PvPdetail")]
        [SerializeField] private TextMeshProUGUI namePlayerPvPtext;
        [SerializeField] private TextMeshProUGUI nameEnemyPvPtext;
        [SerializeField] private TextMeshProUGUI cupHeroText;
        [SerializeField] private TextMeshProUGUI cupEnemyText;
        public GameObject winObj;
        public GameObject loseObj;

        [SerializeField] private GameObject HoldeAvatarEnemy;
        [SerializeField] private TextMeshProUGUI namePlayerText;

        [Header("PvPBattle")]
        [SerializeField] private HpBar hpEnemy;
        [SerializeField] private HpBar hpHero;
        [SerializeField] private TextMeshProUGUI HpEnemyText;
        [SerializeField] private TextMeshProUGUI HpHeroText;
        [SerializeField] private TextMeshProUGUI doubleText;

        [Header("Avatar")]
        [SerializeField] private List<Transform> HeroAvaListPos;
        [SerializeField] private List<Transform> EnemyAvaListPos;
        public List<Transform> TurnLstPos;
        [SerializeField] private List<Image> avatarHeros;
        [SerializeField] private List<Image> avatarEnemys;
        public Transform holderEffectArena;

        [Header("chest")]
        [SerializeField] private GameObject[] chestPres;
        [SerializeField] private GameObject goldChestPre;
        [SerializeField] private Transform holderChest;
        [Header("newinfor")]
        [SerializeField] private TextMeshProUGUI BossNametext;
        [SerializeField] private TextMeshProUGUI PlayerNameText;
        // hàm này khỏi tạo khi off gameobject;
        private void OnDisable()
        {
            //BattleCtrl.instance.timeScale = 1;
        }

        public void DoubleTimeScale()
        {
            if (BattleController.instance.SpeedGame == 2)
            {
                BattleController.instance.SpeedGame = 1;
                doubleText.text = "X1";
            }
            else
            {
                BattleController.instance.SpeedGame = 2;
                doubleText.text = "X2";
            }
        }
        public void GetHpEnemyArena(int maxHp, int curHp)
        {
            if (maxHp <= 0) return;
            hpEnemy.UpdateHpBar(maxHp, curHp);
            HpEnemyText.text = (curHp + "/" + maxHp).ToString();
        }

        public void GetHpHeroArena(int maxHp, int curHp)
        {
            if (maxHp <= 0) return;
            hpHero.UpdateHpBar(maxHp, curHp);
            HpHeroText.text = (curHp + "/" + maxHp).ToString();
        }
        public void LoadRankArena()
        {
            MyFunction.ClearChild(rankHolder);
            for (int i = 1; i <= 10; i++)
            {
                if (i > HeroAssets.Instance.RankDatas.rankdatas.Count) return;
                ChangeRank rank = Instantiate(changeRankPrefab);
                rank.transform.SetParent(rankHolder, false);
                rank.InitRank(HeroAssets.Instance.RankDatas.rankdatas[i - 1], i);
            }
            UpdateInfor();
        }
        public void CanCelBattle()
        {
            if (battle != null)
            {
                StopCoroutine(battle);
            }
            LoadingObj.SetActive(false);
            RankObj.SetActive(true);
            ButtonObj.SetActive(true);
            //TopObj.SetActive(true);
        }

        public void Battle()
        {
            SoundManager.instance.PressButtonAudio();
            if (!BattleController.instance.checkHeroOnTeam())
            {
                UIManager.instance.Warning();
                return;
            }
            winObj.SetActive(false);
            loseObj.SetActive(false);
            BattleController.instance.indexTurn = 0;
            if (battle != null)
            {
                StopCoroutine(battle);
            }
            battle = StartCoroutine(Loading());
            Debug.Log("Battle");
        }


        Coroutine battle;
        private IEnumerator Loading()
        {
            float timer = UnityEngine.Random.Range(1f, 4f);
            LoadingObj.SetActive(true);
            ButtonObj.SetActive(false);
            //TopObj.SetActive(false);
            RankObj.SetActive(false);
            yield return new WaitForSeconds(timer);
            LoadingObj.SetActive(false);
            //PvPObj.GetComponent<BackGroundMove>().ResetPosition();
            //InitDetailPvP();
            //DOVirtual.DelayedCall(2f, delegate
            //{
            //    BattleController.instance.battleArena.SpawnBattleArena();
            //    PvPObj.GetComponent<BackGroundMove>().Move();
            //});
            PvPObj.GetComponent<BackGroundMove>().OutAnimationChangeScene();
        }

        private void InitDetailPvP()
        {
            string namePlayer;
            int index = UnityEngine.Random.Range(0, HeroAssets.Instance.NamePlayerDataCfs.NamePlayerDatas.Count);
            int point = UnityEngine.Random.Range(-20, 20);
            namePlayer = HeroAssets.Instance.NamePlayerDataCfs.NamePlayerDatas[index].Name;
            MyFunction.ClearChild(HoldeAvatarEnemy.transform);
            GameObject avatarEnemy;
            avatarEnemy = Instantiate(UIManager.instance.avartarEnemy[HeroAssets.Instance.NamePlayerDataCfs.NamePlayerDatas[index].Id].gameObject);
            avatarEnemy.transform.SetParent(HoldeAvatarEnemy.transform, false);
            avatarEnemy.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            this.namePlayerText.text = namePlayer;
            namePlayerPvPtext.text = UserManager.Instance.DataPlayerController.Name;
            cupHeroText.text = UserManager.Instance.DataPlayerController.point.ToString();
            nameEnemyPvPtext.text = namePlayer;
            int pointtext = UserManager.Instance.DataPlayerController.point + point;
            if (pointtext <= 0)
            {
                pointtext = 0;
            }
            cupEnemyText.text = pointtext.ToString();
        }
        public void Win()
        {
            MyFunction.ClearChild(this.holderChest);
            winObj.SetActive(true);
            loseObj.SetActive(false);
            int point = Random.Range(15, 25);
            UserManager.Instance.DataPlayerController.point += point;
            UserManager.Instance.DataPlayerController.NumberWin++;
            UserManager.Instance.DataPlayerController.gold += 100;
            UserManager.Instance.SaveData();
            winObj.GetComponent<PanelMove>().SetActive(point);
            UpdateInfor();
            //SpawnChest();
        }
        public void SpawnChest()
        {
            ChestType chestType;
            int a;
            a = Random.Range(0, 2000);
            if (a <= 200)
            {
                chestType = ChestType.Epic;
            }
            else if (a > 200 && a <= 400)
            {
                chestType = ChestType.Rare;
            }
            else if (a > 400 && a <= 700)
            {
                chestType = ChestType.Prenium;
            }
            else
            {
                chestType = ChestType.Common;
            }
            AddChest(chestType);
        }
        public void AddChest(ChestType chestType)
        {
            switch (chestType)
            {
                case ChestType.Common:
                    SpawnIcoinChest(chestPres[0]);
                    HeroAssets.Instance.ChestDatas.datas[0].number += 1;
                    break;
                case ChestType.Prenium:
                    SpawnIcoinChest(chestPres[1]);
                    HeroAssets.Instance.ChestDatas.datas[1].number += 1;
                    break;
                case ChestType.Rare:
                    SpawnIcoinChest(chestPres[2]);
                    HeroAssets.Instance.ChestDatas.datas[2].number += 1;
                    break;
                case ChestType.Epic:
                    SpawnIcoinChest(chestPres[3]);
                    HeroAssets.Instance.ChestDatas.datas[3].number += 1;
                    break;
                    //case ChestType.Legendary:
                    //    SpawnIcoinChest(chestPres[4]);
                    //    HeroAssets.Instance.ChestDatas.datas[4].number += 1;
                    //    break;
            }
            HeroAssets.Instance.SaveChestData();
        }
        public void SpawnIcoinChest(GameObject gameObject)
        {
            GameObject chest = Instantiate(gameObject, holderChest);
            chest.transform.SetParent(holderChest, false);
            ScaleInforHero(chest);
        }
        private void ScaleInforHero(GameObject a)
        {
            a.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.1f)
            .SetEase(Ease.Linear)
            .SetDelay(0f)
            .OnComplete(() =>
            {
                Scale(a);
            });
        }
        private void Scale(GameObject gameObject)
        {
            gameObject.transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), 0.2f)
        .SetEase(Ease.Linear)
        .SetDelay(0f)
        .OnComplete(() =>
        {
            gameObject.transform.DOScale(Vector3.one, 0.2f)
            .SetEase(Ease.Linear)
            .SetDelay(0f)
            .OnComplete(() =>
            {
            });
        });
        }
        public void Lose()
        {
            winObj.SetActive(false);
            loseObj.SetActive(true);
            int point = UnityEngine.Random.Range(15, 25);
            UserManager.Instance.DataPlayerController.point -= point;
            if (UserManager.Instance.DataPlayerController.point <= 0)
            {
                UserManager.Instance.DataPlayerController.point = 0;
            }
            UserManager.Instance.DataPlayerController.NumberLose++;
            //Debug.Log("point win " + UserManager.Instance.DataPlayerController.point);
            UserManager.Instance.SaveData();
            loseObj.GetComponent<PanelMove>().SetActiveLose(point);
            UpdateInfor();
        }

        public void SetAvatarArena()
        {
            for (int i = 0; i < this.HeroAvaListPos.Count; i++)
            {
                MyFunction.ClearChild(this.HeroAvaListPos[i]);
                GameObject avatarHero;
                if (i >= BattleController.instance.battleArena.HeroArenas.Count) continue;
                avatarHero = HeroAssets.Instance.GetAvatar(BattleController.instance.battleArena.HeroArenas[i]);
                avatarHero.transform.SetParent(this.HeroAvaListPos[i].transform, false);
                avatarHero.transform.localScale = Vector3.one;
            }

            for (int i = 0; i < this.EnemyAvaListPos.Count; i++)
            {
                MyFunction.ClearChild(this.EnemyAvaListPos[i]);
                GameObject avatarHero;
                if (i > BattleController.instance.battleArena.EnemyArenas.Count) continue;
                avatarHero = HeroAssets.Instance.GetAvatar(BattleController.instance.battleArena.EnemyArenas[i]);
                avatarHero.transform.SetParent(this.EnemyAvaListPos[i].transform, false);
                avatarHero.transform.localScale = Vector3.one;
            }
            InitInformation();

            CheckAvatarOnOff();
        }

        private void InitInformation()
        {
            string namePlayer;
            int index = Random.Range(0, HeroAssets.Instance.NamePlayerDataCfs.NamePlayerDatas.Count);
            int point = Random.Range(-20, 20);
            namePlayer = HeroAssets.Instance.NamePlayerDataCfs.NamePlayerDatas[index].Name;
            MyFunction.ClearChild(HoldeAvatarEnemy.transform);
            GameObject avatarEnemy;
            avatarEnemy = Instantiate(UIManager.instance.avartarEnemy[HeroAssets.Instance.NamePlayerDataCfs.NamePlayerDatas[index].Id].gameObject);
            avatarEnemy.transform.SetParent(HoldeAvatarEnemy.transform, false);
            avatarEnemy.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            this.BossNametext.text = namePlayer;
            this.PlayerNameText.text = UserManager.Instance.DataPlayerController.Name;
        }
        public void SetTurnAvar()
        {
            List<HeroModel> listHeroModelAvar = heroModelAva();
            //Debug.Log("listHeroModelAvar.Count" + listHeroModelAvar.Count);
            if (listHeroModelAvar.Count < 7) return;
            for (int i = 0; i < TurnLstPos.Count; i++)
            {
                MyFunction.ClearChild(TurnLstPos[i]);
                GameObject avartarturn;
                avartarturn = HeroAssets.Instance.GetAvatar(listHeroModelAvar[i]);
                //avartarturn = HeroAssets.Instance.GetAvatar(BattleController.instance.battleArena.TurnHeroArenas[BattleController.instance.battleArena.indexTurn + i]);
                avartarturn.transform.SetParent(TurnLstPos[i].transform, false);
                avartarturn.transform.localScale = Vector3.one;
            }
        }
        public List<HeroModel> heroModelAva()
        {
            int index = 0;
            List<HeroModel> heroModelAvars = new List<HeroModel>();
            int a = BattleController.instance.battleArena.indexTurn;
            for (int i = a; i < BattleController.instance.battleArena.TurnHeroArenas.Count; i++)
            {
                if (BattleController.instance.battleArena.TurnHeroArenas[i].CurHP <= 0)
                {
                    continue;
                }
                else
                {
                    heroModelAvars.Add(BattleController.instance.battleArena.TurnHeroArenas[i]);
                    index++;

                    if (index >= 7) return heroModelAvars;
                }
            }
            return null;
        }
        public void CheckAvatarOnOff()
        {
            for (int i = 0; i < BattleController.instance.battleArena.HeroArenas.Count; i++)
            {
                if (BattleController.instance.battleArena.HeroArenas[i].CurHP <= 0)
                {
                    avatarHeros[i].gameObject.SetActive(true);
                }
                else
                {
                    avatarHeros[i].gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < BattleController.instance.battleArena.EnemyArenas.Count; i++)
            {
                if (BattleController.instance.battleArena.EnemyArenas[i].CurHP <= 0)
                {
                    avatarEnemys[i].gameObject.SetActive(true);
                }
                else
                {
                    avatarEnemys[i].gameObject.SetActive(false);
                }
            }
        }
        public void NextState()
        {
            SoundManager.instance.PressButtonAudio();
            Battle();
            UpdateInfor();
        }

        public void BackState()
        {
            SoundManager.instance.PressButtonAudio();
            RankObj.SetActive(true);
            winObj.SetActive(false);
            loseObj.SetActive(false);
            buttomObj.SetActive(true);
            //TopObj.SetActive(true);
            UpdateInfor();
        }
        private void UpdateInfor()
        {
            cupNumberText.text = UserManager.Instance.DataPlayerController.point.ToString();
            winNumberText.text = UserManager.Instance.DataPlayerController.NumberWin.ToString();
            loseNumberText.text = UserManager.Instance.DataPlayerController.NumberLose.ToString();
            Debug.Log("point" + UserManager.Instance.DataPlayerController.point);
        }
        public void DoubleSpeed()
        {
            SoundManager.instance.PressButtonAudio();
            if (BattleController.instance.SpeedGame == 2)
            {
                BattleController.instance.SpeedGame = 1;
                doubleText.text = "X1";
            }
            else
            {
                BattleController.instance.SpeedGame = 2;
                doubleText.text = "X2";
            }
            SoundManager.instance.ChangeAudioSpeed();
        }
    }
}
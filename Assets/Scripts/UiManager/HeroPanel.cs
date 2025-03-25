
using DG.Tweening;
using GuanYu.Hero;
using GuanYu.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;


namespace GuanYu.UI
{
    public class HeroPanel : MonoBehaviour
    {
        [Header("abc")]
        public HeroShow heroShowPreb;
        public HeroShop heroShopPreb;
        public Drag DragPreb;

        public Transform holder;
        public List<HeroShow> heroShows;
        //public List<Transform> ListPos;
        public List<AvatarHero> avatarHero;

        public List<Transform> HeroPosOnShop;
        public Transform HeroPosOnShopStart;
        public Transform HeroPosToTargetPanel;

        public RectTransform PosSwap;

        public GameObject warningObj;

        private bool isSpawn = false;

        [SerializeField] private GameObject btnShop;
        [SerializeField] private GameObject btnClaim;
        public bool IsSpawnHeroUi = false;

        public List<int> ListHeroStartGame;

        public List<Sprite> SpriteHeros;
        public List<Sprite> BackGroundHeroUI;
        public HeroShopUI HeroShopUIPrefab;
        public HeroShowUI HeroShowUIPrefab;
        public List<HeroShowUI> heroShowUIs;

        [SerializeField] private GameObject shopObj;
        [SerializeField] private TextMeshProUGUI goldText;
        //[SerializeField] private int PriceShop = 2000;
        [SerializeField] private Button buttonShop;
        [SerializeField] private TextMeshProUGUI goldPriceText;

        [SerializeField] GameObject FX;
        [SerializeField] Transform holderFxShop;
        [Header("ShowInforHero")]
        [SerializeField] private GameObject InforHero;
        [SerializeField] private GameObject InforObj;
        [SerializeField] private TextMeshProUGUI AtkText;
        [SerializeField] private TextMeshProUGUI HpText;
        [SerializeField] private TextMeshProUGUI DefText;
        [SerializeField] private TextMeshProUGUI SpdText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI goldUpdateText;
        [SerializeField] private Transform CharAva;
        [SerializeField] private Button buttonUpdateHero;
        //public void OnUI()
        //{
        //    ListHeroStartGame = new List<int>(UserManager.Instance.UserData.HeroTeams);
        //    if (gameObject.activeSelf) return;
        //    if (IsSpawnHeroUi) return;
        //    IsSpawnHeroUi = true;
        //    this.heroShows.Clear();
        //    foreach (Transform item in holder)
        //    {
        //        item.gameObject.SetActive(false);
        //        if (item.TryGetComponent<HeroShow>(out HeroShow show)) { this.heroShows.Add(show); }
        //    }
        //    for (int i = 0; i < UserManager.Instance.HeroDatas.heroDatas.Count; i++)
        //    {
        //        HeroShow heroShow;
        //        try
        //        {
        //            heroShow = heroShows[i];
        //        }
        //        catch (Exception e)
        //        {
        //            heroShow = Instantiate(heroShowPreb);
        //            this.heroShows.Add(heroShow);
        //        }
        //        heroShow.gameObject.SetActive(true);
        //        //heroShow.SetData(HeroAssets.Instance.HeroDataCfs.HeroDatas[i]);
        //        heroShow.SetData(UserManager.Instance.HeroDatas.heroDatas[i]);
        //        heroShow.transform.SetParent(this.holder);
        //        heroShow.transform.localScale = Vector3.one;
        //        if (CheckHeroOnTeam(heroShow))
        //        {
        //            heroShow.OneClick();
        //        }
        //    }

        //}

        private void OnDisable()
        {
            shopObj.SetActive(false);
            //IsSpawnHeroUi = false;
        }
        private void OnEnable()
        {
            OnUI();
            SetNotice();
        }

        public void SetNotice()
        {
            for (int i = 0; i < this.heroShowUIs.Count; i++)
            {
                this.heroShowUIs[i].ShowNotice();
            }
        }

        public void Exit()
        {
            SoundManager.instance.PressButtonAudio();
            //if (btnClaim.activeInHierarchy)
            //{
            //    Claim();
            //}
            shopObj.SetActive(false);
            SetNotice();
        }
        public void OpenShop()
        {
            SoundManager.instance.PressButtonAudio();
            shopObj.SetActive(true);
            goldText.text = UserManager.Instance.DataPlayerController.gold.ToString();
            goldPriceText.text = Contans.PriceShop.ToString();
            CheckOnOffBtn();
        }
        public void OnUI()
        {
            //if (gameObject.activeSelf) return;
            ListHeroStartGame = new List<int>(UserManager.Instance.UserData.HeroTeams);
            if (IsSpawnHeroUi) return;
            IsSpawnHeroUi = true;
            this.heroShowUIs.Clear();
            foreach (Transform item in holder)
            {
                item.gameObject.SetActive(false);
                if (item.TryGetComponent<HeroShowUI>(out HeroShowUI show)) { this.heroShowUIs.Add(show); }
            }
            for (int i = 0; i < UserManager.Instance.HeroDatas.heroDatas.Count; i++)
            {
                HeroShowUI HeroShowUI;
                try
                {
                    HeroShowUI = heroShowUIs[i];
                }
                catch (Exception e)
                {
                    HeroShowUI = Instantiate(HeroShowUIPrefab);
                    this.heroShowUIs.Add(HeroShowUI);
                }
                HeroShowUI.gameObject.SetActive(true);
                //heroShow.SetData(HeroAssets.Instance.HeroDataCfs.HeroDatas[i]);
                HeroShowUI.InitHeroInv(UserManager.Instance.HeroDatas.heroDatas[i]);
                HeroShowUI.transform.position = Vector3.zero;
                HeroShowUI.transform.SetParent(this.holder);
                HeroShowUI.transform.localScale = Vector3.one;
                HeroShowUI.ShowNotice();
                if (CheckHeroOnTeam(HeroShowUI))
                {
                    HeroShowUI.OneClick();
                }
            }

        }
        public bool CheckHeroOnTeam(HeroShowUI HeroShowUI)
        {
            for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
            {
                if (UserManager.Instance.UserData.HeroTeams[i] == HeroShowUI.HeroData.Index)
                {
                    //heroShow.HeroIndexOnTeam = i;
                    return true;
                }
            }
            return false;
        }
        // init hero treen phaanf chon tuownsg 
        public void InitTeamUI()
        {
            for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
            {
                if (UserManager.Instance.UserData.HeroTeams[i] < 0)
                {
                    continue;
                }
                else
                {
                    Drag drag;
                    drag = Instantiate(DragPreb);
                    avatarHero[i].HeroDrag = drag;

                    drag.index = i;
                    drag.drag = drag;
                    drag.transform.SetParent(this.avatarHero[i].transform, false);
                    drag.InitAvaHeroUI(UserManager.Instance.UserData.HeroTeams[i]);
                }
            }
        }

        //public void UpdateTeamUI()
        //{
        //    for (int i = 0; i < this.ListPos.Count; i++)
        //    {
        //        try
        //        {
        //            if (UserManager.Instance.UserData.HeroTeams[i] < 0) throw null;
        //            GameObject go = HeroAssets.Instance.GetHeroUIPrefabByIndex(UserManager.Instance.UserData.HeroTeams[i]);
        //            if (go != null)
        //            {
        //                MyFunction.ClearChild(this.ListPos[i]);
        //                go.transform.SetParent(this.ListPos[i]);
        //                go.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        //                go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            MyFunction.ClearChild(this.ListPos[i]);

        //        }
        //    }

        //}
        public void UpdateTeamUI(int index)
        {
            for (int i = 0; i < this.avatarHero.Count; i++)
            {
                if (avatarHero[i].HeroDrag != null)
                {
                    continue;
                }
                else
                {
                    Drag drag;
                    drag = Instantiate(DragPreb);
                    avatarHero[i].HeroDrag = drag;
                    //avatarHero[i].Id = index;
                    drag.index = i;
                    drag.drag = drag;
                    // thêm 1 dòng avatar
                    //drag.Id = index;
                    drag.transform.SetParent(this.avatarHero[i].transform, false);
                    drag.InitAvaHeroUI(index);
                    return;
                }
            }
        }

        // test
        //public Drag SpawnHero(int index)
        //{
        //    for (int i = 0; i < this.avatarHero.Count; i++)
        //    {
        //        if (avatarHero[i].HeroDrag != null)
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            Drag drag;
        //            drag = Instantiate(DragPreb);
        //            avatarHero[i].HeroDrag = drag;
        //            //avatarHero[i].Id = index;
        //            drag.index = i;
        //            drag.drag = drag;
        //            // thêm 1 dòng avatar
        //            //drag.Id = index;
        //            drag.transform.SetParent(this.avatarHero[i].transform, false);
        //            drag.InitAvaHeroUI(index);
        //            return drag;
        //        }
        //    }
        //    return null;
        //}
        public Drag SpawnHero(int index, int HeroIndexOnTeam)
        {
            Drag drag;
            drag = Instantiate(DragPreb);
            avatarHero[HeroIndexOnTeam].HeroDrag = drag;
            drag.index = HeroIndexOnTeam;
            drag.drag = drag;
            drag.transform.SetParent(this.avatarHero[HeroIndexOnTeam].transform, false);
            drag.InitAvaHeroUI(index);
            return drag;
        }
        public void UpdateData()
        {
            foreach (HeroShow item in heroShows)
            {
                item.UpdateData();
            }
        }
        // buttton de huy hero
        public void Unselect(int slot)
        {

            UnityEngine.Debug.Log("abc");
            if (UserManager.Instance.UserData.HeroTeams[slot] < 0) return;
            if (!UserManager.Instance.Unselect(slot)) return;
            this.UpdateData();
            GameObject Go = avatarHero[slot].HeroDrag.gameObject;
            avatarHero[slot].HeroDrag = null;
            Destroy(Go);
        }

        public void Campain()
        {
            if (CheckWarning())
            {
                warningObj.SetActive(true);
                DOVirtual.DelayedCall(0.5f, delegate
                {
                    warningObj.SetActive(false);
                });
                return;
            }
            // kiểm tra xem có cần thay đổi lại teams

            MenuController.Instance.btnMenuClick(4);
        }

        private bool CheckWarning()
        {
            for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
            {
                if (UserManager.Instance.UserData.HeroTeams[i] >= 0)
                {
                    return false;
                }
            }
            return true;
        }

        [ContextMenu("OpenHeroOnshop")]
        public void OpenHeroOnshop()
        {
            SoundManager.instance.PressButtonAudio();
            GameObject Fx = Instantiate(FX, holderFxShop);
            Fx.transform.position = Vector3.zero;
            Fx.transform.SetParent(holderFxShop, false);
            DOVirtual.DelayedCall(2.7f, delegate
            {
                Destroy(Fx);
                if (!isSpawn)
                {
                    StartCoroutine(SpwanHeroOnshop());
                }
            });

        }
        //IEnumerator SpwanHeroOnshop()
        //{
        //    int indexSpawn = 0;
        //    isSpawn = true;
        //    for (int i = 0; i < HeroPosOnShop.Count; i++)
        //    {
        //        MyFunction.ClearChild(HeroPosOnShop[i]);
        //    }

        //    for (int i = 0; i < HeroPosOnShop.Count; i++)
        //    {
        //        HeroDataCf heroDataCf;
        //        int indexHeroData = HeroAssets.Instance.HeroDataCfDic.ToList()[UnityEngine.Random.Range(0, HeroAssets.Instance.HeroDataCfDic.Count)].Index;
        //        heroDataCf = HeroAssets.Instance.GetHeroDataCfByIndex(indexHeroData);
        //        HeroShop HeroShop;
        //        HeroShop = Instantiate(heroShopPreb, HeroPosOnShopStart.position, Quaternion.identity);
        //        HeroShop.targetHero = HeroPosToTargetPanel.position;
        //        HeroShop.transform.position = HeroPosOnShopStart.position;
        //        HeroShop.transform.SetParent(HeroPosOnShop[i], true);

        //        HeroPosOnShop[i].GetComponent<HeroShopOnPos>().heroShop = HeroShop;

        //        HeroShop.transform.localScale = new Vector3(0f, 0f, 0f);
        //        HeroShop.MoveHero(HeroPosOnShop[i].position);
        //        HeroShop.ScaleHero(heroDataCf);
        //        yield return new WaitForSeconds(0.3f);
        //        indexSpawn++;
        //        if (indexSpawn == HeroPosOnShop.Count)
        //        {
        //            isSpawn = false;
        //            DOVirtual.DelayedCall(0f, delegate
        //            {
        //                btnShop.SetActive(false);
        //                btnClaim.SetActive(true);
        //            });

        //        }
        //    }
        //}
        IEnumerator SpwanHeroOnshop()
        {
            int indexSpawn = 0;
            isSpawn = true;
            UserManager.Instance.DataPlayerController.gold -= Contans.PriceShop;
            goldText.text = UserManager.Instance.DataPlayerController.gold.ToString();
            UserManager.Instance.SaveData();
            for (int i = 0; i < HeroPosOnShop.Count; i++)
            {
                MyFunction.ClearChild(HeroPosOnShop[i]);
            }
            for (int i = 0; i < HeroPosOnShop.Count; i++)
            {
                HeroDataCf heroDataCf;

                //int indexHeroData = HeroAssets.Instance.HeroDataCfDic.ToList()[UnityEngine.Random.Range(0, HeroAssets.Instance.HeroDataCfDic.Count)].Index;
                Rarity rarity = GetRandomRarity();
                List<HeroDataCf> lisHeroRandoms = GetHeroesByRarity(rarity);
                //heroDataCf = HeroAssets.Instance.GetHeroDataCfByIndex(indexHeroData);
                heroDataCf = GetRandomHero(lisHeroRandoms);
                if (heroDataCf == null)
                {
                    Debug.Log("No heroes in the list.");
                }
                HeroShopUI HeroShopUI;
                HeroShopUI = Instantiate(HeroShopUIPrefab, HeroPosOnShopStart.position, Quaternion.identity);
                HeroShopUI.transform.SetParent(HeroPosOnShop[i], false);
                HeroPosOnShop[i].GetComponent<HeroShopOnPos>().HeroshopUI = HeroShopUI;
                HeroShopUI.transform.localScale = new Vector3(1f, 1f, 1f);
                HeroShopUI.InitHeroDetail(heroDataCf);
                HeroShopUI.Rotate();
                yield return new WaitForSeconds(0.05f);
                indexSpawn++;
                if (indexSpawn == HeroPosOnShop.Count)
                {
                    isSpawn = false;
                    DOVirtual.DelayedCall(0f, delegate
                    {
                        btnShop.SetActive(false);
                        btnClaim.SetActive(true);
                    });
                }
            }
        }

        private HeroDataCf GetRandomHero(List<HeroDataCf> HeroDataCfs)
        {
            if (HeroDataCfs.Count <= 0) return null; // Kiểm tra nếu danh sách rỗng

            int randomIndex = UnityEngine.Random.Range(0, HeroDataCfs.Count); // Lấy chỉ số ngẫu nhiên
            return HeroDataCfs[randomIndex]; // Trả về Hero tại chỉ số ngẫu nhiên
        }
        private List<HeroDataCf> GetHeroesByRarity(Rarity rarity)
        {
            List<HeroDataCf> filteredHeroes = new List<HeroDataCf>();

            foreach (HeroDataCf hero in HeroAssets.Instance.heroDataCfs.HeroDatas)
            {
                if (hero.RarityHero == rarity)
                {
                    filteredHeroes.Add(hero);
                }
            }
            return filteredHeroes;
        }


        private Rarity GetRandomRarity()
        {
            int randomValue = UnityEngine.Random.Range(0, 100);
            if (randomValue < 65) return Rarity.Common;     // 65% cho Common
            if (randomValue < 80) return Rarity.Prenium;    // 15% cho Premium
            if (randomValue < 90) return Rarity.Rare;       // 10% cho Rare
            if (randomValue < 95) return Rarity.Epic;       // 5% cho Epic
            if (randomValue < 99) return Rarity.Legendary;  // 4% cho Legendary
            return Rarity.Mythical;
        }
        private void CheckOnOffBtn()
        {
            if (UserManager.Instance.DataPlayerController.gold >= Contans.PriceShop)
            {
                buttonShop.interactable = true;
            }
            else
            {
                buttonShop.interactable = false;
            }

        }

        public void Claim()
        {
            SoundManager.instance.PressButtonAudio();
            if (HeroUI != null)
            {
                StopCoroutine(HeroUI);
            }
            HeroUI = StartCoroutine(DestroyHeroOnUI());
        }
        Coroutine HeroUI;
        IEnumerator DestroyHeroOnUI()
        {
            int index = 0;
            for (int i = 0; i < HeroPosOnShop.Count; i++)
            {
                index++;
                if (HeroPosOnShop[i].GetComponent<HeroShopOnPos>().HeroshopUI == null) continue;
                HeroPosOnShop[i].GetComponent<HeroShopOnPos>().HeroshopUI.SelectHero();
                GameObject heroUI = HeroPosOnShop[i].GetComponent<HeroShopOnPos>().HeroshopUI.gameObject;
                Destroy(heroUI);
                yield return new WaitForSeconds(0.05f);
                if (index == HeroPosOnShop.Count)
                {
                    btnShop.SetActive(true);
                    btnClaim.SetActive(false);
                    goldPriceText.text = Contans.PriceShop.ToString();
                    CheckOnOffBtn();
                }
            }
            AddHeroOnShop();
        }

        public void AddHeroOnShop()
        {
            this.heroShowUIs.Clear();
            foreach (Transform item in holder)
            {
                item.gameObject.SetActive(false);
                if (item.TryGetComponent<HeroShowUI>(out HeroShowUI show)) { this.heroShowUIs.Add(show); }
            }
            for (int i = 0; i < UserManager.Instance.HeroDatas.heroDatas.Count; i++)
            {
                HeroShowUI HeroShowUI;
                try
                {
                    HeroShowUI = heroShowUIs[i];
                }
                catch (Exception e)
                {
                    HeroShowUI = Instantiate(HeroShowUIPrefab);
                    this.heroShowUIs.Add(HeroShowUI);
                }
                HeroShowUI.gameObject.SetActive(true);
                //heroShow.SetData(HeroAssets.Instance.HeroDataCfs.HeroDatas[i]);
                HeroShowUI.InitHeroInv(UserManager.Instance.HeroDatas.heroDatas[i]);
                HeroShowUI.transform.position = new Vector3(0, 0, 0);
                HeroShowUI.transform.SetParent(this.holder);
                HeroShowUI.transform.localScale = Vector3.one;
            }
        }
        public void ShowInfor(HeroData herodata)
        {
            InforObj.SetActive(true);
            InforHero.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            SpwanAvatarHero(herodata);
            InitInfor(herodata);
            ScaleInforHero();
        }

        private void InitInfor(HeroData herodata)
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(herodata.Index);
            HpText.text = heroDataCf.Hp.ToString();
            AtkText.text = heroDataCf.Atk.ToString();
            DefText.text = heroDataCf.Def.ToString();
            SpdText.text = heroDataCf.Speed.ToString();
            goldUpdateText.text = heroDataCf.GoldLevel[herodata.LevelHero - 1].ToString();
            levelText.text = "Lv." + herodata.LevelHero.ToString();
            CheckBtnOnOff(herodata);
        }
        private void ScaleInforHero()
        {
            InforHero.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.1f)
            .SetEase(Ease.Linear)
            .SetDelay(0f)
            .OnComplete(() =>
            {
                Scale();
            });
        }
        private void Scale()
        {
            InforHero.transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), 0.2f)
        .SetEase(Ease.Linear)
        .SetDelay(0f)
        .OnComplete(() =>
        {
            InforHero.transform.DOScale(Vector3.one, 0.2f)
            .SetEase(Ease.Linear)
            .SetDelay(0f)
            .OnComplete(() =>
            {
            });
        });
        }

        public void SpwanAvatarHero(HeroData herodata)
        {
            MyFunction.ClearChild(this.CharAva);
            UserManager.Instance.indexHeroUpdate = HeroAssets.Instance.GetIndexHeroUpdate(herodata.Index);
            HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(herodata.Index);
            GameObject avaPreb = HeroAssets.Instance.GetHeroUIPrefabByIndex(heroDataCf.Index);
            if (avaPreb != null)
            {
                avaPreb.transform.SetParent(this.CharAva, false);
                avaPreb.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        public void NextHero()
        {
            SoundManager.instance.PressButtonAudio();
            UserManager.Instance.indexHeroUpdate++;
            if (UserManager.Instance.indexHeroUpdate > UserManager.Instance.HeroDatas.heroDatas.Count - 1)
            {
                UserManager.Instance.indexHeroUpdate = 0;
            }
            HeroData heroData = UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate];
            //HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(heroData.Index);
            SpwanAvatarHero(heroData);
            InitInfor(heroData);
        }
        public void PreviousHero()
        {
            SoundManager.instance.PressButtonAudio();
            UserManager.Instance.indexHeroUpdate--;
            if (UserManager.Instance.indexHeroUpdate < 0)
            {
                UserManager.Instance.indexHeroUpdate = UserManager.Instance.HeroDatas.heroDatas.Count - 1;
            }
            HeroData heroData = UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate];
            //HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(heroData.Index);
            SpwanAvatarHero(heroData);
            InitInfor(heroData);
        }

        public void UpdateHero()
        {
            SoundManager.instance.PressButtonAudio();
            HeroData heroData = UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate];
            HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(heroData.Index);
            UserManager.Instance.DataPlayerController.gold -= heroDataCf.GoldLevel[heroData.LevelHero - 1];
            heroData.LevelHero++;
            InitInfor(heroData);
            UserManager.Instance.SaveData();
            UserManager.Instance.SaveHeroData();
        }
        public void CheckBtnOnOff(HeroData heroData)
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(heroData.Index);
            //HeroDataCf heroDataCf = HeroAssets.Instance.GetHeroDataCfByIndex(heroData.Index);
            Debug.Log("heroData.LevelHero - 1" + (heroData.LevelHero - 1));
            Debug.Log("heroDataCf.GoldLevel[heroData.LevelHero - 1]" + heroDataCf.GoldLevel[heroData.LevelHero - 1]);
            if (UserManager.Instance.DataPlayerController.gold >= heroDataCf.GoldLevel[heroData.LevelHero - 1])
            {
                buttonUpdateHero.interactable = true;
            }
            else
            {
                buttonUpdateHero.interactable = false;
            }
        }
        //public void Claim()
        //{
        //    if (isSpawn) return;
        //    if (claim != null)
        //    {
        //        StopCoroutine(claim);
        //    }
        //    claim = StartCoroutine(ClaimHero());
        //}
        //Coroutine claim;
        //IEnumerator ClaimHero()
        //{
        //    int indexClaim = 0;
        //    for (int i = 0; i < HeroPosOnShop.Count; i++)
        //    {
        //        indexClaim++;
        //        if (HeroPosOnShop[i].GetComponent<HeroShopOnPos>().heroShop == null) continue;
        //        yield return new WaitForSeconds(0.3f);
        //        HeroPosOnShop[i].GetComponent<HeroShopOnPos>().heroShop.SelectHero();
        //        if (indexClaim == HeroPosOnShop.Count)
        //        {
        //            DOVirtual.DelayedCall(0f, delegate
        //            {
        //                btnShop.SetActive(true);
        //                btnClaim.SetActive(false);
        //            });
        //        }
        //    }
        //}
    }
}


using GuanYu.Hero;
using GuanYu.User;
using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace GuanYu
{
    public enum ChestAnimationState
    {
        drop,
        snake,
        openfirst,
        flying,
    }
    public class ChestController : MonoBehaviour
    {
        public static ChestController instance;
        //public ChestMenuButton[] BoxMenuButtons;
        public ChestIteam[] chestIteams;
        public ChestData chestData;
        public SkeletonGraphic SpineAnimationChest;
        public int indexChest = -1;
        private bool isSpawn = false;
        [SerializeField] private GameObject btnNext;
        [SerializeField] private GameObject btnPrev;
        public ChestAnimationState animationState = ChestAnimationState.drop;
        [SerializeField] private Button btnOpen1Chest;
        [SerializeField] private Button btnOpen10Chest;
        public GameObject Chest;
        public GameObject BottomPanelObj;
        public GameObject ChestPanelObj;
        public GameObject BackGroundChestOpen;
        public Transform origiSpinePos;
        public bool active = true;

        [SerializeField] private GearIteamUI iteamPre;
        [SerializeField] private Transform holderIteam;
        [SerializeField] private GameObject GoldPanelPre;
        private bool isOpen1Chest;

        [SerializeField] private GameObject NoticeObj;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void OnEnable()
        {
            indexChest = -1;
            this.chestData = null;
            //BtnChestMenuClick(0);
            for (int i = 0; i < chestIteams.Length; i++)
            {
                chestIteams[i].ChestData = HeroAssets.Instance.ChestDatas.datas[i];
            }
            BtnChestMenuClick(chestIteams[0]);
        }
        //public void BtnChestMenuClick(int index)
        //{
        //    if (indexChest != index) isSpawn = false;
        //    if (isSpawn) return;
        //    isSpawn = true;
        //    for (int i = 0; i < BoxMenuButtons.Length; i++)
        //    {
        //        BoxMenuButtons[i].Setfocus(false);
        //    }
        //    indexChest = index;
        //    BoxMenuButtons[index].Setfocus(true);
        //    SetSkin((index + 1).ToString());
        //    InitSpine();
        //    if (indexChest <= 0)
        //    {
        //        btnPrev.SetActive(false);
        //        btnNext.SetActive(true);
        //    }else if (indexChest >= 4)
        //    {
        //        btnNext.SetActive(false);
        //        btnPrev.SetActive(true);
        //    }
        //}
        public void BtnChestMenuClick(ChestIteam chestIteam)
        {
            if (chestIteam.ChestData != this.chestData) isSpawn = false;
            if (isSpawn || !active) return;
            isSpawn = true;
            active = false;
            animationState = ChestAnimationState.drop;
            for (int i = 0; i < this.chestIteams.Length; i++)
            {
                this.chestIteams[i].SetFocus(false);
            }
            chestIteam.SetFocus(true);
            this.chestData = chestIteam.ChestData;
            indexChest = indexchest();
            SetSkin();
            InitSpine();
            if (indexChest <= 0)
            {
                btnPrev.SetActive(false);
                btnNext.SetActive(true);
            }
            else if (indexChest >= 3)
            {
                btnNext.SetActive(false);
                btnPrev.SetActive(true);
            }
            else
            {
                btnPrev.SetActive(true);
                btnNext.SetActive(true);
            }
            SetOnOffBtn();
        }

        public int indexchest()
        {
            int index = -1;
            for (int i = 0; i < this.chestIteams.Length; i++)
            {
                if (this.chestIteams[i].ChestData == this.chestData)
                {
                    index = i;
                    return index;
                }
            }
            return index;
        }
        //public void SetSkin(string index)
        //{
        //    if (SpineAnimationChest != null)
        //    {
        //        SpineAnimationChest.Skeleton.SetSkin(index);
        //        SpineAnimationChest.Skeleton.SetSlotsToSetupPose();
        //        SpineAnimationChest.UpdateMesh();
        //    }
        //}
        public void SetSkin()
        {
            ChestDataCf chestDataCf = HeroAssets.Instance.GetChestDataCfById(this.chestData.Id);
            if (SpineAnimationChest != null)
            {
                SpineAnimationChest.Skeleton.SetSkin(chestDataCf.skin);
                SpineAnimationChest.Skeleton.SetSlotsToSetupPose();
                SpineAnimationChest.UpdateMesh();
            }
        }
        public void InitSpine()
        {
            if (SpineAnimationChest != null)
            {
                animationState = ChestAnimationState.drop;
                SpineAnimationChest.AnimationState.SetAnimation(0, "drop", false);
                SpineAnimationChest.AnimationState.Complete += OnSpinDropComplete;

                SpineAnimationChest.AnimationState.Event += HandleAnimationEvent;
            }

        }

        private void OnSpinDropComplete(TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == "drop")
            {
                //SpineAnimationChest.AnimationState.Complete -= OnSpinDropComplete;
                SpineAnimationChest.AnimationState.SetAnimation(0, "shake", true);
                active = true;
            }
            else if (trackEntry.Animation.Name == "shake" & animationState == ChestAnimationState.snake)
            {
                //SpineAnimationChest.AnimationState.Complete -= OnSpinDropComplete;
                animationState = ChestAnimationState.openfirst;
                SpineAnimationChest.AnimationState.SetAnimation(0, "open_first", false);
            }
            else if (trackEntry.Animation.Name == "open_first")
            {
                //animationState = ChestAnimationState.snake;
                //Chest.SetActive(true);
                animationState = ChestAnimationState.flying;
                SpineAnimationChest.AnimationState.SetAnimation(0, "flying_item", false);
            }
        }
        private void HandleAnimationEvent(TrackEntry trackEntry, Spine.Event e)
        {
            if (animationState == ChestAnimationState.flying)
            {
                if (e.Data.Name == "on_card")
                {
                    //animationState = ChestAnimationState.flying;
                    //SpineAnimationChest.AnimationState.SetAnimation(0, "flying_item", false);
                    //Debug.Log("flying");
                    SpineAnimationChest.AnimationState.Complete -= OnSpinDropComplete;
                    animationState = ChestAnimationState.snake;
                    Chest.SetActive(true);
                    SpawnItem();
                    Debug.Log("bay item");
                }
            }
            //else if (animationState == ChestAnimationState.flying)
            //{
            //    if (e.Data.Name == "on_card")
            //    {
            //        animationState = ChestAnimationState.snake;
            //        Debug.Log("bay item");
            //        Chest.SetActive(true);
            //    }
            //}
        }
        public void OpenOneChest()
        {
            SoundManager.instance.PressButtonAudio();
            isOpen1Chest = true;
            SpineAnimationChest.AnimationState.Complete += OnSpinDropComplete;
            this.chestData.number -= 1;
            HeroAssets.Instance.SaveChestData();
            BackGroundChestOpen.SetActive(true);
            SpineAnimationChest.gameObject.transform.SetParent(BackGroundChestOpen.transform, false);
            //animationState = ChestAnimationState.openfirst;
            animationState = ChestAnimationState.snake;
            if (SpineAnimationChest != null)
            {
                SpineAnimationChest.AnimationState.Event += HandleAnimationEvent;
            }
            //SpineAnimationChest.AnimationState.SetAnimation(0, "open_first", false);
            SpineAnimationChest.AnimationState.SetAnimation(0, "shake", false);
            SetOnOffBtn();
            chestIteams[indexChest].SetFocus(true);
        }

        public void OpenTenChest()
        {
            SoundManager.instance.PressButtonAudio();
            isOpen1Chest = false;
            SpineAnimationChest.AnimationState.Complete += OnSpinDropComplete;
            this.chestData.number -= 10;
            HeroAssets.Instance.SaveChestData();
            BackGroundChestOpen.SetActive(true);
            SpineAnimationChest.gameObject.transform.SetParent(BackGroundChestOpen.transform, false);
            animationState = ChestAnimationState.snake;
            if (SpineAnimationChest != null)
            {
                SpineAnimationChest.AnimationState.Event += HandleAnimationEvent;
            }
            SpineAnimationChest.AnimationState.SetAnimation(0, "shake", false);
            SetOnOffBtn();
            chestIteams[indexChest].SetFocus(true);
        }
        public void NextChest()
        {
            SoundManager.instance.PressButtonAudio();
            if (!active) return;
            isSpawn = false;
            indexChest++;
            if (indexChest >= 4)
            {
                btnNext.SetActive(false);
            }
            if (!btnPrev.gameObject.activeSelf)
            {
                btnPrev.SetActive(true);

            }
            BtnChestMenuClick(chestIteams[indexChest]);
        }

        public void PreviousChest()
        {
            SoundManager.instance.PressButtonAudio();
            if (!active) return;
            isSpawn = false;
            indexChest--;
            if (indexChest <= 0)
            {
                btnPrev.SetActive(false);
            }
            if (!btnNext.gameObject.activeSelf)
            {
                btnNext.SetActive(true);
            }
            BtnChestMenuClick(chestIteams[indexChest]);
        }
        public void SetOnOffBtn()
        {
            if (this.chestData.number >= 10)
            {
                btnOpen10Chest.interactable = true;
            }
            else
            {
                btnOpen10Chest.interactable = false;
            }

            if (this.chestData.number >= 1)
            {
                btnOpen1Chest.interactable = true;
            }
            else
            {
                btnOpen1Chest.interactable = false;
            }
        }

        public void OpenPanelChest()
        {
            SoundManager.instance.PressButtonAudio();
            BottomPanelObj.SetActive(false);
            ChestPanelObj.SetActive(true);
        }
        public void CancelChest()
        {
            SoundManager.instance.CloseButtonAudio();
            BottomPanelObj.SetActive(true);
            ChestPanelObj.SetActive(false);
            HomeController.instance.goldText.text = UserManager.Instance.DataPlayerController.gold.ToString();
            // SetNotice();
        }

        public void SpawnItem()
        {
            //if (spawn == null)
            //{
            //    Debug.Log("spwan");
            //    spawn = StartCoroutine(SpwanItem());
            //}
            spawn = StartCoroutine(SpwanItem());
        }
        Coroutine spawn;
        IEnumerator SpwanItem()
        {
            ChestDataCf chestDataCf = HeroAssets.Instance.GetChestDataCfById(this.chestData.Id);
            MyFunction.ClearChild(holderIteam);
            if (isOpen1Chest)
            {
                for (int i = 0; i < 1; i++)
                {
                    yield return new WaitForSeconds(0.1f);
                    RandomChest(chestDataCf.type);
                    //GameObject abc = Instantiate(iteamPre, holderIteam);
                    //abc.transform.SetParent(holderIteam, false);
                    Debug.Log("chest 1 hom");
                }

            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    yield return new WaitForSeconds(0.1f);
                    RandomChest(chestDataCf.type);
                    Debug.Log("Type" + chestDataCf.type);
                    Debug.Log("chest" + i);
                }
            }

        }

        public void Claim()
        {
            SoundManager.instance.PressButtonAudio();
            BackGroundChestOpen.SetActive(false);
            Chest.SetActive(false);
            SpineAnimationChest.gameObject.transform.SetParent(origiSpinePos, false);
            //animationState = ChestAnimationState.snake;
            animationState = ChestAnimationState.drop;
            SpineAnimationChest.AnimationState.SetAnimation(0, "drop", false);
            MyFunction.ClearChild(holderIteam);
        }
        public void RandomChest(ChestType chestType)
        {
            switch (chestType)
            {
                case ChestType.Common:
                    Common();
                    break;
                case ChestType.Prenium:
                    Prenium();
                    break;
                case ChestType.Rare:
                    Rare();
                    break;
                case ChestType.Epic:
                    Epic();
                    break;
                //case ChestType.Legendary:
                //    Legendary();
                //    break;
            }
            GearDataCtrl.Instance.SaveDataGear();
            UserManager.Instance.SaveData();
            UserManager.Instance.SaveHeroData();
        }

        private void Common()
        {
            int a;
            a = UnityEngine.Random.Range(0, 2000);
            if (a <= 10)
            {
                SpwanGear();
            }
            else
            {
                SpwanGold();
            }
        }
        private void Prenium()
        {
            int a;
            a = UnityEngine.Random.Range(0, 2000);
            if (a <= 50)
            {
                SpwanGear();
            }
            else
            {
                SpwanGold();
            }

        }
        private void Rare()
        {
            int a;
            a = UnityEngine.Random.Range(0, 2000);
            if (a <= 100)
            {
                SpwanGear();
            }
            else
            {
                SpwanGold();
            }

        }
        private void Epic()
        {
            int a;
            a = UnityEngine.Random.Range(0, 2000);
            if (a <= 200)
            {
                SpwanGear();
            }
            else
            {
                SpwanGold();
            }

        }
        private void Legendary()
        {
            Debug.Log("spwanItem");
            int a;
            a = UnityEngine.Random.Range(0, 2000);
            if (a <= 1500)
            {
                SpwanGear();
            }
            else
            {
                SpwanGold();
            }
        }
        private void SpwanGear()
        {
            int index;
            index = UnityEngine.Random.Range(0, HeroAssets.Instance.GearDataCfs.GearDatas.Count);
            GearDataCf gearDataCf = HeroAssets.Instance.GearDataCfs.GearDatas[index];
            GearDataCtrl.Instance.CheckGearOnChest(gearDataCf.ID);
            GearIteamUI abc = Instantiate(iteamPre, holderIteam);
            abc.gameObject.transform.SetParent(holderIteam, false);
            abc.icoinGearImg.sprite = HeroAssets.Instance.GetSprite(gearDataCf.Name);
            abc.SetRarity(gearDataCf.Rarity);
            abc.GetComponent<Button>().enabled = false;
            AchivementDataCtrl.Instance.DoneAchiveMent(AchivementType.FindEquipment);

        }
        private void SpwanGold()
        {
            GameObject goldPanel = Instantiate(GoldPanelPre, holderIteam);
            goldPanel.transform.SetParent(holderIteam, false);
            UserManager.Instance.DataPlayerController.gold += 100;
            UserManager.Instance.SaveData();
        }

        public bool Notice()
        {
            for (int i = 0; i < chestIteams.Length; i++)
            {
                if (chestIteams[i].ChestData.number > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public void SetNotice()
        {
            if (Notice())
            {
                NoticeObj.SetActive(true);    
            }else
            {
                NoticeObj.SetActive(false);
            }
        }
    }
}
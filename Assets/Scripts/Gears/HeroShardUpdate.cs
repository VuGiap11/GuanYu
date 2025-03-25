using DG.Tweening;
using GuanYu.Hero;
using GuanYu.User;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class HeroShardUpdate : MonoBehaviour
    {
        [SerializeField] private Transform Holder;
        [SerializeField] private GameObject HeroShardPrefab;
        [SerializeField] private Image curAvartar;
        [SerializeField] private Image newAvartar;
        [SerializeField] private List<GameObject> StarLstCur;
        [SerializeField] private List<GameObject> StarLstNew;
        public List<GearUpdate> GearUpdates;
        public Button updateBtn;

        [SerializeField] GameObject FxWinUpdate;
        [SerializeField] GameObject FXLoseUpdate;
        [SerializeField] Transform holderFx;
        [SerializeField] GameObject WinUpdate;
        [SerializeField] GameObject LoseUpdate;
        [SerializeField] private List<GameObject> StarLsts;
        [SerializeField] GameObject FX;
        [SerializeField] Transform holderFxUpdate;
        public void OnDisable()
        {
            GearController.instance.SpawnAvatarHeroOnGear();
            GearController.instance.SetNoticeUpdate();
        }

        public void InitHeroShard(HeroData heroData)
        {
            SetCurStar(heroData);
            HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(heroData.Index);
            curAvartar.sprite = HeroAssets.Instance.GetSprite(heroDataCf.Name);
            newAvartar.sprite = HeroAssets.Instance.GetSprite(heroDataCf.Name);
            MyFunction.ClearChild(Holder);
            for (int i = 0; i < heroData.HeroShard; i++)
            {
                GameObject heroshard = Instantiate(HeroShardPrefab);
                heroshard.transform.SetParent(Holder, false);
                heroshard.GetComponent<SetAvatar>().Setavatar(heroData);
            }
            for (int i = 0; i < GearUpdates.Count; i++)
            {
                GearUpdates[i].addAvar = false;
                GearUpdates[i].avarImage.gameObject.SetActive(false);
            }
            CheckBtnUpdate();
        }

        private void SetCurStar(HeroData heroData)
        {
            for (int i = 0; i < StarLstCur.Count; i++)
            {
                StarLstCur[i].gameObject.SetActive(i < heroData.star);

            }
            for (int i = 0; i < StarLstNew.Count; i++)
            {
                StarLstNew[i].gameObject.SetActive(i < (heroData.star + 1));
            }
        }
        public void CheckBtnUpdate()
        {
            if (UpdateBtnOnOff())
            {
                updateBtn.interactable = true;
            }
            else
            {
                updateBtn.interactable = false;
            }
        }
        private bool UpdateBtnOnOff()
        {
            for (int i = 0; i < GearUpdates.Count; i++)
            {
                if (!GearUpdates[i].addAvar)
                {
                    return false;
                }
            }
            return true;
        }

        public void UpdateHero()
        {
            SoundManager.instance.PressButtonAudio();
            int index = Random.Range(0, 100);
            GameObject Fx = Instantiate(FX, holderFxUpdate);
            Fx.transform.position = Vector3.zero;
            Fx.transform.SetParent(holderFxUpdate, false);
            Debug.Log("Update thanh công");
            if (index >= 30 && index <= 80)
            {
                UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].star++;
                //GameObject Fx = Instantiate(FxWinUpdate,holderFx);
                //Fx.transform.position = Vector3.zero;
                //Fx.transform.localScale = new Vector3(300f, 300f,300f);
                //Fx.transform.SetParent(holderFx, false);
                //Debug.Log("Update thanh công");
                SetCurStar(UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate]);
                DOVirtual.DelayedCall(2.7f, delegate
                {
                    //Destroy(Fx);
                    WinUpdate.SetActive(true);
                    SetStar(UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate]);
                    WinUpdate.transform.localScale = Vector3.zero;
                    WinUpdate.transform.DOScale(Vector3.one, 0.2f)
                    .SetEase(Ease.Linear)
                    .SetDelay(0f)
                    .OnComplete(() => {
                        SoundManager.instance.WinUpdate();
                        Destroy(Fx);
                    });
                });
                //WinUpdate.SetActive(true);
                //SetStar(UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate]);
            }
            else
            {
                //GameObject Fx = Instantiate(FXLoseUpdate, holderFx);
                //Fx.transform.position = Vector3.zero;
                //Fx.transform.localScale = new Vector3(300f, 300f,300f);
                //Fx.transform.SetParent(holderFx, false);
                //Debug.Log("Update thất bại");
                DOVirtual.DelayedCall(2.7f, delegate
                {
                    LoseUpdate.SetActive(true);
                    LoseUpdate.transform.localScale = Vector3.zero;
                    LoseUpdate.transform.DOScale(Vector3.one, 0.2f)
                    .SetEase(Ease.Linear)
                    .SetDelay(0f)
                    .OnComplete(() => {
                        SoundManager.instance.DefeatUpdate();
                        Destroy(Fx);
                    });
                    //LoseUpdate.SetActive(true);
                });
                //LoseUpdate.SetActive(true);
            }
            for (int i = 0; i < GearUpdates.Count; i++)
            {
                GearUpdates[i].avarImage.gameObject.SetActive(false);
                GearUpdates[i].addAvar = false;
            }
            UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].HeroShard -= GearUpdates.Count;
            Debug.Log("HeroShard" + UserManager.Instance.HeroDatas.heroDatas[UserManager.Instance.indexHeroUpdate].HeroShard);
            UserManager.Instance.SaveData();
            UserManager.Instance.SaveHeroData();
            updateBtn.interactable = false;
            CheckBtnUpdate();
            GameManager.instance.heroPanel.AddHeroOnShop();
        }
        private void SetStar(HeroData heroData)
        {
            for (int i = 0; i < StarLstCur.Count; i++)
            {
                StarLsts[i].SetActive(i < heroData.star);

            }
        }

    }
}
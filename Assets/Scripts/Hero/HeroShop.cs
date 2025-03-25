using DG.Tweening;
using GuanYu.Hero;
using GuanYu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using GuanYu.User;
using TMPro;
using System.Security.Cryptography;
using JetBrains.Annotations;
using System;

namespace GuanYu
{
    public class HeroShop : MonoBehaviour
    {
        private Vector3 ScaleTo = new Vector3(1f, 1f, 1f);
        public GameObject AvaHero;
        public Transform CharAva;
        public HeroDataCf HeroDataCf;
        public TextMeshProUGUI nameHero;
        public Vector3 targetHero;
        private Vector3 targetScale = new Vector3(0.2f, 0.2f, 0.2f);
        public bool isSellect;
        public void ScaleHero(HeroDataCf heroDataCf)
        {
            transform.DOScale(ScaleTo, 0.1f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                SpawnAvartarOnShop(heroDataCf);
            });
        }
        private void SpawnAvartarOnShop(HeroDataCf heroDataCf)
        {
            this.HeroDataCf = heroDataCf;
            GameObject avaPreb = HeroAssets.Instance.GetAvatarHeroPrefabByIndex(this.HeroDataCf.Index);
            if (avaPreb != null)
            {
                AvaHero = avaPreb;
                AvaHero.transform.SetParent(this.CharAva);
                AvaHero.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                AvaHero.transform.localScale = new Vector3(3f, 3.8f, 3f);
            }
            nameHero.text = this.HeroDataCf.Name.ToString();
        }
        public void MoveHero(Vector3 target)
        {
            transform.DOMove(target, 0.1f)
               .SetEase(Ease.Linear)
               .OnStart(() => { })
           .SetDelay(0f)
           .OnComplete(() =>
           {
           });
        }

        private void ScaleHeroToPanel()
        {
            transform.DOScale(targetScale, 0.1f)
           .SetEase(Ease.Linear)
           .OnComplete(() =>
           {
               Destroy(gameObject);
           });
        }

        public void SelectHero()
        {

            //UserManager.Instance.UserData.HeroDatas.Add(HeroData(UserManager.Instance.UserData.HeroBought[0]);

            if (CheckHeroOnGame(HeroDataCf.Index))
            {
                UserManager.Instance.UserData.HeroBought.Add(HeroDataCf.Index);
                HeroData hero = new HeroData(HeroDataCf.Index);
                UserManager.Instance.HeroDatas.heroDatas.Add(hero);
                UserManager.Instance.SaveHeroData();
            }
            else
            {
                HeroData heroData = HeroAssets.Instance.GetHeroDataByInDex(HeroDataCf.Index);
                heroData.HeroShard += 1;
                UserManager.Instance.SaveHeroData();
            }
            MoveHero(targetHero);
            ScaleHeroToPanel();
        }

        private bool CheckHeroOnGame(int index)
        {
            for (int i = 0; i < UserManager.Instance.UserData.HeroBought.Count; i++)
            {
                if (index == UserManager.Instance.UserData.HeroBought[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

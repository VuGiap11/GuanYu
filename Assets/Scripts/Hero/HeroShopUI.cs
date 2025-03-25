using DG.Tweening;
using GuanYu.Hero;
using GuanYu.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class HeroShopUI : MonoBehaviour
    {
        public Image BackGroud;
        public Image Avatar;
        public GameObject IcoinON;
        public GameObject IcoinOFF;
        public HeroDataCf heroDataCf;

        public void InitHeroDetail(HeroDataCf heroDataCf)
        {
            this.heroDataCf = heroDataCf;
            Avatar.sprite = GameManager.instance.heroPanel.SpriteHeros[heroDataCf.Index];
            BackGroud.sprite = GameManager.instance.heroPanel.BackGroundHeroUI[(int)heroDataCf.RarityHero];    
        }

        public void Rotate()
        {
            transform.DORotate(new Vector3(0f, 180f, 0f), 0.02f, RotateMode.WorldAxisAdd)
            .SetEase(Ease.Linear)
            .SetDelay(0f)
            .OnComplete(() =>
            {
                IcoinOFF.SetActive(false);
                IcoinON.SetActive(true);
            });
        }
        public void SelectHero()
        {

            //UserManager.Instance.UserData.HeroDatas.Add(HeroData(UserManager.Instance.UserData.HeroBought[0]);

            if (CheckHeroOnGame(heroDataCf.Index))
            {
                
                HeroData hero = new HeroData(heroDataCf.Index);
                UserManager.Instance.HeroDatas.heroDatas.Add(hero);
                UserManager.Instance.UserData.HeroBought.Add(hero.Index);
                AchivementDataCtrl.Instance.DoneAchiveMent(AchivementType.FindMonters);
               
            }
            else
            {
                HeroData heroData = HeroAssets.Instance.GetHeroDataByInDex(heroDataCf.Index);
                heroData.HeroShard += 1;
            }
            UserManager.Instance.SaveHeroData();
            UserManager.Instance.SaveData();
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
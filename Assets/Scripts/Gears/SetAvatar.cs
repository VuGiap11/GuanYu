using GuanYu.Hero;
using GuanYu.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class SetAvatar : MonoBehaviour
    {
        [SerializeField] Image avatar;
        public HeroData herodata;
        public void Setavatar(HeroData heroData)
        {
            this.herodata = heroData;
            avatar.sprite = HeroAssets.Instance.GetSprite(UserManager.Instance.SetHeroDataCf(heroData.Index).Name);
        }

        public void AddShard()
        {
            SoundManager.instance.PressButtonAudio();
            HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(herodata.Index);
            for (int i = 0; i < GearController.instance.heroShardUpdate.GearUpdates.Count; i++)
            {
                if (GearController.instance.heroShardUpdate.GearUpdates[i].addAvar == true) continue;
                GearController.instance.heroShardUpdate.GearUpdates[i].addAvar = true;
                GearController.instance.heroShardUpdate.GearUpdates[i].avarImage.gameObject.SetActive(true);
                GearController.instance.heroShardUpdate.GearUpdates[i].avarImage.sprite = HeroAssets.Instance.GetSprite(heroDataCf.Name);
                gameObject.SetActive(false);
                GearController.instance.heroShardUpdate.CheckBtnUpdate();
                return;
            }
        }
    }
}
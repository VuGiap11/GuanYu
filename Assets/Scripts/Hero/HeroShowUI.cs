using GuanYu.Hero;
using GuanYu.UI;
using GuanYu.User;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class HeroShowUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameHeroText;
        public HeroData HeroData;
        public Image BackGround;
        public Image Avatar;
        [SerializeField] private GameObject IcoinSelectHero;
        [SerializeField] private List<GameObject> Stars = new List<GameObject>();
        private Drag DragHero;
        private bool onTeam = false;
        [SerializeField] private GameObject NoticeObj;
        [SerializeField] private List<Sprite> ListRoleHeroSprites;
        [SerializeField] private Image SpriteRoleHeroType;
        private bool isStartGame = true;

        public void InitHeroInv(HeroData heroData)
        {
            this.HeroData = heroData;
            HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(heroData.Index);
            nameHeroText.text = heroDataCf.Name;
            Avatar.sprite = GameManager.instance.heroPanel.SpriteHeros[heroDataCf.Index];
            BackGround.sprite = GameManager.instance.heroPanel.BackGroundHeroUI[(int)heroDataCf.RarityHero];
            SetStar(heroData.star);
            SetSpriteRoleHeroType(heroDataCf.RoleHeroType);
        }
        private void SetStar(int index)
        {
            for (int i = 0; i < Stars.Count; i++)
            {
                Stars[i].SetActive(false);
                if (i < index)
                {
                    Stars[i].SetActive(true);
                }
            }

        }
        public void OneClick()
        {
            if (!isStartGame)
            {
                SoundManager.instance.PressButtonAudio();
            }
            isStartGame = false;
            int HeroIndexOnTeam = IndexHeroTeam();
            if (!onTeam)
            {
                if (HeroIndexOnTeam >= 0)
                {
                    UserManager.Instance.UserData.HeroTeams[HeroIndexOnTeam] = this.HeroData.Index;
                    UserManager.Instance.SaveData();
                    this.DragHero = MenuController.Instance.heroPanel.SpawnHero(this.HeroData.Index, HeroIndexOnTeam);
                    onTeam = true;
                }
            }
            else
            {
                if (HeroIndexOnTeam >= 0)
                {
                    onTeam = false;
                    UserManager.Instance.UserData.HeroTeams[HeroIndexOnTeam] = -1;
                    GameObject Go = this.DragHero.gameObject;
                    Destroy(Go);
                    HeroIndexOnTeam = -1;
                }
            }
            UserManager.Instance.SaveData();    
            this.UpdateData();
        }

        public void ShowNotice()
        {
            if (this.HeroData.HeroShard >= 7)
            {
                NoticeObj.SetActive(true);
                //Debug.Log("an thong bao");
            }
            else
            {
                NoticeObj.SetActive(false);
                //Debug.Log("an thong bao");
            }
        }
        private int IndexHeroTeam()
        {
            for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
            {
                if (UserManager.Instance.UserData.HeroTeams[i] == this.HeroData.Index)
                {
                    return i;
                }
            }
            for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
            {
                if (UserManager.Instance.UserData.HeroTeams[i] < 0)
                {
                    return i;
                }
            }
            return -1;
        }

        private void UpdateData()
        {
            if (!onTeam)
            {
                IcoinSelectHero.SetActive(false);
            }
            else
            {
                IcoinSelectHero.SetActive(true);
            }
        }
        public void UpdatHero()
        {
            //UserManager.Instance.indexHeroUpdate = HeroData.Index;
            UserManager.Instance.indexHeroUpdate = HeroAssets.Instance.GetIndexHeroUpdate(this.HeroData.Index);
            if (UserManager.Instance.indexHeroUpdate < 0) return;
            ParseToGear();
        }

        public void ParseToGear()
        {
            MenuController.Instance.btnMenuClick(3);
        }

        public void Showinfor()
        {
            SoundManager.instance.PressButtonAudio();
            GameManager.instance.heroPanel.ShowInfor(this.HeroData);
        }
        private void SetSpriteRoleHeroType(RoleHeroType RoleHeroType)
        {
            switch (RoleHeroType)
            {
                case RoleHeroType.Ad:
                    SpriteRoleHeroType.sprite = ListRoleHeroSprites[0];
                    //Debug.Log("ad");
                    break;
                case RoleHeroType.Support:
                    SpriteRoleHeroType.sprite = ListRoleHeroSprites[1];
                    //Debug.Log("support");
                    break;
                case RoleHeroType.Mid:
                    SpriteRoleHeroType.sprite = ListRoleHeroSprites[2];
                   // Debug.Log("mid");
                    break;
                case RoleHeroType.Jungle:
                    SpriteRoleHeroType.sprite = ListRoleHeroSprites[3];
                   // Debug.Log("jungle");
                    break;
                case RoleHeroType.Top:
                    SpriteRoleHeroType.sprite = ListRoleHeroSprites[4];
                    //Debug.Log("top");
                    break;
            }
        }
    }
}


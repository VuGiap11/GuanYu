using DG.Tweening;
using GuanYu.UI;
using GuanYu.User;
using TMPro;
using UnityEngine;

namespace GuanYu.Hero
{
    public class HeroShow : MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI TextName;
        public GameObject AvaHero;
        public Transform CharAva;
        public HeroData HeroData;
        private Drag DragHero;
        private bool onTeam = false;
        private int HeroIndexOnTeam = -1; // vi tri hero

        //public void SetData(HeroDataCf heroDataCf)
        //{
        //    this.HeroDataCf = heroDataCf;
        //    if (this.AvaHero != null) Destroy(this.AvaHero);
        //    this.TextName.text = heroDataCf.Name.ToString();

        //    GameObject avaPreb = HeroAssets.Instance.GetAvatarHeroPrefabByIndex(heroDataCf.Index);
        //    if (avaPreb != null)
        //    {
        //        AvaHero = avaPreb;
        //        AvaHero.transform.SetParent(this.CharAva);
        //        AvaHero.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        //       AvaHero.transform.localScale = new Vector3(1f, 1.35f, 1f);
        //    }
        //    this.UpdateData();
        //}

        public void SetData(HeroData heroData)
        {
            this.HeroData = heroData;
            HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(this.HeroData.Index);
            if (this.AvaHero != null) Destroy(this.AvaHero);
            this.TextName.text = heroDataCf.Name.ToString();

            GameObject avaPreb = HeroAssets.Instance.GetAvatarHeroPrefabByIndex(heroData.Index);
            if (avaPreb != null)
            {
                AvaHero = avaPreb;
                AvaHero.transform.SetParent(this.CharAva, false);
                AvaHero.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
            }
            this.UpdateData();
        }

        //public void OneClick()
        //{
        //    if (!onTeam)
        //    {
        //        if (GameManager.instance.heroPanel.CheckHeroOnTeam(this))
        //        {
        //            this.DragHero = HomeController.Instance.heroPanel.SpawnHero(this.HeroData.Index, this.HeroIndexOnTeam);
        //            onTeam = true;
        //        }
        //        else
        //        {
        //            if (CheckHeroOnTeamNew())
        //            {
        //                UserManager.Instance.UserData.HeroTeams[this.HeroIndexOnTeam] = this.HeroData.Index;
        //                UserManager.Instance.SaveData();

        //                this.DragHero = HomeController.Instance.heroPanel.SpawnHero(this.HeroData.Index, this.HeroIndexOnTeam);
        //                onTeam = true;
        //            }
        //        }

        //    }
        //    else
        //    {
        //        onTeam = false;
        //        UserManager.Instance.UserData.HeroTeams[this.HeroIndexOnTeam] = -1;
        //        GameObject Go = this.DragHero.gameObject;
        //        Destroy(Go);
        //    }
        //    this.UpdateData();
        //}

        //private bool CheckHeroOnTeamNew()
        //{
        //    for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
        //    {
        //        if (UserManager.Instance.UserData.HeroTeams[i] < 0)
        //        {
        //            this.HeroIndexOnTeam = i;
        //            return true;
        //        }
        //    }
        //    return false;
        //}


        public void OneClick()
        {
            DOVirtual.DelayedCall(0f, delegate
            {
                SetTeam();
            });
        }

        public void SetTeam()
        {
          
            IndexHeroTeam();
            if (!onTeam)
            {
                //IndexHeroTeam();
                if (this.HeroIndexOnTeam >= 0)
                {
                    UserManager.Instance.UserData.HeroTeams[this.HeroIndexOnTeam] = this.HeroData.Index;
                    UserManager.Instance.SaveData();
                    this.DragHero = MenuController.Instance.heroPanel.SpawnHero(this.HeroData.Index, this.HeroIndexOnTeam);
                    onTeam = true;
                }
            }
            else
            {
                if (this.HeroIndexOnTeam >= 0)
                {
                    onTeam = false;
                    UserManager.Instance.UserData.HeroTeams[this.HeroIndexOnTeam] = -1;
                    GameObject Go = this.DragHero.gameObject;
                    Destroy(Go);
                    this.HeroIndexOnTeam = -1;
                }
            }
            this.UpdateData();
        }


        public int IndexHeroTeam()
        {
            for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
            {
                if (UserManager.Instance.UserData.HeroTeams[i] == this.HeroData.Index)
                {
                    this.HeroIndexOnTeam = i;
                    return this.HeroIndexOnTeam;
                }
            }
            for (int i = 0; i < UserManager.Instance.UserData.HeroTeams.Count; i++)
            {
                if (UserManager.Instance.UserData.HeroTeams[i] < 0)
                {
                    this.HeroIndexOnTeam = i;
                    return this.HeroIndexOnTeam;
                }
            }
            return this.HeroIndexOnTeam = -1;
        }
        public void UpdateData()
        {
            if (onTeam)
            {
                GetComponent<MenuButton>().SetFocus(true);
            }
            else
            {
                GetComponent<MenuButton>().SetFocus(false);
            }
        }

    }
}


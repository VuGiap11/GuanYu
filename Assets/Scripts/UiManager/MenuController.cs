
using UnityEngine;

namespace GuanYu.UI
{
    public enum Menu
    {
        Home,
        Hero,
        Gear,
        Arena,
        Camp
    }
    public class MenuController : MonoBehaviour
    {
        public static MenuController Instance;
        public MenuButton[] listMenuButtons;
        public GameObject[] MainChoise;
        public HeroPanel heroPanel;
        public Menu Menu;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        public void btnMenuClick(int index)
        {
            SoundManager.instance.PressButtonAudio();
            for (int i = 0; i < listMenuButtons.Length; i++)
            {
                if (listMenuButtons[i].gameObject.activeSelf && i == index) continue;
                listMenuButtons[i].SetFocus(false);
                if (i == 2) continue;
                MainChoise[i].SetActive(false);
            }
            listMenuButtons[index].SetFocus(true);
            MainChoise[index].SetActive(true);
            SoundManager.instance.OffSonud();
            //if (index == 4)
            //{
            //    SoundManager.instance.OnsoundArena();
            //    BattleController.instance.campainType = CampainType.Arena;
            //    //UIManager.instance.battleUI.LoadArena();
            //    UIManager.instance.arenaUI.LoadRankArena();

            //}
            //else if (index == 2)
            //{
            //    SoundManager.instance.OnsoundCampain();
            //    BattleController.instance.campainType = CampainType.Campain;
            //    BattleController.instance.battleCampain.Campain();
            //    UIManager.instance.campainUI.InitGold();
            //    //BattleCtrl.instance.SpawnHeroCampain();
            //}
            //else
            //{
            //    //BattleController.instance.campainType = CampainType.Idle;
            //}
            switch (index)
            {
                case 0:
                    if (Menu == Menu.Home)
                    {
                        btnMenuClick(2);
                    }
                    else
                    {
                        Menu = Menu.Home;
                    }
                    break;
                case 1:
                    if (Menu == Menu.Hero)
                    {
                        btnMenuClick(2);
                    }
                    else
                    {
                        Menu = Menu.Hero;
                    }
                    break;
                case 2:
                    ParseToMenuCampain();
                    break;
                case 3:
                    if (Menu == Menu.Gear)
                    {
                        btnMenuClick(2);
                    }
                    else
                    {
                        Menu = Menu.Gear;
                    }
                    break;
                case 4:
                    if (Menu == Menu.Arena)
                    {
                        btnMenuClick(2);
                    }
                    else
                    {
                        Menu = Menu.Arena;
                        ParsetoMenuArena();
                    }
                    break;
            }
            SoundManager.instance.ChangeAudioSpeed();
        }

        private void ParseToMenuCampain()
        {
            SoundManager.instance.OnsoundCampain();
            //BattleController.instance.campainType = CampainType.Campain;
            Menu = Menu.Camp;
            BattleController.instance.battleCampain.Campain();
            UIManager.instance.campainUI.InitGold();
        }
        private void ParsetoMenuArena()
        {
            SoundManager.instance.OnsoundArena();
           // BattleController.instance.campainType = CampainType.Arena;
            UIManager.instance.arenaUI.LoadRankArena();
        }
    }
}



using GuanYu.Hero;
using GuanYu.UI;
using GuanYu.User;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GuanYu
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public HeroPanel heroPanel;
        [SerializeField] private TextMeshProUGUI nameText;
        public TMP_InputField inputFieldName;
        public GameObject Popup_Name;
        public GameObject MainMenuObj;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            UserManager.Instance.LoadData();
            GearDataCtrl.Instance.LoadDataGear();

            //ParseToCampain();
            //HeroAssets.Instance.AddPlayer();
            //heroPanel.ListHeroStartGame = new List<int>(UserManager.Instance.UserData.HeroTeams);
            //StartGame();
        }
        public void ParseToCampain()
        {
            MenuController.Instance.btnMenuClick(2);
        }

        public void StartGame()
        {
            if (!PlayerPrefs.HasKey(Contans.FirstGame))
            {
                Debug.Log("Welcome to the game for the first time!");
                PlayerPrefs.SetInt(Contans.FirstGame, 1);
                ShowTutorial();
            }
            else
            {
                Debug.Log("Welcome back!");
                Popup_Name.SetActive(false);
                //MainMenuObj.SetActive(false);
                ParseToCampain();
                HeroAssets.Instance.AddPlayer();
                heroPanel.ListHeroStartGame = new List<int>(UserManager.Instance.UserData.HeroTeams);
            }
        }

        private void ShowTutorial()
        {
            Popup_Name.SetActive(true);
        }
        public void OffSceneStartGame()
        {
            //Popup_Name.SetActive(true);
            if (!BattleController.instance.checkHeroOnTeam())
            {
                UserManager.Instance.UserData.HeroTeams = new List<int> { 0, 1, -1, -1, -1 };
                UserManager.Instance.SaveData();
            }
            StartGame();
            MainMenuObj.SetActive(false);
        }
        public void ChoosePlayer()
        {
            SoundManager.instance.PressButtonAudio();
            RankData rankData = HeroAssets.Instance.RankDataPlayer();
            nameText.text = inputFieldName.text;
            UserManager.Instance.DataPlayerController.Name = nameText.text;
            if (string.IsNullOrEmpty(nameText.text))
            {
                UserManager.Instance.DataPlayerController.Name = "Player";
                nameText.text = UserManager.Instance.DataPlayerController.Name;
            }
            rankData.Name = UserManager.Instance.DataPlayerController.Name;
            UserManager.Instance.SaveData();
            Popup_Name.SetActive(false);
            ParseToCampain();
            HeroAssets.Instance.AddPlayer();
            heroPanel.ListHeroStartGame = new List<int>(UserManager.Instance.UserData.HeroTeams);
        }
        public void swap(Drag hero, int oriPos, int targetPos)
        {

            Debug.Log("aaaaaa" + oriPos + "_" + targetPos);

            if (oriPos < 0)
            {
                heroPanel.avatarHero[targetPos].HeroDrag = hero;
                hero.transform.SetParent(heroPanel.avatarHero[targetPos].transform);
                hero.transform.localPosition = Vector3.zero;

            }
            else
            {
                if (heroPanel.avatarHero[targetPos].HeroDrag == null)
                {
                    heroPanel.avatarHero[targetPos].HeroDrag = hero;
                    hero.transform.SetParent(heroPanel.avatarHero[targetPos].transform);
                    hero.transform.localPosition = Vector3.zero;
                    heroPanel.avatarHero[oriPos].HeroDrag = null;
                }
                else
                {
                    Drag temp = heroPanel.avatarHero[oriPos].HeroDrag;
                    heroPanel.avatarHero[oriPos].HeroDrag = heroPanel.avatarHero[targetPos].HeroDrag;
                    heroPanel.avatarHero[targetPos].HeroDrag = temp;

                    hero.transform.SetParent(heroPanel.avatarHero[targetPos].transform);
                    hero.transform.localPosition = Vector3.zero;

                    heroPanel.avatarHero[oriPos].HeroDrag.transform.SetParent(heroPanel.avatarHero[oriPos].transform);
                    heroPanel.avatarHero[oriPos].HeroDrag.transform.localPosition = Vector3.zero;

                    heroPanel.avatarHero[oriPos].HeroDrag.index = heroPanel.avatarHero[targetPos].HeroDrag.index;
                }

                SwapElements(UserManager.Instance.UserData.HeroTeams, oriPos, targetPos);
                UserManager.Instance.SaveData();
            }
        }
        public static void SwapElements(List<int> list, int index1, int index2)
        {
            // Hoán đổi các phần tử
            int temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
        public int CheckNearPos(Vector2 pos)
        {
            int index = 0;
            foreach (AvatarHero avatarhero in heroPanel.avatarHero)
            {
                //Debug.Log("avatarheropos  +  " + avatarhero.transform.position);

                //Debug.Log("heropos + " + pos);
                if (pos.x < avatarhero.transform.position.x + 0.2f && pos.x > avatarhero.transform.position.x - 0.2f && pos.y < avatarhero.transform.position.y + 0.2f && pos.y > avatarhero.transform.position.y - 0.2f)
                {

                    Debug.Log(index);
                    return index;
                }
                index++;
            }
            return -1;
        }

        public bool CheckSameList(List<int> list1, List<int> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

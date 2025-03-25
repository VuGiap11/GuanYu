
using GuanYu.Hero;
using GuanYu.User;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class HomeController : MonoBehaviour
    {
        public static HomeController instance;
        [SerializeField] private TextMeshProUGUI nameText;
        public TextMeshProUGUI goldText;
        [SerializeField] private TextMeshProUGUI goldChangeNameText;
        public TMP_InputField inputFieldName;
        public GameObject Popup_Name;
        private bool changeName = false;
        private int GoldChangeName = 500;

        [SerializeField] private Button btnChangeName;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        public void Init()
        {
            this.nameText.text = UserManager.Instance.DataPlayerController.Name;
            this.goldText.text = UserManager.Instance.DataPlayerController.gold.ToString();
        }
        private void OnDisable()
        {
            this.changeName = false;
            Popup_Name.SetActive(false);
        }
        private void OnEnable()
        {
            this.changeName = true;
            Init();

        }
        public void OpenBoxName()
        {
            SoundManager.instance.PressButtonAudio();
            if (changeName)
            {
                Popup_Name.SetActive(true);
                goldChangeNameText.text = GoldChangeName.ToString();
                //Debug.Log("GoldChangeName" + GoldChangeName);
                inputFieldName.text = UserManager.Instance.DataPlayerController.Name;
                if (this.GoldChangeName > UserManager.Instance.DataPlayerController.gold)
                {
                    btnChangeName.interactable = false;
                    //Debug.Log("khoa btn");
                }
                else
                {
                    btnChangeName.interactable = true;
                }
            }
            else
            {
                Debug.Log("không thể đổi tên");
            }
        }
        public void ChangeName()
        {
            SoundManager.instance.PressButtonAudio();
            RankData rankData = HeroAssets.Instance.RankDataPlayer();
            nameText.text = inputFieldName.text;
            UserManager.Instance.DataPlayerController.Name = nameText.text;
            if (string.IsNullOrEmpty(nameText.text))
            {
                //nameText.text = "Player";
                UserManager.Instance.DataPlayerController.Name = "Player";

                nameText.text = UserManager.Instance.DataPlayerController.Name;
                //Debug.Log("name " + UserManager.Instance.DataPlayerController.Name);
            }

            UserManager.Instance.DataPlayerController.gold -= this.GoldChangeName;
            this.goldText.text = UserManager.Instance.DataPlayerController.gold.ToString();
            rankData.Name = UserManager.Instance.DataPlayerController.Name;
            UserManager.Instance.SaveData();
            Popup_Name.SetActive(false);
        }
    }
}
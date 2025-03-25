using GuanYu.Hero;
using GuanYu.UI;
using GuanYu.User;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class AchivementInfor : MonoBehaviour
    {
        [SerializeField] private Image Avatar;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI nameTaskText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private Button btnClaim;
        [SerializeField] private HpBar manaBarAchi;
        public AchivementData achivementData;
        [SerializeField] private GameObject IconDone;
        public Image targetImage; // Gắn Image UI từ Unity Inspector

        //private AchivementDataCf AchivementDataCf;

        //public void InitAchivement(AchivementData achivementData, AchivementDataCf AchivementDataCf)
        //{
        //    this.achivementData = achivementData;
        //    this.AchivementDataCf = AchivementDataCf;
        //    Avatar.sprite = HeroAssets.Instance.GetSprite(this.AchivementDataCf.Name);
        //    nameText.text = this.AchivementDataCf.Name.ToString();
        //    nameTaskText.text = string.Format(this.AchivementDataCf.NameTask, this.AchivementDataCf.Level[this.achivementData.levelClaim]).ToString();
        //    if (this.achivementData.number >= this.AchivementDataCf.Level[achivementData.levelClaim])
        //    {
        //        btnClaim.interactable = true;
        //        this.achivementData.number = this.AchivementDataCf.Level[this.achivementData.levelClaim];

        //    }
        //    else
        //    {
        //        btnClaim.interactable = false;
        //    }
        //    levelText.text = (this.achivementData.number + "/" + this.AchivementDataCf.Level[this.achivementData.levelClaim]).ToString();
        //    manaBarAchi.UpdateHpBar(this.AchivementDataCf.Level[this.achivementData.levelClaim], this.achivementData.number);
        //}
        public void InitAchivement(AchivementData achivementData)
        {
            this.achivementData = achivementData;
            AchivementDataCf AchivementDataCf = AchivementDataCtrl.Instance.GetAchivementDataCfByID(achivementData.ID);
            Avatar.sprite = HeroAssets.Instance.GetSprite(AchivementDataCf.Name);
            nameText.text = AchivementDataCf.Name.ToString();
            nameTaskText.text = string.Format(AchivementDataCf.NameTask, AchivementDataCf.Level[this.achivementData.levelClaim]).ToString();
            goldText.text = (AchivementDataCf.Gift[this.achivementData.levelClaim]).ToString();
            CheckDone();
            if (this.achivementData.number >= AchivementDataCf.Level[achivementData.levelClaim] && !this.achivementData.isDone)
            {
                btnClaim.interactable = true;
                this.achivementData.number = AchivementDataCf.Level[this.achivementData.levelClaim];
            }
            else
            {
                btnClaim.interactable = false;
            }
            levelText.text = (this.achivementData.number + "/" + AchivementDataCf.Level[this.achivementData.levelClaim]).ToString();
            manaBarAchi.UpdateHpBar(AchivementDataCf.Level[this.achivementData.levelClaim], this.achivementData.number);
        }
        public void Claim()
        {
            SoundManager.instance.PressButtonAudio();
            AchivementDataCf AchivementDataCf = AchivementDataCtrl.Instance.GetAchivementDataCfByID(this.achivementData.ID);
            this.achivementData.number = 0;
            UserManager.Instance.DataPlayerController.gold += AchivementDataCf.Gift[this.achivementData.levelClaim];
            UserManager.Instance.SaveData();
            HomeController.instance.Init();
            this.achivementData.levelClaim++;
            if (this.achivementData.levelClaim >= AchivementDataCf.Level.Count)
            {
                btnClaim.interactable = false;
                this.achivementData.levelClaim = AchivementDataCf.Level.Count-1;
                this.achivementData.number = AchivementDataCf.Level[this.achivementData.levelClaim];
                this.achivementData.isDone = true;
                AchivementDataCtrl.Instance.SaveDateAchivement();
                CheckDone();
                return;
            }
            CheckDone();
            btnClaim.interactable = false;
            levelText.text = (this.achivementData.number + "/" + AchivementDataCf.Level[this.achivementData.levelClaim]).ToString();
            nameTaskText.text = string.Format(AchivementDataCf.NameTask, AchivementDataCf.Level[this.achivementData.levelClaim]).ToString();
            goldText.text = (AchivementDataCf.Gift[this.achivementData.levelClaim]).ToString();
            manaBarAchi.UpdateHpBar(AchivementDataCf.Level[this.achivementData.levelClaim], this.achivementData.number);
            AchivementDataCtrl.Instance.SaveDateAchivement();
        }
        private void CheckDone()
        {
            if (this.achivementData.isDone)
            {
                btnClaim.interactable = false;
                IconDone.SetActive(true);
                ChangeColorItemDone();
            }
            else
            {
                IconDone.SetActive(false);
            }
        }
        public void ChangeColorItemDone()
        {
            if (ColorUtility.TryParseHtmlString(Contans.hexColor, out Color newColor))
            {
                targetImage.color = newColor;
            }
            else
            {
                Debug.LogError("Invalid Hex Color");
            }
        }
    }
}  
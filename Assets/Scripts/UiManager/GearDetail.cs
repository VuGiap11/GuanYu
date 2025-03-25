using DG.Tweening;
using GuanYu.Hero;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class GearDetail : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameGear;
        [SerializeField] private TextMeshProUGUI AtkText;
        [SerializeField] private TextMeshProUGUI DefText;
        [SerializeField] private TextMeshProUGUI HpText;
        [SerializeField] private TextMeshProUGUI SpeedText;
        [SerializeField] private TextMeshProUGUI GearAddTypeText;
        public Image iconGearImg;
        public GearData GearData;
        public GearItem GearItem;
        public GameObject GearObjScale;

        private Vector3 originalScale;
        private Vector3 ScaleTo = new Vector3(1.3f, 1.3f, 1.3f);
        public void ShowInfor(GearItem gearItem,GearData gearData)
        {
            originalScale = GearObjScale.transform.localScale;
            this.GearItem = gearItem;   
            this.GearData = gearData;
            GearDataCf gearDataCf = GearDataCtrl.Instance.GetGearDataCfByIndex(this.GearData.id);
            nameGear.text = gearDataCf.Name.ToString();
            AtkText.text = gearDataCf.ATK.ToString();
            DefText.text = gearDataCf.Def.ToString();
            HpText.text = gearDataCf.Hp.ToString();
            SpeedText.text = gearDataCf.Speed.ToString();
            iconGearImg.sprite = HeroAssets.Instance.GetSprite(gearDataCf.Name);
            for (int i = 0; i < GearController.instance.GearSlots.Count; i++)
            {
                if ((int)gearDataCf.WeaponType == GearController.instance.GearSlots[i].indexslot)
                {
                    if (GearController.instance.GearSlots[i].gearIteamUI == null)
                    {
                        GearAddTypeText.text = "Add";
                    }
                    else
                    {
                        GearAddTypeText.text = "Replace";
                    }
                }
            }
            OnScale();
        }

        //public void AddGear()
        //{
        //    GearController.instance.AddGrear(GearItem, GearData);
        //    UserManager.Instance.SaveHeroData();
        //    GearDataCtrl.Instance.SaveDataGear();
        //    //this.gameObject.SetActive(false);
        //    OffScale();
        //}

        private void OnScale()
        {
            GearObjScale.transform.DOScale(ScaleTo, 0.2f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => { });
        }

        private void OffScale()
        {
            GearObjScale.transform.DOScale(originalScale, 0.2f)
           .SetEase(Ease.InOutSine)
           .OnComplete(() => { this.gameObject.SetActive(false); });
        }

        public void OffGearDetail()
        {
            OffScale();
        }
    }
}
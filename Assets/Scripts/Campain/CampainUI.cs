using GuanYu.UI;
using GuanYu.User;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class CampainUI : MonoBehaviour
    {
        [SerializeField] private HpBar hpEnemyCampain;
        [SerializeField] private TextMeshProUGUI curLeverText;
        [SerializeField] private TextMeshProUGUI nextLeverText;
        [SerializeField] private List<Image> ImagesWave;
        [SerializeField] private TextMeshProUGUI hpEnemyText;
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private TextMeshProUGUI nameText;

        public Transform HolderCampain;

        public void InitLevel(int curlevel, int wave)
        {
            InitUiCampain(curlevel);
            ChangeColorWave(wave);
        }
        public void InitUiCampain(int curlevel)
        {
            curLeverText.text = curlevel.ToString();
            nextLeverText.text = (curlevel +1).ToString();
        }
        public void GetHpEnemy(int maxHp, int curHp)
        {
            if(maxHp <= 0) return;
            hpEnemyCampain.UpdateHpBar(maxHp, curHp);
            hpEnemyText.text = (curHp + "/" + maxHp).ToString();
        }
        public void ChangeColorWave(int wave)
        {
            if (UnityEngine.ColorUtility.TryParseHtmlString(Contans.WavePass, out Color newColor) && UnityEngine.ColorUtility.TryParseHtmlString(Contans.WaveCur, out Color curentColor))
            {
                for (int i = 0; i < ImagesWave.Count; i++)
                {
                    if (i < wave)
                    {
                        ImagesWave[i].color = newColor;
                    }
                    else
                    {
                        ImagesWave[i].color = curentColor;
                    }
                }
            }
            else
            {
                Debug.LogWarning("Invalid Hexadecimal color code");
            }
        }
        public void InitGold()
        {
            goldText.text = UserManager.Instance.DataPlayerController.gold.ToString();
            nameText.text = UserManager.Instance.DataPlayerController.Name.ToString();

        }
    }
}
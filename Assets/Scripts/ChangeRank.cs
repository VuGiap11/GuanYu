using GuanYu.Hero;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeRank : MonoBehaviour
{
    public GameObject MedalObj;
    public GameObject SttObj;
    public TextMeshProUGUI sttText;
    public TextMeshProUGUI namePlayer;
    public TextMeshProUGUI pointPlayer;
    public RankData RankData;

    private void SetRank(int index)
    {
        if (index <= 3)
        {
            MedalObj.SetActive(true);
            SttObj.SetActive(false);
            if (index == 1)
            {
                MedalObj.GetComponent<Image>().sprite = HeroAssets.Instance.GetSprite("ranking_medal_gold");
            }
            else if (index == 2)
            {
                MedalObj.GetComponent<Image>().sprite = HeroAssets.Instance.GetSprite("ranking_medal_silver");
            }
            else if (index == 3)
            {
                MedalObj.GetComponent<Image>().sprite = HeroAssets.Instance.GetSprite("ranking_medal_bronze");
            }
        }
        else
        {
            MedalObj.SetActive(false);
            SttObj.SetActive(true);
        }
    }

    public void InitRank(RankData rankData, int index)
    {
        this.RankData = rankData;
        namePlayer.text = this.RankData.Name.ToString();
        pointPlayer.text = this.RankData.point.ToString();
        SetRank(index);
        sttText.text = (index).ToString();
    }
}

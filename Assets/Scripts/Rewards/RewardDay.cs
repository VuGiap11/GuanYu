using GuanYu.Hero;
using GuanYu.User;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class RewardDay : MonoBehaviour
    {
        public Button BtnReward;
        public GameObject dailyClear;
        public GameObject NextDailyReward;
        public GameObject backGroundReward;
        public double TimeDay;

        [ContextMenu("ReveiveReward")]
        public void ReveiveReward(int gold)
        {
            SoundManager.instance.AudioReward();
            UserManager.Instance.DataPlayerController.gold += gold;
            DoneReceive();
            UserManager.Instance.UserData.indexRewardDay++;
            if (UserManager.Instance.UserData.indexRewardDay >= 7)
            {
                UserManager.Instance.UserData.indexRewardDay= 0;
            }
            PlayerPrefs.SetString(Contans.LastRewartTime, System.DateTime.Now.ToString());
            UserManager.Instance.SaveData();
            //BtnReward.GetComponent<Button>().enabled = false;
            //RewardManager.instance.ResetReward();
            HomeController.instance.Init();
            RewardManager.instance.CheckRewardAvailability();
        }
        public void DoneReceive()
        {
            this.dailyClear.SetActive(true);
            this.BtnReward.interactable = false;
            this.BtnReward.GetComponent<Button>().enabled = false;
            this.backGroundReward.SetActive(false);
            this.NextDailyReward.SetActive(false);
        }
        public void NextDay()
        {
            this.dailyClear.SetActive(false);
            this.BtnReward.interactable = true;
            this.BtnReward.GetComponent<Button>().enabled = false;
            this.backGroundReward.SetActive(false);
            this.NextDailyReward.SetActive(true);
        }
        public void OrigiReward()
        {
            this.BtnReward.interactable = true;
            this.BtnReward.GetComponent<Button>().enabled = false;
            this.backGroundReward.SetActive(false);
            this.NextDailyReward.SetActive(false);
            this.dailyClear.SetActive(false);

        }
        public void RewardCanOpen()
        {
            this.BtnReward.interactable = true;
            this.BtnReward.GetComponent<Button>().enabled = true;
            this.backGroundReward.SetActive(true);
            this.NextDailyReward.SetActive(false);
            this.dailyClear.SetActive(false);
        }
    }
}
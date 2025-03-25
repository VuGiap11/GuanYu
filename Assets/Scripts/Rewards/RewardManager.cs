using GuanYu.User;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class RewardManager : MonoBehaviour
    {
        public static RewardManager instance;
        [SerializeField] List<RewardDay> rewardDays;
        DateTime firstLaunchTime;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            //if (PlayerPrefs.GetString(Contans.FirstRewartTime, "") == "")
            //{
            //    PlayerPrefs.SetString(Contans.FirstRewartTime, DateTime.Now.ToString());
            //}
            //CheckRewardAvailability();
        }
        private void OnEnable()
        {
            CheckRewardAvailability();
        }
        public void CheckRewardAvailability()
        {
            if (PlayerPrefs.HasKey(Contans.LastRewartTime))
            {
                string lastRewardTimeString = PlayerPrefs.GetString(Contans.LastRewartTime);
                DateTime lastRewardTime = DateTime.Parse(lastRewardTimeString);
                DateTime currentDate = DateTime.Now.Date;
                DateTime lastRewardDate = lastRewardTime.Date;
                int daysElapsed = (currentDate - lastRewardDate).Days;
                ResetRewardDay(daysElapsed, false);
            }
            else
            {

                string firstLaunchTimeString = PlayerPrefs.GetString(Contans.FirstRewartTime);
                if (firstLaunchTimeString == null|| firstLaunchTimeString=="")
                {
                    string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
                    PlayerPrefs.SetString(Contans.FirstRewartTime, currentTime);
                    firstLaunchTimeString = currentTime;
                }
                Debug.Log(firstLaunchTimeString);
                try
                {
                     firstLaunchTime = DateTime.Parse(firstLaunchTimeString);
                    Debug.Log("Parsed date: " + firstLaunchTime);
                }
                catch(Exception e)
                {
                    Debug.LogError(e);
                }
                int timeSinceFirstLaunch = (DateTime.Now.Date - firstLaunchTime).Days;
                ResetRewardDay(timeSinceFirstLaunch, true);
                Debug.Log("timeSinceFirstLaunch" + timeSinceFirstLaunch);
            }
        }
        void ShowRewardButton()
        {
            for (int i = 0; i < rewardDays.Count; i++)
            {
                rewardDays[i].backGroundReward.SetActive(false);
                if (i == UserManager.Instance.UserData.indexRewardDay)
                {
                    rewardDays[i].backGroundReward.SetActive(true);
                    rewardDays[i].BtnReward.GetComponent<Button>().enabled = true;
                }
            }
        }
        void HideRewardButton()
        {
            for (int i = 0; i < rewardDays.Count; i++)
            {
                rewardDays[i].NextDailyReward.SetActive(false);
                if (i == UserManager.Instance.UserData.indexRewardDay)
                {
                    rewardDays[i].NextDailyReward.SetActive(true);
                }else if (i < UserManager.Instance.UserData.indexRewardDay)
                {
                    rewardDays[i].DoneReceive();
                }
            }
        }
        public void ResetRewardDay(int timeDay, bool isStartGame)
        {
            //for (int i = 0; i < rewardDays.Count; i++)
            //{
            //    rewardDays[i].OrigiReward();
            //    if (i < UserManager.Instance.UserData.indexRewardDay)
            //    {
            //        rewardDays[i].DoneReceive();
            //    }else if (i == UserManager.Instance.UserData.indexRewardDay && rewardDays[i].TimeDay > timeDay)
            //    {
            //        rewardDays[i].NextDay();
            //    }else if (i == UserManager.Instance.UserData.indexRewardDay && rewardDays[i].TimeDay <= timeDay)
            //    {
            //        rewardDays[i].RewardCanOpen();
            //    }
            //}
            for (int i = 0; i < rewardDays.Count; i++)
            {
                rewardDays[i].OrigiReward();
                if (i < UserManager.Instance.UserData.indexRewardDay)
                {
                    rewardDays[i].DoneReceive();
                }else if (i == UserManager.Instance.UserData.indexRewardDay && UserManager.Instance.UserData.indexRewardDay !=0)
                {
                    if (timeDay >= 1 )
                    {
                        rewardDays[i].RewardCanOpen();
                    }else
                    {
                        rewardDays[i].NextDay();
                    }
                }else if (UserManager.Instance.UserData.indexRewardDay == 0 && isStartGame)
                {
                    rewardDays[0].RewardCanOpen();
                }
                else if (UserManager.Instance.UserData.indexRewardDay == 0 &&
                    !isStartGame)
                {
                    if (timeDay >= 1)
                    {
                        rewardDays[0].RewardCanOpen();
                    }
                    else
                    {
                        rewardDays[0].NextDay();
                    }
                }
                
            }
        }
        public void ResetReward()
        {
            for (int i = 0; i < rewardDays.Count; i++)
            {
                rewardDays[i].backGroundReward.SetActive(false);
                rewardDays[i].NextDailyReward.SetActive(false);
                rewardDays[i].dailyClear.SetActive(false);
                rewardDays[i].BtnReward.GetComponent<Button>().enabled = false;

                if (i < UserManager.Instance.UserData.indexRewardDay)
                {
                    rewardDays[i].dailyClear.SetActive(true);
                    //rewardDays[i].BtnReward.GetComponent<Button>().enabled = false;
                    rewardDays[i].BtnReward.interactable = false;
                }
                if (i == UserManager.Instance.UserData.indexRewardDay)
                {
                    rewardDays[i].NextDailyReward.SetActive(true);
                }
            }
        }
    }
}


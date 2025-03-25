using GuanYu.Hero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuanYu
{
    public class AchivementCtrl : MonoBehaviour
    {
        public static AchivementCtrl Instance;
        [SerializeField] private AchivementInfor AchivemenPre;
        [SerializeField] private Transform holderAchi;
        [SerializeField] private GameObject NoticeObj;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        private void OnEnable()
        {
            InitAchivement();
        }
        [ContextMenu("InitAchivement")]
        public void InitAchivement()
        {
            MyFunction.ClearChild(holderAchi);
            //for (int i = 0; i < HeroAssets.Instance.AchivementDataCfs.AchivementDatas.Count; i++)
            //{
            //    AchivementInfor gameobj = Instantiate(AchivemenPre);
            //    gameobj.transform.SetParent(holderAchi, false);
            //    //gameobj.InitAchivement(AchivementDataCtrl.Instance.AchivementDatas.achivementDatas[i], HeroAssets.Instance.AchivementDataCfs.AchivementDatas[i]);
            //    gameobj.InitAchivement(AchivementDataCtrl.Instance.AchivementDatas.achivementDatas[i]);
            //}
            for (int i = 0; i < AchivementDataCtrl.Instance.AchivementDatas.achivementDatas.Count; i++)
            {
                AchivementInfor gameobj = Instantiate(AchivemenPre);
                gameobj.transform.SetParent(holderAchi, false);
                gameobj.InitAchivement(AchivementDataCtrl.Instance.AchivementDatas.achivementDatas[i]);
            }
        }

        [ContextMenu("AddKill")]
        public void AddKill()
        {
            AchivementData achivementData = AchivementDataCtrl.Instance.GetAchivementDataByID(0);
            achivementData.number += 20;
            AchivementDataCtrl.Instance.SaveDateAchivement();
            InitAchivement();
        }
        public void SetNotice()
        {
            if (AchivementDataCtrl.Instance.Notice())
            {
                NoticeObj.SetActive(true);
            }else
            {
                NoticeObj.SetActive(false);
            }
        }
    }
}


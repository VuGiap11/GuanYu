using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GuanYu
{
    public class PanelMove : MonoBehaviour
    {
        //public GameObject panel;
        //public float moveTime = 0.5f;
        public GameObject panelObj;
        public GameObject pointObj;
        public TextMeshProUGUI pointText;

        //public void Move()
        //{
        //    {
        //        panel.transform.DOMove(endPos.position, moveTime)
        //        .SetEase(Ease.Linear)
        //        .OnStart(() => { })
        //    .SetDelay(0f)
        //    .OnComplete(() =>
        //    {
        //        gameObject.SetActive(false);
        //        MoveBack();
        //    });
        //    }
        //}
        //public void MoveBack()
        //{
        //    {
        //        panel.transform.DOMove(StarPos.position, moveTime)
        //        .SetEase(Ease.Linear)
        //        .OnStart(() => { })
        //    .SetDelay(0f)
        //    .OnComplete(() => { });
        //    }
        //}

        private void OnDisable()
        {
            SetActiveOff();
        }

        public void SetActive(int point)
        {
            if (SetactiveWin != null)
            {
                StopCoroutine(SetactiveWin);
            }

            SetactiveWin = StartCoroutine(SetactiveWinObj(point));

        }
        Coroutine SetactiveWin;
        IEnumerator SetactiveWinObj(int point)
        {
            yield return new WaitForSeconds(Contans.TimeDelay);
            panelObj.SetActive(true);
            yield return new WaitForSeconds(Contans.TimeDelay);
            pointObj.SetActive(true);
            pointText.text = point.ToString();
            yield return new WaitForSeconds(Contans.TimeDelay);
            UIManager.instance.arenaUI.SpawnChest();
        }

        public void SetActiveOff()
        {
            panelObj.SetActive(false);
            pointObj.SetActive(false);
        }

        public void SetActiveLose(int point)
        {
            if (SetactiveLose != null)
            {
                StopCoroutine(SetactiveLose);
            }
            SetactiveLose = StartCoroutine(SetactiveLoseObj(point));

        }
        Coroutine SetactiveLose;
        IEnumerator SetactiveLoseObj(int point)
        {
            yield return new WaitForSeconds(Contans.TimeDelay);
            panelObj.SetActive(true);
            yield return new WaitForSeconds(Contans.TimeDelay);
            pointObj.SetActive(true);
            pointText.text = "-" + point.ToString();
            Debug.Log("xuat hien panel");
        }
    }
}
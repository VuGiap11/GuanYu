using DG.Tweening;
using System;
using UnityEngine;

namespace GuanYu
{
    public class GoldMove : MonoBehaviour
    {
        public RectTransform targetGoldPos;
        public float timeMove = 0.5f;
        public bool isDone;
        public bool isMoveGold = false;
        //public void Move(Action spawn = null)
        //{
        //    if (targetGoldPos == null) return;
        //    transform.DOMove(targetGoldPos.position, timeMove)
        //    .SetEase(Ease.Linear)
        //    .SetDelay(0f)
        //    .OnComplete(() => 
        //    {
        //        if (spawn != null)
        //        {
        //            spawn.Invoke();
        //        }
        //        //GoldCtrl.instance.Golds.Clear();
        //        DestroyGold();

        //    });
        //}
        public void Move(Action action = null)
        {
            isMoveGold = false;
            if (targetGoldPos == null) return;
            transform.DOMove(targetGoldPos.position, timeMove)
            .SetEase(Ease.Linear)
            .SetDelay(0f)
            .OnComplete(() =>
            {
                //GoldCtrl.instance.Golds.Clear();
                //if (isDone)
                //{
                //    BattleController.instance.AddGold();

                //}
                //if (isDone)
                //{
                //    if (action != null)
                //    {
                //        action.Invoke();
                //    }
                //}
                //DestroyGold();
                isMoveGold = true;
            });
        }
        private void DestroyGold()
        {
            Destroy(gameObject);
        }
    }
}
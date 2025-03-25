using DG.Tweening;
using GuanYu.Hero;
using GuanYu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GuanYu
{
    public class PointMove : MonoBehaviour
    {
        private float movetime = 0.3f;
        [SerializeField] private TextMeshProUGUI pointtext;
        public void MovePoint(Vector3 targetPos)
        {
            //transform.DOMove(BattleCtrl.instance.TargetPos, movetime)
                transform.DOMove(targetPos, movetime)
                  .SetEase(Ease.Linear)
                  .OnStart(() => { })
              .SetDelay(0f)
              .OnComplete(() =>
              {
                  Destroy(gameObject);
              });
        }

        public void InitPoint(HeroModel heromodel,int dmg)
        {
            if (!heromodel.isHero)
            {
                ChangeColor(Color.red);
            }
            pointtext.text = dmg.ToString();
        }
        private void ChangeColor(Color newColor)
        {
            if (pointtext != null)
            {
                pointtext.color = newColor;
            }
            else
            {
                Debug.LogError("TextMeshPro component is not assigned!");
            }
        }
    }
}
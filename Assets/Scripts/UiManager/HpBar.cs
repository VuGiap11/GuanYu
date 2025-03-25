
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _hpBarSprite;
        public void UpdateHpBar(int maxHp, int curHp)
        {
            if (maxHp <= 0) return;
            _hpBarSprite.fillAmount = (float)curHp / (float)maxHp;
            //Debug.Log((float)curHp / (float)maxHp);
        }
    }
}



using UnityEngine;
using UnityEngine.UI;

namespace GuanYu.UI
{
    public class MenuButton : MonoBehaviour
    {
        public Image imFocus;
        public void SetFocus(bool focus)
        {
            imFocus.gameObject.SetActive(focus);
        }
    }
}


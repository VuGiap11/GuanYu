using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuanYu
{
    public class ChestMenuButton : MonoBehaviour
    {
        public GameObject IcoinOffChest;
        public GameObject IcoinOnChest;

        public void Setfocus(bool focus)
        {
            IcoinOnChest.SetActive(focus);
            IcoinOffChest.SetActive(!focus);
        }
    }
}
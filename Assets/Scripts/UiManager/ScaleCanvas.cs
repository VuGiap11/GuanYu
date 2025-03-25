using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class ScaleCanvas : MonoBehaviour
    {
        private float scaleValue;
        CanvasScaler ss;
        void Start()
        {
            ss = this.GetComponent<CanvasScaler>();
            ScaleScr();
        }
        void Update()
        {
            ScaleScr();
        }
        public void ScaleScr()
        {
            var x = Screen.width;
            var y = Screen.height;
            scaleValue = ((float)x / y) / ((float)1080 / 1920);
            //ss.matchWidthOrHeight = (int)(scaleValue);
            ss.matchWidthOrHeight = (scaleValue);
        }
    }
}
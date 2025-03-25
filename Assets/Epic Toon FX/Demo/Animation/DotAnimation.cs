using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace GuanYu
{
    public class DotAnimation : MonoBehaviour
    {
        public TextMeshProUGUI dotText; // Gán Text vào đây
        public float interval = 0.2f; // Thời gian giữa các dấu chấm
        private string[] dots = { "", ".", "..", "..." };
        private int currentIndex = 0;
        private float timer;
        private bool isStart;

        void Update()
        {
            SetAnimation();
        }
        private void OnDisable()
        {
            isStart = false;
        }
        private void OnEnable()
        {
            isStart = true;
        }

        public void SetAnimation()
        {
            if (!isStart) return;
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                timer = 0f;
                currentIndex = (currentIndex + 1) % dots.Length;
                dotText.text = dots[currentIndex];
            }
        }
    }
}
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuanYu
{
    public class ArrowMove : MonoBehaviour
    {
        public Transform StartPoint;
        public Transform EndPoint;

        public Vector3 _endPos;
        public Vector3 _startPos;

        public float power = 1f;

        public bool isEnd = false;

        public float _speed = 15;
        public int speedGame;
        
        public Action takeDamage;



        void FixedUpdate()
        {
            this._startPos = this.StartPoint.position;
            this._endPos = this.EndPoint.position;
            this.MoveParabol();


        }

        protected void MoveParabol()
        {
            Vector3 old = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (transform.position == this._endPos)
            {
                this.isEnd = true;
                if (takeDamage != null)
                {
                    takeDamage.Invoke();
                }
                Destroy(gameObject);
            }
            if (this.isEnd)
            {
                return;
            }

            Vector2 nextXZ = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.z), new Vector2(_endPos.x, _endPos.z), this._speed *this.speedGame *Time.fixedDeltaTime);
            float totalDis = Vector2.Distance(new Vector2(_startPos.x, _startPos.z), new Vector2(_endPos.x, _endPos.z));
            float curDis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_startPos.x, _startPos.z));
            float baseY = Mathf.Lerp(this._startPos.y, this._endPos.y, curDis / totalDis);
            float height = this.power * curDis * (curDis - totalDis) / (-0.25f * totalDis * totalDis);
            if (totalDis <= 0)
            {
                baseY = this._endPos.y;
                height = 0;
            }
            this.transform.position = new Vector3(nextXZ.x, baseY + height, nextXZ.y);

            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            transform.right = new Vector3(newPos.x - old.x, newPos.y - old.y, newPos.z - old.z);
        }

      
    }
}



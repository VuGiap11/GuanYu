using DG.Tweening;
using GuanYu.Hero;
using UnityEngine;

namespace GuanYu
{
    public class ArrowAutoMove : MonoBehaviour
    {
        private float moveTime =0.2f;
        //public float rotationSpeed = 3600000f;
        public Vector3 StartPoint;
        public Transform EndPoint;

        public Vector3 _endPos;
        public Vector3 _startPos;

        public float power = 1f;

        public bool isEnd = false;

        public float _speed = 5;
        public int speedGame = 1;
        public HeroModel heromodel;
        public int dmg = 2;
        public void ArrowMove(HeroModel heromodel, Transform target)
        {

            if (heromodel != null)
            {
                //    Vector3 direction = (heromodel.transform.position - transform.position).normalized;
                //    transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                transform.DOMove(heromodel.transform.position, moveTime)
                .SetEase(Ease.Linear)
                .OnStart(() => { })
                 .OnUpdate(() =>
                 {
                     //Vector3 direction = (target.position - transform.position).normalized;
                     //Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
                     ////transform.rotation = targetRotation;
                     //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                 })
                .SetDelay(0f)
                .OnComplete(() =>
                {
                    //TakeDamage(heromodel, 2);
                    Destroy(gameObject);
                });
            }
            else
            {
                //Vector3 direction = (target.position - transform.position).normalized;
                //transform.rotation = Quaternion.LookRotation(direction);
                transform.DOMove(target.position, moveTime)
                .SetEase(Ease.Linear)
                .OnStart(() => { })
                 .OnUpdate(() =>
                 {
                     //Vector3 direction = (target.position - transform.position).normalized;
                     //Quaternion targetRotation = Quaternion.LookRotation(direction);
                     ////transform.rotation = targetRotation;
                     //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                 })
                .SetDelay(0f)
                .OnComplete(() =>
                {
                    //TakeDamage(heromodel, 2);
                    Destroy(gameObject);
                });

            }

        }
        public void ArrowMove()
        {

        }

        //private void TakeDamage()
        //{
        //    heromodel.CurHP -= dmg;
        //    heromodel.HeroSpin.State = Hero.AnimationState.Hurt;
        //    if (heromodel.CurHP <= 0)
        //    {
        //        Dead(heromodel);
        //        AchivementDataCtrl.Instance.DoneAchiveMent(AchivementType.DefeatMonster);
        //    }
        //}
        //private void Dead(HeroModel heromodel)
        //{
        //    if (!heromodel.isDead)
        //    {
        //        heromodel.isDead = true;
        //        heromodel.CurHP = 0;
        //        heromodel.HeroSpin.State = Hero.AnimationState.Dead;
        //        heromodel.HeroSpin.Spin();
        //        BattleController.instance.RemoveTurn(heromodel);
        //    }
        //}

        //void FixedUpdate()
        //{
        //    this._startPos = this.StartPoint;
        //    this._endPos = this.EndPoint.position;
        //    this.MoveParabol();
        //}

        //protected void MoveParabol()
        //{
        //    Vector3 old = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //    if (transform.position == this._endPos)
        //    {
        //        //this.isEnd = true;
        //        //if (takeDamage != null)
        //        //{
        //        //    takeDamage.Invoke();
        //        //}
        //        if (this.heromodel != null)
        //        {
        //            TakeDamage();
        //        }

        //        Destroy(gameObject);
        //    }
        //    if (this.isEnd)
        //    {
        //        return;
        //    }

        //    Vector2 nextXZ = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.z), new Vector2(_endPos.x, _endPos.z), this._speed * this.speedGame * Time.fixedDeltaTime);
        //    float totalDis = Vector2.Distance(new Vector2(_startPos.x, _startPos.z), new Vector2(_endPos.x, _endPos.z));
        //    float curDis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_startPos.x, _startPos.z));
        //    float baseY = Mathf.Lerp(this._startPos.y, this._endPos.y, curDis / totalDis);
        //    float height = this.power * curDis * (curDis - totalDis) / (-0.25f * totalDis * totalDis);
        //    if (totalDis <= 0)
        //    {
        //        baseY = this._endPos.y;
        //        height = 0;
        //    }
        //    this.transform.position = new Vector3(nextXZ.x, baseY + height, nextXZ.y);

        //    Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //    transform.right = new Vector3(newPos.x - old.x, newPos.y - old.y, newPos.z - old.z);
        //}
    }
}
using DG.Tweening;
using System;
using System.Security.Cryptography;
using UnityEngine;

namespace GuanYu.Hero
{

    public class PlayerMovement : MonoBehaviour
    {
        private float moveTime = 0.15f;
        public HeroModel HeroModel;
        public Action DoActionDone;
        [SerializeField] private bool isBack = false;

        //public void Init()
        //{
        //    if (HeroModel.SpineAnimation != null)
        //    {
        //        HeroModel.SpineAnimation.AnimationState.Event += HandleAnimationEvent;
        //    }
        //}
        //public void PlayerMove()
        //{
        //    if (HeroModel.TargetPos == null) return;
        //    Vector3 TargetPos = new Vector3(HeroModel.TargetPos.position.x, HeroModel.TargetPos.position.y, HeroModel.TargetPos.position.z);
        //    transform.DOMove(TargetPos, moveTime)
        //        .SetEase(Ease.Linear)
        //        //.OnStart(() => { StartSpineAnimation("stand", true); })
        //        .OnStart(() => { HeroModel.HeroSpin.State = AnimationState.Idle; HeroModel.HeroSpin.Spin(); })
        //        .SetDelay(1f)
        //    //.OnComplete(() => { HeroModel.HeroSpin.SpineAnimation("attack", false); });
        //    .OnComplete(() => { HeroModel.HeroSpin.State = AnimationState.Attack; HeroModel.HeroSpin.Spin(); });
        //    Debug.Log("A");
        //    //StartCoroutine(MoveBack());
        //}
        //public void PlayerMove()
        //{
        //    HeroModel.isAttacking = true;
        //    //Debug.Log("A");
        //    if (HeroModel.Target == null) return;
        //    if (HeroModel.isHero)
        //    {
        //        Vector3 TargetPos = new Vector3(HeroModel.Target.transform.position.x - 0.7f, HeroModel.Target.transform.position.y, HeroModel.Target.transform.position.z);
        //        transform.DOMove(TargetPos, moveTime)
        //        .SetEase(Ease.Linear)
        //        .OnStart(() => { HeroModel.HeroSpin.State = AnimationState.Idle; HeroModel.HeroSpin.Spin(); })
        //    .SetDelay(0.3f)
        //    .OnComplete(() =>
        //    {
        //        HeroModel.HeroSpin.State = AnimationState.Attack; HeroModel.HeroSpin.Spin();
        //    });
        //    }
        //    else
        //    {

        //        Vector3 TargetPos = new Vector3(HeroModel.Target.transform.position.x + 0.7f, HeroModel.Target.transform.position.y, HeroModel.Target.transform.position.z);
        //        transform.DOMove(TargetPos, moveTime)
        //        .SetEase(Ease.Linear)
        //        .OnStart(() => { HeroModel.HeroSpin.State = AnimationState.Idle; HeroModel.HeroSpin.Spin(); })
        //    .SetDelay(0.3f)
        //    .OnComplete(() =>
        //    {
        //        HeroModel.HeroSpin.State = AnimationState.Attack; HeroModel.HeroSpin.Spin();
        //    });
        //    }
        //}

        //public void PlayerMoveSkill(Vector3 pos)
        //{
        //    HeroModel.isAttacking = true;
        //    if (HeroModel.isHero)
        //    {
        //        transform.DOMove(pos, moveTime)
        //        .SetEase(Ease.Linear)
        //        .OnStart(() => { HeroModel.HeroSpin.State = AnimationState.Idle; HeroModel.HeroSpin.Spin(); })
        //    .SetDelay(0f)
        //    .OnComplete(() =>
        //    {
        //        HeroModel.HeroSpin.State = AnimationState.Skill; HeroModel.HeroSpin.Spin();
        //    });
        //    }
        //    else
        //    {
        //        transform.DOMove(pos, moveTime)
        //        .SetEase(Ease.Linear)
        //        .OnStart(() => { HeroModel.HeroSpin.State = AnimationState.Idle; HeroModel.HeroSpin.Spin(); })
        //    .SetDelay(0f)
        //    .OnComplete(() =>
        //    {
        //        HeroModel.HeroSpin.State = AnimationState.Skill; HeroModel.HeroSpin.Spin();
        //    });
        //    }
        //}
        //private void HandleAnimationEvent(TrackEntry trackEntry, Spine.Event e)
        //{
        //    if (e.Data.Name.Equals("end"))
        //    {
        //        StartSpineAnimation("stand", true);
        //        PlayerMoveBack();

        //    }else if(e.Data.Name.Equals("attack"))
        //    {
        //        HeroModel.DoAttack();

        //    }
        //}
        //public void StartSpineAnimation(string animationName, bool loop = false)
        //{
        //    if (HeroModel.SpineAnimation == null) return;
        //    HeroModel.SpineAnimation.AnimationState.SetAnimation(0, animationName, loop).TimeScale = 1;
        //}

        //public void PlayerMoveBack()
        //{
        //    transform.DOMove(HeroModel.OriginPos.position, moveTime)
        //    .SetEase(Ease.Linear)
        //    .OnStart(() => { HeroModel.HeroSpin.State = AnimationState.Idle; HeroModel.HeroSpin.Spin(); })
        //     .SetDelay(0.3f)
        //    .OnComplete(() =>
        // {
        //     HeroModel.isAttacking = false;
        //     HeroModel.HeroSpin.State = AnimationState.Idle; HeroModel.HeroSpin.Spin();
        //     //BattleController.instance.SpawnBattle();
        //     HeroModel.indexSkill++;

        // }); this.DoActionDone?.Invoke();
        //}
        public void PlayerMove()
        {
            //this.HeroModel.isDone = true;
            int speed;
            Vector3 target;
            if (!isBack)
            {
                target = BattleController.instance.TargetPosition(this.HeroModel);
                if (target == Vector3.zero) return;
            }
            else
            {
                target = HeroModel.OriginPos.position;
            }
            if (HeroModel.typeHero == -1)
            {
                speed = 1;
                //Debug.Log("speedCampain" + speed);
            }
            else { speed = BattleController.instance.SpeedGame; }
            //Debug.Log("speed" + BattleController.instance.SpeedGame);
            transform.DOMove(target, (moveTime / speed))
            .SetEase(Ease.Linear)
            .OnStart(() => { })
            .SetDelay(0f)
             .OnComplete(() =>
             {
                 //BattleController.instance.TargetPos = HeroModel.OriginPos.position;
                 //if (Vector3.Distance(transform.position, HeroModel.OriginPos.position) < 0.1f)
                 //{
                 //    this.HeroModel.isDone = false;
                 //    HeroModel.HeroSpin.State = AnimationState.Idle;
                 //    HeroModel.HeroSpin.Spin();
                 //}
                 if (!isBack)
                 {
                     isBack = true;
                 }
                 else
                 {
                     isBack = false;
                     //this.HeroModel.isDone = false;
                     if (HeroModel.HeroSpin.State == AnimationState.Attack)
                     {
                         this.HeroModel.isDoneAttack = false;
                     }else if (HeroModel.HeroSpin.State == AnimationState.Skill)
                     {
                         this.HeroModel.isDoneSkill = false;
                     }
                     HeroModel.HeroSpin.State = AnimationState.Idle;
                     HeroModel.HeroSpin.Spin();
                 }

             });

        }
    }

}


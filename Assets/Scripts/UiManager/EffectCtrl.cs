using GuanYu.Hero;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;

namespace GuanYu
{
    public class EffectCtrl : MonoBehaviour
    {

        public SkeletonGraphic SpineAnimationSkin;
        public SkeletonGraphic SpineAnimationEffect;
        //public ArrowMove ArrowPrefab;
        public Arrow ArrowPrefab;
        public GameObject EffectPrefab;
        public Transform Holder;
        public Transform HolderSkill;
        //public Transform SkillPos;
        public Transform origiPosSkillEffect;
        //public float timeEffect;
        public SkeletonDataAsset skeletonGraphicUpdate;


        public void ExfectSkill(HeroModel heromodel, Transform spwanPos)
        {
            int speed;
            ////if (SpineAnimationEffect == null) return;
            ////SpineAnimationEffect.AnimationState.SetAnimation(0, "animation", false);
            ////SpineAnimationEffect.transform.position = Target.position;
            //GameObject effect  = Instantiate(EffectPrefab, spwanPos);
            //effect.transform.SetParent(holder.transform, true);
            if (heromodel.typeHero == -1)
            {
                speed = 1;
            }
            else
            {
                speed = BattleController.instance.SpeedGame;
            }
            Debug.Log("Speed" + speed);
            SpineAnimationEffect.AnimationState.SetAnimation(0, "animation", false).TimeScale = speed;
            SpineAnimationEffect.transform.position = spwanPos.position;
            SpineAnimationEffect.transform.SetParent(spwanPos, true);
            float delay = TimeSpineAnimation("animation", false);
            //Debug.Log("timeDelay" + delay);
            StartCoroutine(ResetPositionAfterDelay(delay));
        }

        public float TimeSpineAnimation(string animationName, bool loop = false)
        {
            if (SpineAnimationEffect == null) return 0;
            var trackEntry = SpineAnimationEffect.AnimationState.SetAnimation(0, animationName, loop);
            trackEntry.TimeScale = 1;
            return trackEntry.AnimationEnd;
        }
        private IEnumerator ResetPositionAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SpineAnimationEffect.transform.SetParent(origiPosSkillEffect, false);
        }
        //public void OffEffect()
        //{
        //    SpineAnimationEffect.gameObject.SetActive(false);
        //}
        //public void OnEffect()
        //{
        //    SpineAnimationEffect.gameObject.SetActive(true);
        //}



        //public void SpwanBrow(HeroModel target, Action move = null)
        //{
        //    //if (target == null) return;
        //    if (target.CurHP <= 0) return;
        //    GameObject brow = Instantiate(BowPrefab, Holder.position,Quaternion.identity);
        //    brow.transform.SetParent(Holder);
        //    brow.transform.localScale = Vector3.one;
        //    //brow.GetComponent<BrowMove>().Move(target, move);
        //    brow.GetComponent<BrowMove>().Move(target, move);

        //}
        public void SpwanArrow(HeroSpin heroSpin, HeroModel target, Action takeDamage = null)
        {
            //if (target.CurHP <=0) return;
            //ArrowMove arrow = Instantiate(ArrowPrefab, Holder.position, Quaternion.identity);
            Arrow arrow = Instantiate(ArrowPrefab, Holder.position, Quaternion.identity);
            arrow.pos2 = target.takeDmgPos;
            if (heroSpin.heroModel.typeHero == -1)
            {
                arrow.speedGame = 1;
            }
            else { arrow.speedGame = BattleController.instance.SpeedGame; }
            if (heroSpin.State == Hero.AnimationState.Attack)
            {
                arrow.transform.SetParent(this.Holder, false);
                //arrow.StartPoint = this.Holder;
                arrow.pos1 = this.Holder;
            }
            else if ((heroSpin.State == Hero.AnimationState.Skill))
            {
                arrow.transform.SetParent(this.HolderSkill, false);
                //arrow.StartPoint = this.HolderSkill;
                arrow.pos1 = this.HolderSkill;
            }
            arrow.MoveToPosition(arrow.pos1.position, arrow.pos2.position, 20f, takeDamage);
            //arrow.EndPoint = target.takeDmgPos;
            //arrow.takeDamage = takeDamage;
        }
    }


}

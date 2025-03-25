using GuanYu.UI;
using GuanYu.User;
using Spine;
using System.Collections;
using UnityEngine;

namespace GuanYu.Hero
{
    public enum AnimationState
    {
        Into,
        Idle,
        Attack,
        Hurt,
        Skill,
        Dead,
    }
    public class HeroSpin : MonoBehaviour
    {
        public HeroModel heroModel;
        public AnimationState State = AnimationState.Idle;

        //public float TimeAtk;
        //public string eventSpine;
        private int attackTriggerCount = 0;
        public void Init()
        {
            if (heroModel.SpineAnimation != null)
            {
                heroModel.SpineAnimation.AnimationState.Event += HandleAnimationEvent;
            }
        }
        //private void _HandleAnimationEvent(TrackEntry trackEntry, Spine.Event e)
        //{
        //if (State == AnimationState.Attack && e.Data.Name.Equals("end"))
        //{
        //    if (heroModel.HeroDataCf.Type == HeroType.Melee)
        //    {
        //        heroModel.isAttacking = false;
        //        State = AnimationState.Idle;
        //        Spin();
        //    }
        //    else
        //    {

        //        State = AnimationState.Idle;
        //        Spin();
        //        heroModel.isAttacking = false;
        //    }
        //}
        //else if (State == AnimationState.Dead && e.Data.Name.Equals("end"))
        //{
        //    heroModel.Dead();
        //}
        //else if (e.Data.Name.Equals("up"))
        //{
        //    heroModel.PlayerMovement.PlayerMove();
        //}
        //else if (e.Data.Name.Equals("fast"))
        //{
        //    heroModel.PlayerMovement.PlayerMove();
        //}
        //else if (State == AnimationState.Attack && e.Data.Name.Equals("attack"))
        //{
        //    if (heroModel.HeroDataCf.Type == HeroType.Bow)
        //    {
        //        heroModel.No.GetComponent<EffectCtrl>().SpwanBrow(heroModel.Target, () => { heroModel.Target.TakeDmg(heroModel.HeroDataCf.Atk); });

        //    }
        //    else if (heroModel.HeroDataCf.Type == HeroType.Row)
        //    {
        //        heroModel.No.GetComponent<EffectCtrl>().SpwanArrow(heroModel.Target, () => { heroModel.Target.TakeDmg(heroModel.HeroDataCf.Atk); });
        //    }
        //    heroModel.DoAttack();
        //}
        //else if (State == AnimationState.Skill && e.Data.Name.Equals("attack"))
        //{
        //    if (heroModel.HeroDataCf.Type == HeroType.Bow)
        //    {

        //        for (int i = 0; i < heroModel.TargetSkills.Count; i++)
        //        {
        //            EffectSpawnBow(i);
        //        }
        //    }
        //    heroModel.DoAttack();
        //}
        //else if (State == AnimationState.Into && e.Data.Name.Equals("end"))
        //{
        //    State = AnimationState.Idle;
        //    Spin();
        //}
        //else if (State == AnimationState.Skill && e.Data.Name.Equals("end"))
        //{
        //    heroModel.isAttacking = false;
        //    if (heroModel.HeroDataCf.Type == HeroType.Melee)
        //    {
        //        State = AnimationState.Idle;
        //        Spin();
        //    }
        //    else
        //    {
        //        State = AnimationState.Idle;
        //        Spin();

        //    }

        //}
        //else if (State == AnimationState.Hurt && e.Data.Name.Equals("end"))
        //{
        //    State = AnimationState.Idle;
        //    Spin();
        //}
        //}
        private void HandleAnimationEvent(TrackEntry trackEntry, Spine.Event e)
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(heroModel);
            string evenName = e.Data.Name;
            switch (State)
            {
                case AnimationState.Attack:
                    SpineAttack(evenName);
                    break;
                case AnimationState.Skill:
                    SpineSkill(evenName);
                    break;
                case AnimationState.Dead:

                    if (evenName == "end")
                    {
                        this.heroModel.isDoneDead = false;
                        Dead();
                        //heroModel.isDone = false;

                    }
                    break;
                case AnimationState.Into:
                    if (evenName == "end")
                    {
                        State = AnimationState.Idle;
                        Spin();
                    }
                    break;
                case AnimationState.Hurt:
                    if (evenName == "end")
                    {
                        //heroModel.isDone = false;
                        State = AnimationState.Idle;
                        Spin();
                    }
                    break;
            }
        }
        private void SpineAttack(string spine)
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(heroModel);
            switch (spine)
            {
                case "up":
                    if (heroDataCf.Type == HeroType.Melee)
                    {

                        heroModel.PlayerMovement.PlayerMove();
                    }
                    break;
                case "fast":
                    if (heroDataCf.Type == HeroType.Melee)
                    {
                        heroModel.PlayerMovement.PlayerMove();
                    }
                    break;
                case "attack":
                    if (heroDataCf.Type == HeroType.Melee /*|| heroDataCf.Type == HeroType.Idle*/)
                    {
                        heroModel.DoAttack();
                    }
                    else
                    {
                        this.heroModel.isDoneArrow = true;
                        heroModel.No.GetComponent<EffectCtrl>().SpwanArrow(this, heroModel.heroModelTargets[0], ()
                            =>
                        {
                            heroModel.DoAttack();
                            this.heroModel.isDoneArrow = false;
                        });
                    }
                    break;
                case "music":
                    HeroAudio(this.heroModel.dataHero.attackAudio);
                    HeroAudio(this.heroModel.dataHero.attackhitAudio);
                    break;
                case "end":
                    if (this.heroModel.isDoneAttack)
                    { this.heroModel.isDoneAttack = false; }
                    State = AnimationState.Idle;
                    Spin();
                    break;
            }
        }

        private void SpineSkill(string spine)
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(heroModel);
            switch (spine)
            {
                case "up":
                    if (heroDataCf.Type == HeroType.Melee)
                    {
                        heroModel.PlayerMovement.PlayerMove();
                    }
                    break;
                case "fast":
                    if (heroDataCf.Type == HeroType.Melee)
                    {
                        heroModel.PlayerMovement.PlayerMove();
                    }
                    break;
                case "attack":
                    if (heroDataCf.Type == HeroType.Melee /*|| heroDataCf.Type == HeroType.Idle*/)
                    {
                        heroModel.DoAttack();
                    }
                    else
                    {
                        attackTriggerCount++;
                        this.heroModel.isDoneArrow = true;
                        for (int i = 0; i < heroModel.heroModelTargets.Count; i++)
                        {
                            heroModel.EffectSpawnArrow(heroModel, i, attackTriggerCount >= heroModel.No.GetComponent<HeroAvatar>().datahero.countSkill ? CompleteEffect : null);
                        }
                    }
                    break;
                case "music":
                    HeroAudio(this.heroModel.dataHero.skillAudio);
                    HeroAudio(this.heroModel.dataHero.skillhitAudio);
                    break;
                case "end":
                    if (this.heroModel.isDoneSkill) { this.heroModel.isDoneSkill = false; }
                    State = AnimationState.Idle;
                    Spin();
                    break;
            }
            //if (spine == "up" || spine == "fast")
            //{
            //    heroModel.PlayerMovement.PlayerMove();
            //}
            //else if (spine == "attack")
            //{
            //    if (heroDataCf.Type == HeroType.Melee || heroDataCf.Type == HeroType.Idle)
            //    {
            //        heroModel.DoAttack();
            //    }
            //    else
            //    {
            //        attackTriggerCount++;
            //        for (int i = 0; i < heroModel.heroModelTargets.Count; i++)
            //        {
            //            heroModel.EffectSpawnArrow(heroModel, i, attackTriggerCount >= heroModel.No.GetComponent<HeroAvatar>().datahero.countSkill ? CompleteEffect : null);
            //        }
            //    }
            //}
            //else if (spine == "end")
            //{
            //    if (heroDataCf.Type == HeroType.Melee
            //   && heroDataCf.Skill == Skill.IdleSkill)
            //    {
            //        State = AnimationState.Idle;
            //        Spin();
            //    }
            //    else if (heroDataCf.Type == HeroType.Row || heroDataCf.Type == HeroType.Bow)
            //    {
            //        State = AnimationState.Idle;
            //        Spin();
            //    }
            //    if (heroDataCf.Type == HeroType.Idle && heroDataCf.Skill == Skill.IdleSkill)
            //    {
            //        heroModel.isDone = false;
            //        State = AnimationState.Idle;
            //        Spin();
            //    }
            //}
            //else if (spine == "music")
            //{
            //    HeroAudio(this.heroModel.dataHero.skillAudio);
            //    HeroAudio(this.heroModel.dataHero.skillhitAudio);
            //}
        }
        private void CompleteEffect()
        {
            this.heroModel.isDoneArrow = false;
            attackTriggerCount = 0;
        }
        public void Spin()
        {
            switch (State)
            {
                case AnimationState.Idle:
                    SpineAnimation("stand", true);
                    //heroModel.isDone = false;
                    break;
                case AnimationState.Attack:
                    SpineAnimation("attack", false);
                    break;
                case AnimationState.Skill:
                    SpineAnimationSkill();
                    break;
                case AnimationState.Dead:
                    //heroModel.isDone = true;
                    this.heroModel.isDoneDead = true;
                    SpineAnimation("dead", false);
                    break;
                case AnimationState.Hurt:
                    //heroModel.isDone = true;
                    SpineAnimation("hurt", false);
                    break;
                case AnimationState.Into:
                    SpineAnimation("into", false);
                    break;
            }
        }

        public void SpineAnimationSkill()
        {
            ExfectSkill();
            SpineAnimation("skill01", false);
        }
        public void ExfectSkill()
        {
            this.heroModel.isDoneEffect = true;
            //heroModel.isDone = true;
            Transform spwanPos;
            int speed;
            if (heroModel.typeHero == -1)
            {
                speed = 1;
                if (heroModel.isHero)
                {
                    spwanPos = BattleController.instance.battleCampain.skillHeroPos;
                }
                else
                {
                    spwanPos = BattleController.instance.battleCampain.skillEnemyPos;
                }

            }
            else if (heroModel.typeHero == 0)
            {
                speed = BattleController.instance.SpeedGame;
                if (heroModel.isHero)
                {
                    spwanPos = BattleController.instance.battleArena.skillHeroPos;
                }
                else
                {
                    spwanPos = BattleController.instance.battleArena.skillEnemyPos;
                }
            }
            else
            {
                speed = 1;
                if (heroModel.isHero)
                {
                    spwanPos = BosController.instance.heroPosSkill;
                }
                else
                {
                    spwanPos = BosController.instance.bossPosSkill;
                }
            }
            Debug.Log("Speed" + speed);
            heroModel.SpineFxSkill.transform.SetParent(spwanPos, false);
            heroModel.SpineFxSkill.AnimationState.SetAnimation(0, "animation", false).TimeScale = speed;
            float delay = TimeSpineAnimation(speed, "animation", false);
            StartCoroutine(ResetPositionAfterDelay(delay));
        }

        public float TimeSpineAnimation(int speedGame, string animationName, bool loop = false)
        {
            if (heroModel.SpineFxSkill == null) return 0;
            var trackEntry = heroModel.SpineFxSkill.AnimationState.SetAnimation(0, animationName, loop);
            trackEntry.TimeScale = speedGame;
            return trackEntry.AnimationEnd / speedGame;
        }
        private IEnumerator ResetPositionAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            //heroModel.isDone = false;
            this.heroModel.isDoneEffect = false;
            heroModel.SpineFxSkill.transform.SetParent(heroModel.No.GetComponent<EffectCtrl>().origiPosSkillEffect, false);
        }
        public void SpineAnimation(string animationName, bool loop = false)
        {
            int speed;
            if (heroModel.typeHero == 0)
            {
                speed = BattleController.instance.SpeedGame;
            }
            else
            {
                speed = 1;
            }
            if (heroModel.SpineAnimation == null) return;
            heroModel.SpineAnimation.AnimationState.SetAnimation(0, animationName, loop).TimeScale = speed;

        }
        public void Dead()
        {

            if (this.heroModel.typeHero == -1)
            {
                if (!this.heroModel.isHero)
                {
                    //GoldCtrl.instance.SpawnGold(transform.position);

                    BattleController.instance.battleCampain.SpawnGold(transform.position);
                }
                //BattleCtrl.instance.SpawnHeroCampain(this.heroModel);
            }
            else
            {
                //BattleController.instance.CheckWinLoseArena(this.heroModel);
                UIManager.instance.arenaUI.CheckAvatarOnOff();
            }
            this.heroModel.gameObject.SetActive(false);
        }
        public void HeroAudio(AudioClip audioClip)
        {
            if (this.heroModel.typeHero == -1)
            {
                if (MenuController.Instance.Menu == Menu.Camp)
                {
                    SoundManager.instance.SoundCampain1(audioClip);
                }
                else return;
            }
            else if (this.heroModel.typeHero == 0)
            {

                if (MenuController.Instance.Menu == Menu.Arena)
                {
                    SoundManager.instance.PlaySingle(audioClip);
                }
                else return;
            }
            else
            {
                if (MenuController.Instance.Menu == Menu.Home)
                {
                    SoundManager.instance.PlaySingleHome(audioClip);
                }
                else return;
            }
        }
    }
}


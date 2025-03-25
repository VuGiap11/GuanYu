using Spine.Unity;
using System;
using UnityEngine;

namespace GuanYu.Hero
{
    using GuanYu.User;
    using System.Collections.Generic;
    using UI;
    using Random = UnityEngine.Random;

    public enum HeroModelType
    {
        HeroArena,
        HeroCampain,
    }

    public class HeroModel : MonoBehaviour
    {
        public PlayerMovement PlayerMovement;
        public HeroSpin HeroSpin;
        [SerializeField] private Transform Skin;
        public SkeletonGraphic SpineAnimation;
        public SkeletonGraphic SpineFxSkill;
        //public HeroDataCf HeroDataCf;
        public HpBar HpBar;
        public HpBar SkillBar;
        public Transform OriginPos;
        public int CurHP;
        public int CurindexSkill;
        public bool isHero;
        public int index;
        public bool isDead;
        public bool isDone;
        public bool isDoneAttack;//
        public bool isDoneSkill;//
        public bool isDoneDead;//
        public bool isDoneEffect;
        public bool isDoneArrow;//

        public Transform takeDmgPos;
        public GameObject No;
        [SerializeField] private Transform holderEffect;
        [SerializeField] private Transform holderPoint;
        public int typeHero; // nếu là -1 thì là campain, nếu là 0 là loại heroarena, nếu là 1 thì là heroBoss;
        public List<HeroModel> heroModelTargets = new List<HeroModel>();

        public DataHero dataHero;
        //public HeroModelType heroModelType;
        //public object Rectransform { get; internal set; }

        //public void Init(HeroDataCf heroDataCf, Transform targetPos, Transform originPos, HeroModel target, bool isHero = true)
        //{
        //    this.Target = target;
        //    this.HeroDataCf = heroDataCf;
        //    this.GenSkin(isHero);
        //    this.LoadSke();
        //    //this.TargetPos = targetPos;
        //    this.OriginPos = originPos;
        //    this.HeroSpin.Init();
        //    //this.PlayerMovement.Init();
        //    transform.position = originPos.position;
        //    this.CurHP = this.HeroDataCf.Hp;
        //}

        //public void InitBtl(HeroDataCf heroDataCf, Transform originPos, bool isHero = true)
        //{
        //    this.HeroDataCf = heroDataCf;
        //    if (isHero)
        //    {
        //        this.HeroDataCf = UserManager.Instance.SetHeroDataCf(heroDataCf.Index);
        //    }
        //    else
        //    {
        //        if (BattleCtrl.instance.campainType == CampainType.Campain)
        //        {
        //            this.HeroDataCf = UserManager.Instance.SetEnemyDataCf(heroDataCf.Index, BattleCtrl.instance.lever);
        //        }
        //        else
        //        {
        //            this.HeroDataCf = UserManager.Instance.SetEnemyDataCf(heroDataCf.Index, BattleCtrl.instance.levelArena);
        //        }
        //    }
        //    this.GenSkin(isHero);
        //    //this.LoadSke();
        //    this.HeroSpin.Init();
        //    this.OriginPos = originPos;
        //    this.CurHP = this.HeroDataCf.Hp;
        //    this.CurindexSkill = 0;
        //    this.SkillBar.UpdateHpBar(this.HeroDataCf.IndexSkill, this.CurindexSkill);
        //    this.HpBar.UpdateHpBar(this.HeroDataCf.Hp, this.CurHP);

        //    this.isHero = isHero;
        //    if (isHero)
        //    {
        //        transform.localScale = new Vector3(1f, 1f, 1f);
        //    }
        //    else
        //    {
        //        transform.localScale = new Vector3(-1f, 1f, 1f);
        //    }
        //    HeroSpin.State = AnimationState.Into;
        //    HeroSpin.Spin();
        //}

        public void InitHeroBattle(int index, Transform originPos)
        {
            this.isDone = false;
            this.index = index;
            this.isDoneAttack = false;
            this.isDoneSkill = false;
            this.isDoneDead = false;
            this.isDoneEffect = false;
            this.isDoneArrow = false;
            //Debug.Log(this.index);
            HeroDataCf heroDataCf = UserManager.Instance.SetHeroDataCf(this.index);
            GenSkin(true);
            HeroSpin.Init();
            this.OriginPos = originPos;
            this.CurHP = heroDataCf.Hp;
            this.CurindexSkill = 0;
            SetSkillBar(heroDataCf, CurindexSkill);
            //this.SkillBar.UpdateHpBar(heroDataCf.IndexSkill, this.CurindexSkill);
            //this.HpBar.UpdateHpBar(heroDataCf.Hp, this.CurHP);
            SetHpBar(heroDataCf, this.CurHP);
            transform.localScale = new Vector3(1f, 1f, 1f);
            HeroSpin.State = AnimationState.Into;
            HeroSpin.Spin();
        }
        public void InitEnemyBattle(int index, Transform originPos)
        {
            this.isDone = false;
            this.index = index;
            this.isDoneAttack = false;
            this.isDoneSkill = false;
            this.isDoneDead = false;
            this.isDoneEffect = false;
            this.isDoneArrow = false;
            HeroDataCf heroDataCf = UserManager.Instance.SetEnemyDataCf(this);
            GenSkin(false);
            this.HeroSpin.Init();
            this.OriginPos = originPos;
            this.CurHP = heroDataCf.Hp;
            //Debug.Log("heroDataCf.Hp" + heroDataCf.Hp);
            this.CurindexSkill = 0;
            SetSkillBar(heroDataCf, CurindexSkill);
            //this.SkillBar.UpdateHpBar(heroDataCf.IndexSkill, this.CurindexSkill);  
            //this.HpBar.UpdateHpBar(heroDataCf.Hp, this.CurHP);
            SetHpBar(heroDataCf, this.CurHP);
            transform.localScale = new Vector3(1f, 1f, 1f);
            HeroSpin.State = AnimationState.Into;
            HeroSpin.Spin();
        }
        // thêm eemy
        //public void InitBtlEnemy(EnemyDataTrainingCf enemyDataTrainingCf, Transform originPos, bool isHero = true)
        //{
        //    this.EnemyDataTrainingCf = enemyDataTrainingCf;
        //    this.GenSkin(!isHero);
        //    this.LoadSke();
        //    this.HeroSpin.Init();
        //    this.OriginPos = originPos;
        //    this.CurHP = this.EnemyDataTrainingCf.Hp;
        //    this.isHero = (!isHero);
        //    transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
        //    HeroSpin.State = AnimationState.Into;
        //    HeroSpin.Spin();
        //}

        //public void GenSkin(bool isHero)
        //{
        //    MyFunction.ClearChild(this.Skin);
        //    GameObject a;

        //    if (isHero) a = HeroAssets.Instance.GetHeroUIPrefabByIndex(HeroDataCf.Index);
        //    else a = HeroAssets.Instance.GetEnemyUIPrefabByIndex(HeroDataCf.Index);
        //    this.No = a;
        //    a.transform.SetParent(this.Skin, false);
        //    SpineAnimation = a.GetComponent<EffectCtrl>().SpineAnimationSkin;
        //    a.transform.localScale = Vector3.one;
        //}
        public void GenSkin(bool isHero)
        {
            //MyFunction.ClearChild(this.Skin);
            //GameObject a;
            //if (isHero) a = HeroAssets.Instance.GetHeroUIPrefabByIndex(this.index);
            //else a = HeroAssets.Instance.GetEnemyUIPrefabByIndex(this.index);
            //this.No = a;
            //a.transform.SetParent(this.Skin, false);
            //SpineAnimation = a.GetComponent<EffectCtrl>().SpineAnimationSkin;
            //SpineFxSkill = a.GetComponent<EffectCtrl>().SpineAnimationEffect;
            //a.transform.localScale = Vector3.one;
            MyFunction.ClearChild(this.Skin);
            if (isHero) this.No = HeroAssets.Instance.GetHeroUIPrefabByIndex(this.index);
            else this.No = HeroAssets.Instance.GetEnemyUIPrefabByIndex(this.index);
            this.No.transform.SetParent(this.Skin, false);
            SpineAnimation = this.No.GetComponent<EffectCtrl>().SpineAnimationSkin;
            SpineFxSkill = this.No.GetComponent<EffectCtrl>().SpineAnimationEffect;
            this.No.transform.localScale = Vector3.one;
            this.dataHero = No.GetComponent<HeroAvatar>().datahero;

        }

        //public void DoAttack()
        //{
        //    if (HeroSpin.State == AnimationState.Attack)
        //    {
        //        for (int i = 0; i < BattleCtrl.instance.TargetSkills.Count; i++)
        //        {
        //            BattleCtrl.instance.TargetSkills[i].TakeDmg(this.HeroDataCf.Atk / (No.GetComponent<HeroAvatar>().datahero.countAtk));
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < BattleCtrl.instance.TargetSkills.Count; i++)
        //        {
        //            BattleCtrl.instance.TargetSkills[i].TakeDmg(this.HeroDataCf.Atk / (No.GetComponent<HeroAvatar>().datahero.countSkill));
        //        }
        //    }

        //}
        //public void DoAttack()
        //{
        //    HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
        //    if (HeroSpin.State == AnimationState.Attack)
        //    {
        //        for (int i = 0; i < BattleController.instance.TargetSkills.Count; i++)
        //        {
        //            BattleController.instance.TargetSkills[i].TakeDmg(heroDataCf.Atk / (No.GetComponent<HeroAvatar>().datahero.countAtk));
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < BattleController.instance.TargetSkills.Count; i++)
        //        {
        //            BattleController.instance.TargetSkills[i].TakeDmg(heroDataCf.Atk / (No.GetComponent<HeroAvatar>().datahero.countSkill));
        //        }
        //    }

        //}
        public void DoAttack()
        {
            if (this.heroModelTargets.Count <= 0) return;
            for (int i = 0; i < this.heroModelTargets.Count; i++)
            {
                this.heroModelTargets[i].TakeDmg(this);
            }

        }
        //public void TakeDmg(int dmg)
        //{
        //    HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
        //    if (this.CurHP <= 0) return;
        //    if (HeroSpin.State == AnimationState.Attack)
        //    {
        //        dmg = (int)(dmg * dmg / (dmg + heroDataCf.Def)) / No.GetComponent<HeroAvatar>().datahero.countAtk;

        //    }
        //    else if (HeroSpin.State == AnimationState.Skill)
        //    {
        //        dmg = (int)(dmg * dmg / (dmg + heroDataCf.Def)) / No.GetComponent<HeroAvatar>().datahero.countSkill;
        //        Debug.Log("dmg skill" + No.GetComponent<HeroAvatar>().datahero.countSkill);

        //    }
        //    this.CurHP -= dmg;
        //    HeroSpin.State = AnimationState.Hurt;
        //    if (this.CurHP <= 0)
        //    {
        //        if (!isDead)
        //        {
        //            isDead = true;
        //            this.CurHP = 0;
        //            HeroSpin.State = AnimationState.Dead;
        //            BattleController.instance.RemoveTurn(this);
        //        }
        //    }
        //    HeroSpin.Spin();
        //    this.HpBar.UpdateHpBar(heroDataCf.Hp, this.CurHP);
        //    BattleController.instance.UpdateHp();
        //    float random = Random.Range(-0.4f, 0.4f);
        //    Vector3 pointpos = new Vector3(this.transform.position.x + random, this.transform.position.y + 1f, this.transform.position.x);
        //    //UIManager.instance.battleUI.SpawnPoint(pointpos, dmg, this.holderPoint);
        //    BattleController.instance.SpawnPoint(pointpos, dmg, this.holderPoint);
        //}
        public void TakeDmg(HeroModel heromodel)
        {
            if (this.CurHP <= 0) return;
            int dmg = 0;
            HeroDataCf heroDataCfHero = UserManager.Instance.SetdataCf(heromodel);
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
            if (heromodel.HeroSpin.State == AnimationState.Attack)
            {
                dmg = (int)(heroDataCfHero.Atk * heroDataCfHero.Atk / (heroDataCfHero.Atk + heroDataCf.Def)) / heromodel.No.GetComponent<HeroAvatar>().datahero.countAtk;
            }
            else if (heromodel.HeroSpin.State == AnimationState.Skill)
            {
                dmg = (int)(heroDataCfHero.Atk * heroDataCfHero.Atk / (heroDataCfHero.Atk + heroDataCf.Def)) / heromodel.No.GetComponent<HeroAvatar>().datahero.countSkill;
            }
            this.CurHP -= dmg;
            this.HeroSpin.State = AnimationState.Hurt;
            if (this.CurHP <= 0)
            {
                Dead();
                if (!this.isHero)
                {
                    AchivementDataCtrl.Instance.DoneAchiveMent(AchivementType.DefeatMonster);
                }
            }
            //this.HpBar.UpdateHpBar(heroDataCf.Hp, this.CurHP);
            SetHpBar(heroDataCf, this.CurHP);
            this.HeroSpin.Spin();
            BattleController.instance.UpdateHp(this);
            float random = Random.Range(-0.4f, 0.4f);
            Vector3 pointpos = new Vector3(this.transform.position.x + random, this.transform.position.y + 1f, this.transform.position.x);
            BattleController.instance.SpawnPoint(heromodel, pointpos, dmg, this.holderPoint);
        }
        private void Dead()
        {
            if (!isDead)
            {
                isDead = true;
                this.CurHP = 0;
                HeroSpin.State = AnimationState.Dead;
                this.HeroSpin.Spin();
                BattleController.instance.RemoveTurn(this);
            }
        }

        public void TakeDamageArr(int dmg)
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
            if (this.CurHP <= 0) return;
            this.CurHP -= dmg;
            if (this.CurHP <= 0)
            {
                Dead();
                this.HeroSpin.Spin();
                if (!this.isHero)
                {
                    AchivementDataCtrl.Instance.DoneAchiveMent(AchivementType.DefeatMonster);
                }
            }
            SetHpBar(heroDataCf, this.CurHP);
            BattleController.instance.UpdateHp(this);
            float random = Random.Range(-0.4f, 0.4f);
            Vector3 pointpos = new Vector3(this.transform.position.x + random, this.transform.position.y + 1f, this.transform.position.x);
            BattleController.instance.SpawnPoint(this, pointpos, dmg, this.holderPoint);
        }
        //public void TakeDmg( int dmg)
        //{
        //    //List<HeroModel> listTarget = BattleController.instance.GetTarget(this);
        //    List<HeroModel> listTarget = BattleController.instance.GetTarget(this);
        //    for (int i = 0; i < listTarget.Count; i++)
        //    {
        //        if (listTarget[i].CurHP <= 0) continue;
        //        HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(listTarget[i]);
        //        if (HeroSpin.State == AnimationState.Attack)
        //        {
        //            dmg = (int)(dmg * dmg / (dmg + heroDataCf.Def)) / No.GetComponent<HeroAvatar>().datahero.countAtk;
        //        }
        //        else if(HeroSpin.State == AnimationState.Skill)
        //        {
        //            dmg = (int)(dmg * dmg / (dmg + heroDataCf.Def)) / No.GetComponent<HeroAvatar>().datahero.countSkill;
        //        }
        //        listTarget[i].CurHP -= dmg;
        //        listTarget[i].HeroSpin.State = AnimationState.Hurt;
        //        if (listTarget[i].CurHP <= 0)
        //        {
        //            if (!listTarget[i].isDead)
        //            {
        //                listTarget[i].isDead = true;
        //                listTarget[i].CurHP = 0;
        //                listTarget[i].HeroSpin.State = AnimationState.Dead;
        //                //BattleController.instance.RemoveTurn(listTarget[i]);
        //                if (listTarget[i].typeHero == -1)
        //                {
        //                    BattleController.instance.battleCampain.RemoveTurn(listTarget[i]);
        //                }else
        //                {
        //                    BattleController.instance.battleArena.RemoveTurn(listTarget[i]);
        //                }
        //            }
        //        }
        //        listTarget[i].HeroSpin.Spin();
        //        listTarget[i].HpBar.UpdateHpBar(heroDataCf.Hp, listTarget[i].CurHP);
        //        BattleController.instance.UpdateHp(this);
        //        //BattleController.instance.UpdateHp();
        //        float random = Random.Range(-0.4f, 0.4f);
        //        Vector3 pointpos = new Vector3(listTarget[i].transform.position.x + random, listTarget[i].transform.position.y + 1f, listTarget[i].transform.position.x);
        //        BattleController.instance.SpawnPoint(pointpos, dmg, listTarget[i].holderPoint);
        //    }
        //}
        public void TakeDamageOneHit(HeroModel heromodel)
        {
            if (this.CurHP <= 0) return;
            int dmg = 0;
            HeroDataCf heroDataCfHero = UserManager.Instance.SetdataCf(heromodel);
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
            if (heromodel.HeroSpin.State == AnimationState.Attack)
            {
                dmg = (int)(heroDataCfHero.Atk * heroDataCfHero.Atk / (heroDataCfHero.Atk + heroDataCf.Def)) / heromodel.No.GetComponent<HeroAvatar>().datahero.countAtk;
            }
            else if (heromodel.HeroSpin.State == AnimationState.Skill)
            {
                dmg = (int)(heroDataCfHero.Atk * heroDataCfHero.Atk / (heroDataCfHero.Atk + heroDataCf.Def)) / heromodel.No.GetComponent<HeroAvatar>().datahero.countSkill;
            }
            this.CurHP -= dmg;
            if (this.CurHP <= 0)
            {
                Dead();
            }
            this.HpBar.UpdateHpBar(heroDataCf.Hp, this.CurHP);
            BattleController.instance.UpdateHp(this);
        }

        public void DoAttackOneHit()
        {
            if (this.heroModelTargets.Count <= 0) return;
            for (int i = 0; i < this.heroModelTargets.Count; i++)
            {
                this.heroModelTargets[i].TakeDamageOneHit(this);
            }
        }

        //public void TakeDmg(int dmg)
        //{
        //    HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
        //    if (this.CurHP <= 0) return;
        //    //dmg = (int)(dmg * dmg / (dmg + heroDataCf.Def));
        //    //this.CurHP -= dmg;
        //    if (this.HeroSpin.State == AnimationState.Attack)
        //    {
        //        dmg = (int)(dmg * dmg / (dmg + heroDataCf.Def))/No.GetComponent<HeroAvatar>().datahero.countAtk;

        //    }else if(this.HeroSpin.State == AnimationState.Skill)
        //    {
        //        dmg = (int)(dmg * dmg / (dmg + heroDataCf.Def)) / No.GetComponent<HeroAvatar>().datahero.countSkill;
        //        Debug.Log("dmg skill" + No.GetComponent<HeroAvatar>().datahero.countSkill);

        //    }
        //    this.CurHP -= dmg;
        //    HeroSpin.State = AnimationState.Hurt;
        //    //HeroSpin.Spin();
        //    //this.HpBar.UpdateHpBar(heroDataCf.Hp, this.CurHP);
        //    if (this.CurHP <= 0)
        //    {
        //        if (!isDead)
        //        {
        //            isDead = true;
        //            this.CurHP = 0;
        //            //this.HpBar.UpdateHpBar(heroDataCf.Hp, this.CurHP);

        //            HeroSpin.State = AnimationState.Dead;
        //            //HeroSpin.Spin();
        //            BattleController.instance.RemoveTurn(this);
        //        }
        //    }
        //    HeroSpin.Spin();
        //    this.HpBar.UpdateHpBar(heroDataCf.Hp, this.CurHP);
        //    BattleController.instance.UpdateHp();
        //    float random = Random.Range(-0.4f, 0.4f);
        //    Vector3 pointpos = new Vector3(this.transform.position.x + random, this.transform.position.y + 1f, this.transform.position.x);
        //    //UIManager.instance.battleUI.SpawnPoint(pointpos, dmg, this.holderPoint);
        //    BattleController.instance.SpawnPoint(pointpos, dmg, this.holderPoint);
        //}

        //public void ResetHero()
        //{
        //    //if (!this.gameObject.activeSelf)
        //    //{
        //    //    this.gameObject.SetActive(true);
        //    //}
        //    HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
        //    this.CurHP = heroDataCf.Hp;
        //    CurindexSkill = 0;
        //}

        public void ResetHero()
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
            this.CurHP = heroDataCf.Hp;
            this.CurindexSkill = 0;
            this.isDead = false;
        }
        //public void TakeDamageOneHit(int dmg)
        //{
        //    //HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
        //    //if (this.CurHP <= 0) return;
        //    //if (HeroSpin.State == AnimationState.Attack)
        //    //{
        //    //    dmg = (int)(dmg * dmg / (dmg + heroDataCf.Def)) / heromodel.No.GetComponent<HeroAvatar>().datahero.countAtk;
        //    //}
        //    //else if (heromodel.HeroSpin.State == AnimationState.Skill)
        //    //{
        //    //    dmg = (int)(dmg * dmg / (dmg + heroDataCf.Def)) / heromodel.No.GetComponent<HeroAvatar>().datahero.countSkill;
        //    //    Debug.Log("dmg skill" + heromodel.No.GetComponent<HeroAvatar>().datahero.countSkill);
        //    //}
        //    //heromodel.CurHP -= dmg;
        //    //if (heromodel.CurHP <= 0)
        //    //{
        //    //    if (!heromodel.isDead)
        //    //    {
        //    //        heromodel.isDead = true;
        //    //        heromodel.CurHP = 0;
        //    //        heromodel.gameObject.SetActive(false);
        //    //        BattleController.instance.RemoveTurn(heromodel);
        //    //    }
        //    //}
        //    //heromodel.HpBar.UpdateHpBar(heroDataCf.Hp, heromodel.CurHP);
        //    //BattleController.instance.UpdateHp();
        //}

        public void Attack()
        {
            isDone = true;
            if (typeHero == 0)
            {
                UIManager.instance.arenaUI.SetTurnAvar();
            }
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
            if (CurindexSkill < heroDataCf.IndexSkill)
            {
                this.isDoneAttack = true;
                HeroSpin.State = AnimationState.Attack;
                //Debug.Log("state" +HeroSpin.State);
                this.heroModelTargets = BattleController.instance.GetTarget(this);
                if (this.heroModelTargets.Count <= 0) { isDone = false; isDoneAttack = false; return; }
                HeroSpin.Spin();
                //CurindexSkill++;
                //SetSkillBar( heroDataCf, CurindexSkill);
            }
            else
            {
                isDoneSkill = true;
                HeroSpin.State = AnimationState.Skill; 
                this.heroModelTargets = BattleController.instance.GetTarget(this);
                if (this.heroModelTargets.Count <= 0) { isDone = false; isDoneSkill = false; return; }
                HeroSpin.Spin();
                //CurindexSkill = 0;
                //SetSkillBar(heroDataCf, CurindexSkill);
            }
        }
        public void AttackOneHit()
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
            if (CurindexSkill < heroDataCf.IndexSkill)
            {
                HeroSpin.State = AnimationState.Attack;
                this.heroModelTargets = BattleController.instance.GetTarget(this);
                if (this.heroModelTargets.Count <= 0) return;
                CurindexSkill++;
            }
            else if (CurindexSkill >= heroDataCf.IndexSkill)
            {
                HeroSpin.State = AnimationState.Skill;
                this.heroModelTargets = BattleController.instance.GetTarget(this);
                if (this.heroModelTargets.Count <= 0) return;
                CurindexSkill = 0;
            }

        }
        public void EffectSpawn()
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
            if (heroDataCf.Type == HeroType.Melee)
            {
                if (typeHero == -1)
                {
                    if (isHero)
                    {
                        No.GetComponent<EffectCtrl>().ExfectSkill(this, BattleController.instance.battleCampain.skillHeroPos);
                    }
                    else
                    {
                        No.GetComponent<EffectCtrl>().ExfectSkill(this, BattleController.instance.battleCampain.skillEnemyPos);
                    }
                }
                else
                {
                    if (isHero)
                    {
                        No.GetComponent<EffectCtrl>().ExfectSkill(this, BattleController.instance.battleArena.skillHeroPos);
                    }
                    else
                    {
                        No.GetComponent<EffectCtrl>().ExfectSkill(this, BattleController.instance.battleArena.skillEnemyPos);
                    }

                }
            }

        }
        public void EffectSpawnArrow(HeroModel heroModel, int index, Action onComplete = null)
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(heroModel);
            //No.GetComponent<EffectCtrl>().SpwanArrow(heroModel.HeroSpin, BattleController.instance.TargetSkills[index], () => { BattleController.instance.TargetSkills[index].TakeDmg(heroModel); onComplete?.Invoke(); });
            No.GetComponent<EffectCtrl>().SpwanArrow(heroModel.HeroSpin, this.heroModelTargets[index], () => { this.heroModelTargets[index].TakeDmg(heroModel); onComplete?.Invoke(); });
        }


        public bool Setdone()
        {
            //if (isDoneAttack) return true;
            //if (isDoneSkill) return true;
            //if (isDoneDead) return true;
            //if (isDoneEffect) return true;
            //if (isDoneArrow) return true;
            if (isDoneAttack || isDoneSkill || isDoneDead || isDoneEffect || isDoneArrow)
            {
                this.isDone = true;
                return true;
            }
            UpdateMana();
            this.isDone = false;
            return false;
        }

        public void UpdateMana()
        {
            HeroDataCf heroDataCf = UserManager.Instance.SetdataCf(this);
            CurindexSkill++;
            SetSkillBar(heroDataCf, CurindexSkill);
        }
        [ContextMenu("InitBoss")]
        public void InitBoss()
        {
            HpBar.gameObject.SetActive(false);
            SkillBar.gameObject.SetActive(false);
        }

        private void SetSkillBar(HeroDataCf heroDataCf, int CurIndexSkill)
        {
            if (heroDataCf.isBoss)
            {
                BosController.instance.UpdateManaBoss(heroDataCf.IndexSkill, CurIndexSkill);
                Debug.Log("setHpBoss");
            }
            else
            {
                this.SkillBar.UpdateHpBar(heroDataCf.IndexSkill, this.CurindexSkill);
            }
        }
        private void SetHpBar(HeroDataCf heroDataCf, int curHp)
        {
            if (heroDataCf.isBoss)
            {
                BosController.instance.UpdateHpBoss(heroDataCf.Hp, curHp);
                Debug.Log("setHpBoss");
            }
            else
            {
                this.HpBar.UpdateHpBar(heroDataCf.Hp, curHp);
            }
        }
    }
}


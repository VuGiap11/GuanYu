
using DG.Tweening;
using GuanYu;
using Spine;
using Spine.Unity;
using UnityEngine;


public class BackGroundMove : MonoBehaviour
{
    [Header("Old")]
    [SerializeField] private GameObject LeftBackGround;
    [SerializeField] private GameObject RightBackGround;
    [SerializeField] private GameObject MidBackGround;

    [SerializeField] private Transform LeftTarget;
    [SerializeField] private Transform RightTarget;
    [SerializeField] private Transform MidTarget;

    [SerializeField] private Transform curLeftPos;
    [SerializeField] private Transform curRightPos;
    [SerializeField] private Transform curMidPos;
    public float timeMove = 0.8f;

    [Header("New")]
    public SkeletonGraphic SpineAnimationChangeScene;

    public void Move()
    {
        ResetPosition();
        LeftMove();
        RightMove();
        MidMove();
    }
    public void ResetPosition()
    {
        LeftBackGround.SetActive(true);
        RightBackGround.SetActive(true);
        MidBackGround.SetActive(true);
    }
    public void LeftMove()
    {
        LeftBackGround.transform.DOMove(LeftTarget.position, timeMove)
                        .SetEase(Ease.Linear)
                .OnStart(() => { })
            .SetDelay(0f)
            .OnComplete(() =>
            {
                LeftBackGround.SetActive(false);
                LeftBack();
                BattleController.instance.battleArena.Combatt();
            });
    }
    private void LeftBack()
    {
        LeftBackGround.transform.DOMove(curLeftPos.position, 0.01f)
                        .SetEase(Ease.Linear)
                .OnStart(() => { })
            .SetDelay(0f)
            .OnComplete(() =>
            {
            });
    }
    public void RightMove()
    {
        RightBackGround.transform.DOMove(RightTarget.position, timeMove)
                        .SetEase(Ease.Linear)
                .OnStart(() => { })
            .SetDelay(0f)
            .OnComplete(() =>
            {
                RightBackGround.SetActive(false);
                RightBack();
            });

    }
    private void RightBack()
    {
        RightBackGround.transform.DOMove(curRightPos.position, 0.01f)
                        .SetEase(Ease.Linear)
                .OnStart(() => { })
            .SetDelay(0f)
            .OnComplete(() =>
            {
            });
    }
    public void MidMove()
    {
        MidBackGround.transform.DOMove(MidTarget.position, timeMove)
                       .SetEase(Ease.Linear)
               .OnStart(() => { })
           .SetDelay(0f)
           .OnComplete(() =>
           {

               MidBackGround.SetActive(false); MidBack();
           });
    }
    private void MidBack()
    {
        MidBackGround.transform.DOMove(curMidPos.position, 0.01f)
                        .SetEase(Ease.Linear)
                .OnStart(() => { })
            .SetDelay(0f)
            .OnComplete(() =>
            {

            });
    }

    public void OutAnimationChangeScene()
    {
        if (SpineAnimationChangeScene != null)
        {
            SpineAnimationChangeScene.gameObject.SetActive(true);
            SpineAnimationChangeScene.AnimationState.SetAnimation(0, "out", false).TimeScale = 0.2f;
            BattleController.instance.battleArena.SpawnBattleArena();
            SpineAnimationChangeScene.AnimationState.Complete += OnSpinDropComplete;
        }

    }

    private void OnSpinDropComplete(TrackEntry trackEntry)
    {
        if (SpineAnimationChangeScene != null)
        {
            SpineAnimationChangeScene.AnimationState.Complete -= OnSpinDropComplete;
        }
        SpineAnimationChangeScene.gameObject.SetActive(false);
        BattleController.instance.battleArena.Combatt();
        //SpineAnimationChangeScene.gameObject.SetActive(false);
    }

}

using DG.Tweening;
using GuanYu.Hero;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowMove : MonoBehaviour
{
    public float moveTime = 0.4f;
    public void Move(HeroModel target, Action callback = null)
    {
        //if(target == null) return;
        Vector3 tar = new Vector3(target.transform.position.x, target.transform.position.y+0.6f, target.transform.position.z);
            transform.DOMove(tar, moveTime)
            .SetEase(Ease.Linear)
            .SetDelay(0f)
            .OnComplete(() =>
            {
                if(callback != null) 
                {
                    callback.Invoke();
                }
                DestroyBrow();
            });
    }
    private void DestroyBrow()
    {
        Destroy(gameObject);
    }
}

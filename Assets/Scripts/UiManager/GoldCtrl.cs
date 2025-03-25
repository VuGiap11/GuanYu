using GuanYu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCtrl : MonoBehaviour
{
    public static GoldCtrl instance;

    public RectTransform GoldPosTarget;// VIJ TRIS TRONG CANVAS THI DUNG RECTRANFORM
    public GoldMove Gold;
    public List<GoldMove> Golds;
    public Transform GoldParent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SpawnGold(Vector3 pos)
    {
        int randomInt = UnityEngine.Random.Range(2, 5);
        for (int i = 0; i < randomInt; i++)
        {
            float scale = UnityEngine.Random.Range(0.3f, 0.9f);
            float posnewx = UnityEngine.Random.Range(-0.5f, 0.5f);
            float posnewy = UnityEngine.Random.Range(-0.1f, 0.3f);
            GoldMove gold = Instantiate(Gold);
            Golds.Add(gold);
            Vector3 position = new Vector3(pos.x + posnewx, pos.y + posnewy, pos.z);
            gold.transform.position = position;
            gold.targetGoldPos = this.GoldPosTarget;
            gold.transform.SetParent(GoldParent, true);
           
            //gold.transform.localScale = Vector3.one;
            gold.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
    //public void MoveGold(Action spawn = null)
    //{
    //    Debug.Log("gold move");
    //    StartCoroutine(GoldMove(spawn));
    //}
    //public IEnumerator GoldMove(Action spawn = null)
    //{
    //    yield return 0.05f;
    //    Debug.Log("gold move");
    //    for (int i = 0; i < Golds.Count; i++)
    //    {
    //        //if (spawn != null)
    //        //{
    //        //    Golds[i].Move(spawn);
    //        //}
    //        if (Golds[i] == null) continue;
    //        Golds[i].Move();
    //        if (i == Golds.Count - 1)
    //        {


    //            Golds[i].Move(spawn);
    //            Golds.Clear();
    //        }
    //    }


    //}
    public void MoveGold()
    {
        //Debug.Log("gold move");
        StartCoroutine(GoldMove());
    }
    public IEnumerator GoldMove()
    {
        yield return new WaitForSeconds(0.05f);
        //Debug.Log("gold move");
        for (int i = 0; i < Golds.Count; i++)
        {
            //if (spawn != null)
            //{
            //    Golds[i].Move(spawn);
            //}
            if (Golds[i] == null) continue;
            if (i == Golds.Count - 1)
            {
                Golds[i].isDone = true;
            }
            Golds[i].Move();
        }
        Golds.Clear();

    }
}


using UnityEngine;

public class MyFunction : MonoBehaviour
{
    public static void ClearChild(Transform trans)
    {
        int count = trans.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            Transform child = trans.GetChild(i);
            child.gameObject.SetActive(false);
            DestroyImmediate(child.gameObject);
        }
    }
}

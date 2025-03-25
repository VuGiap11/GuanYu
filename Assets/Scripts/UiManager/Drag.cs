using GuanYu.Hero;
using UnityEngine;


namespace GuanYu
{
    public class Drag : MonoBehaviour
    {

        [SerializeField] private bool isDragged = false;
        [SerializeField] private Vector2 mouseDragStartPosition;
        [SerializeField] private Vector2 startPos;
        public int index; // ví trí mà hero đứng trong đội hình 
        public Drag drag;
        //public SkeletonAnimation skeletonAnimation;
        public void InitAvaHeroUI(int index)
        {
            GameObject go = HeroAssets.Instance.GetHeroUIPrefabByIndex(index);
            if (go != null)
            {
                MyFunction.ClearChild(this.transform);
                go.transform.SetParent(this.transform,false);
                go.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                go.transform.localScale = new Vector3(1f, 1f, 1f);

            }
        }
        private void Start()
        {
            //startPos = transform.position;
        }
        private void OnMouseDown()
        {
            isDragged = true;
            mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos = transform.position;
            //Debug.Log(startPos);
            ////IncreaseOrderInlayer(5);

            transform.SetParent(GameManager.instance.heroPanel.PosSwap);
        }

        private void OnMouseDrag()
        {
            if (isDragged)
            {
                //transform.localPosition = spriteDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
                mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mouseDragStartPosition;
                //Debug.Log(transform.position);
            }
        }
        private void OnMouseUp()
        {
            isDragged = false;
            mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int pos = GameManager.instance.CheckNearPos(mouseDragStartPosition);

            //Debug.Log(pos);
            if (pos == -1)
            {
                transform.position = startPos;
                //Debug.Log(pos);
            }
            else
            {

                GameManager.instance.swap(this.drag, index, pos);
                index = pos;
                //IncreaseOrderInlayer(1);
            }

        }

        //private void IncreaseOrderInlayer(int increaseAmount)
        //{
        //    if (skeletonAnimation != null)
        //    {
        //        Renderer renderer = skeletonAnimation.GetComponent<Renderer>();
        //        if (renderer != null)
        //        {
        //            renderer.sortingOrder = increaseAmount;
        //        }
        //        else
        //        {
        //            Debug.LogWarning("Renderer component not found on the SkeletonAnimation.");
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogWarning("SkeletonAnimation not assigned.");
        //    }
        //}
    }
}

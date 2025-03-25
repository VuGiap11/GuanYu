
using GuanYu.UI;
using GuanYu.User;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuanYu
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        public List<Image> avartarEnemy;

        public ArenaUI arenaUI;
        public CampainUI campainUI;

        [SerializeField] private GameObject warningCampainObj;
        [SerializeField] private GameObject warningArenaObj;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void Warning()
        {
            warningPanel = StartCoroutine(warning());
        }
        Coroutine warningPanel;
        IEnumerator warning()
        {
            if (MenuController.Instance.Menu == Menu.Camp)
            {
                warningCampainObj.SetActive(true);
                yield return new WaitForSeconds(1f);
                warningCampainObj.SetActive(false);
            }else
            {
                warningArenaObj.SetActive(true);
                yield return new WaitForSeconds(1f);
                warningArenaObj.SetActive(false);
            }

        }

    }
}


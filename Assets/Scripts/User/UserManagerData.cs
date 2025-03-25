using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuanYu
{
    public class DataPlayerController
    {
        public int gold;
        public int star;
        public int point;
        public int levelCampain;
        public int NumberWin;
        public int NumberLose;
        public string Name;
        public DataPlayerController(int gold, int star, int point, int levelCampain, int numberWin, int numberLose, string name)
        {
            this.gold = gold;
            this.star = star;
            this.point = point;
            this.levelCampain = levelCampain;
            this.NumberWin = numberWin;
            this.NumberLose = numberLose;
            this.Name = name;
        }

        public class UserManagerData : MonoBehaviour
        {
            // Start is called before the first frame update
            void Start()
            {

            }

            // Update is called once per frame
            void Update()
            {

            }
        }
    }
}

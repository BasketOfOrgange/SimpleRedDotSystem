using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
namespace Assets.Scripts
{
	public class RdTester : MonoBehaviour
	{
        public static Dictionary<string, int> tempDic = new Dictionary<string, int>();

        public static bool TryGetData(string str,out int res )
        {
            if (tempDic.ContainsKey(str))
            {
                res = tempDic[str];
                return true;
            }
            res = 0;
            return false;

        }
        public static void AddDic(string str)
        {
            if (!tempDic.ContainsKey(str))
            {
                tempDic.Add(str, 0);
            }
            tempDic[str]++;

            RedDotManager.CheckAllTerminalTrigger();
        }
        public static void KillDic(string str)
        {
            if ( tempDic.ContainsKey(str))
            {
                tempDic.Remove(str);
            } 

            RedDotManager.CheckAllTerminalTrigger();
        }
        private void Awake()
        {
            RedDotManager.OnCreate(); Debug.LogError("³õÊ¼»¯Íê±Ïdb");

        }
        private void Start()
        {
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1)) { RedDotManager.OnPlayerDoSomething("newEquip"); Debug.LogError("fas"); }

            if (Input.GetKeyDown(KeyCode.Alpha1)) { AddDic("test111"); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { AddDic("test112"); }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { AddDic("test121"); }

            if (Input.GetKeyDown(KeyCode.Q)) { KillDic("test111"); }
            if (Input.GetKeyDown(KeyCode.W)) { KillDic("test112"); }
            if (Input.GetKeyDown(KeyCode.E )) { KillDic("test121"); }

        }
    }
}
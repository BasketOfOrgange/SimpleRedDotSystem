using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{ 
    [CreateAssetMenu(fileName = "RedDotActionEffect", menuName = "创建玩家事件和红点响应数据")]
    public class RedDotPlayerActionData : ScriptableObject
    {
        public List<RedDot_PlayerAction_Id_Pair> Effectors; 

    }


    [System.Serializable]
    public class RedDot_PlayerAction_Id_Pair
    {
        public string tempStr;
        public string ActionName;
        public List<int> EffectRedDotId;//可能会受到影响改变的id
    }
     
}
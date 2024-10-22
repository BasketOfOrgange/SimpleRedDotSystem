using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{ 
    [CreateAssetMenu(fileName = "RedDotActionEffect", menuName = "��������¼��ͺ����Ӧ����")]
    public class RedDotPlayerActionData : ScriptableObject
    {
        public List<RedDot_PlayerAction_Id_Pair> Effectors; 

    }


    [System.Serializable]
    public class RedDot_PlayerAction_Id_Pair
    {
        public string tempStr;
        public string ActionName;
        public List<int> EffectRedDotId;//���ܻ��ܵ�Ӱ��ı��id
    }
     
}
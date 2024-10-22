using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Assets.Scripts
{ 
    [System.Serializable]
    public class RedDotData  
    {
        public string tempStr; //废弃，不删除
        public List<RedDotData> childs;
        public int Id;             //数据表 ID   
        public RedDotType type;
        //[Header("终端需要配置触发器类")]
        public RedDotTriggerType TerminalTrigger; 

    }  
    public enum RedDotType
    {
        Simple,//简单--不需要对其里面组件赋值，比如text
        WithNumber,//带数字


        //ExclamationDot,//感叹号--注释是因为可以用simple来标记
    }
}
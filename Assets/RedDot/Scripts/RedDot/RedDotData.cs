using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Assets.Scripts
{ 
    [System.Serializable]
    public class RedDotData  
    {
        public string tempStr; //��������ɾ��
        public List<RedDotData> childs;
        public int Id;             //���ݱ� ID   
        public RedDotType type;
        //[Header("�ն���Ҫ���ô�������")]
        public RedDotTriggerType TerminalTrigger; 

    }  
    public enum RedDotType
    {
        Simple,//��--����Ҫ�������������ֵ������text
        WithNumber,//������


        //ExclamationDot,//��̾��--ע������Ϊ������simple�����
    }
}
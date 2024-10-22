using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
	public class RedDotLeaf : RedDotNode
    {
        private RedDotTriggerBase TerminalTrigger;

        public override bool IsLeaf => true;

        public int Index = -1;

        public RedDotLeaf(RedDotData d) : base(d)
        {
        }

        //ִ�м��
        public void DoCheckTrigger()
        {
            if (TerminalTrigger == null)
            {
                Debug.Log($"��� {Data.Id} ,{Data.tempStr}û��ע��ˢ����");
                return;
            }
            var tgr = TerminalTrigger.CheckRedDot();

            IsActive = TerminalTrigger.IsActive; 
            RedCount = IsActive ? tgr.Count : 0; //��������
            RedDotType = tgr.Type;
            OnStatusChaned();
            Debug.Log($"��� {Data.Id} ,{Data.tempStr}��ʼ�������{IsActive},������{RedCount}������{RedDotType}");

            //����������߿���״̬���ⲻһ�����Ÿı�
            //if (active != IsActive || lastRedCount != RedCount)
            {

            }
        }
        public void OnCreate(RedDotRoot root, RedDotNode parent, RedDotTriggerBase checker =null, int index = -1)
        {
            TerminalTrigger = checker;
            Parent = parent;
            Root = root;
            Index = index;
            if (checker != null)
            {
                TerminalTrigger = checker;
            }
            else
            {
                Debug.Log($"���{Data.Id}û�д�����");
            }
            RedCount = 0; 
        }

        public override long GetActiveCount()
        {
            return IsActive ? 1 : 0;
        }

       
        //״̬�ı�
        public override void OnStatusChaned()
        {
            //֪ͨroot�ı�
            if (Root != null)
            {
                Root.OnLeafStatusChanged(this, IsActive);
            }
               //֪ͨparent�ı�
            if (Parent != null)
            {
                Parent.OnChildStatusChange(this);
            }

            base.OnStatusChaned();
        }

        public override void OnRelease()
        { 
            Root = null;
            Parent = null;
            base.OnRelease();
        }
    }
}

 
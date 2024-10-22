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

        //执行检查
        public void DoCheckTrigger()
        {
            if (TerminalTrigger == null)
            {
                Debug.Log($"红点 {Data.Id} ,{Data.tempStr}没有注册刷新类");
                return;
            }
            var tgr = TerminalTrigger.CheckRedDot();

            IsActive = TerminalTrigger.IsActive; 
            RedCount = IsActive ? tgr.Count : 0; //更新数量
            RedDotType = tgr.Type;
            OnStatusChaned();
            Debug.Log($"红点 {Data.Id} ,{Data.tempStr}开始检测结果：{IsActive},数量：{RedCount}，类型{RedDotType}");

            //如果数量或者开合状态任意不一样，才改变
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
                Debug.Log($"红点{Data.Id}没有触发器");
            }
            RedCount = 0; 
        }

        public override long GetActiveCount()
        {
            return IsActive ? 1 : 0;
        }

       
        //状态改变
        public override void OnStatusChaned()
        {
            //通知root改变
            if (Root != null)
            {
                Root.OnLeafStatusChanged(this, IsActive);
            }
               //通知parent改变
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

 
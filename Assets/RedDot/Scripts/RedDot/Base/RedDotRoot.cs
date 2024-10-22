using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
namespace Assets.Scripts
{
	public class RedDotRoot : RedDotNode
    {
        private List<RedDotNode> ActivedLeaves = new List<RedDotNode>();

        public RedDotRoot(RedDotData d) : base(d)
        {
        }

        public void OnCreate(bool isactive = false)
        {
            Root = this;
            IsActive = isactive;
            RedCount = 0;
        }

        public override long GetRedNum()
        {
            var count = 0L;
            foreach (var node in ActivedLeaves)
            {
                count += node.GetRedNum();
            }
            return count;
        }

        public void OnLeafStatusChanged(RedDotNode node, bool statusIsOn)
        {
            if (statusIsOn)
            {
                if (!ActivedLeaves.Contains(node))
                    ActivedLeaves.Add(node);
            }
            else
            {
                if (ActivedLeaves.Contains(node))
                    ActivedLeaves.Remove(node);
            }

            RedCount = GetRedNum();
            OnStatusChaned();
        }

    }
}
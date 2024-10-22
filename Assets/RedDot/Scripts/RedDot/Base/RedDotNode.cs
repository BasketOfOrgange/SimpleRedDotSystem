using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
// 定义命名空间
namespace Assets.Scripts
{
    // 定义类 RedDotNode
    public class RedDotNode 
    {
        public RedDotNode (RedDotData d)
        {
            Data = d;
        }

        private RedDotData _data;
        public RedDotData Data
        {
            get
            {
                if (_data == null)
                    _data = new RedDotData();

                return _data;
            }
           private set
            {
                _data = value;
            }

        }
        public RedDotRoot Root { get; protected set; }// 获取和设置根节点的属性 
        public RedDotNode Parent { get; protected set; } // 获取和设置父节点的属性 
        public List<RedDotNode> Children { get; protected set; }  // 获取和设置子节点列表的属性


        public bool ReChangeStatus = false;  // 表示是否需要重新改变状态的标志

        private bool _isActive;   // 表示当前节点是否激活

        // 获取和设置激活状态的属性，在设置时进行了一些调试代码的输出
        public bool IsActive
        {
            get => _isActive;
            protected set
            {
                //debugCode
                _isActive = value;
            }
        }

        public long RedCount { get; protected set; }   // 红点数量
        public int RedDotType { get; set; }//红点类型0感叹号，1数字，2自定义，3自定义【在实现中注释】
        public virtual bool IsLeaf { get { return false; } }  // 判断是否为叶子节点的属性，默认返回 false，可在子类中重写

        public List<Action<bool>> OnStatusChanged = new List<Action<bool>>();   // 当状态改变时执行的委托列表

        public Action<long> OnActivityCountChanged;   // 当活动计数改变时执行的委托

        public bool ClickDisapperAndClicked = false;       // 点击消失且已被点击的标志


        // 创建节点的方法，可以传入根节点、父节点和初始激活状态
        public void OnCreate(RedDotRoot root = null, RedDotNode parent = null, bool isactive = false)
        {
            // 如果有父节点
            if (parent != null)
            {
                // 设置父节点
                Parent = parent;
                // 如果传入了根节点
                if (root != null)
                {
                    Root = root;
                }
            }

            // 设置激活状态
            IsActive = isactive;
            // 重置红点数量
            RedCount = 0;
        }

       
        // 释放节点资源的方法，在子类中可重写
        public virtual void OnRelease()
        {
        }

        // 当子节点状态改变时调用的方法
        public virtual void OnChildStatusChange(RedDotNode childNode)
        {
            // 如果点击消失且不需要重新改变状态，则直接返回
            //if (ClickDisapperAndClicked && !ReChangeStatus) return;
         
            
            // 获取激活子节点的数量
            long count = GetActiveCount();
            RedCount = count;
            // 如果子节点激活且当前节点未激活
            if (childNode.IsActive && !IsActive)
            {
                // 设置当前节点激活状态为子节点的激活状态
                IsActive = childNode.IsActive;
                // 调用状态改变的方法
                OnStatusChaned();
                return;
            }

            // 如果子节点未激活且当前节点激活
            if (!childNode.IsActive && IsActive)
            {
                // 如果激活子节点数量为 0
                if (count <= 0)
                {
                    // 设置当前节点未激活
                    IsActive = false;
                    // 调用状态改变的方法
                    OnStatusChaned();
                }
            }
            // 获取红点数量
            var redCount = GetRedNum();
            // 如果红点数量改变
            if (RedCount != redCount)
            {
                // 调用状态改变的方法
                OnStatusChaned();
            }

            // 调用活动计数改变的委托
            OnActivityCountChanged?.Invoke(count);
        }

        // 状态改变时调用的方法
        public virtual void OnStatusChaned()
        {
            //// 如果点击消失且不需要重新改变状态，则直接返回
            //if (ClickDisapperAndClicked && !ReChangeStatus) return;
            // 遍历状态改变的委托列表并调用
            OnStatusChanged?.ForEach(x => x.Invoke(IsActive));
            // 如果有父节点，则调用父节点的子节点状态改变方法
            if (Parent != null) Parent.OnChildStatusChange(this);
        }

        // 获取红点数量的方法，可在子类中重写
        public virtual long GetRedNum()
        {
            return RedCount;
        }

        // 获取激活子节点数量的方法
        public virtual long GetActiveCount()
        {
            RedDotType = 0;
            int count = 0;
            // 如果没有子节点则返回 0
            if (Children == null) return 0;
            // 遍历子节点列表
            for (int i = 0; i < Children.Count; i++)
            {
                // 如果子节点激活，则计数加一
                if (Children[i].IsActive) 
                {
                    count++;
                    RedDotType = RedDotType < Children[i].RedDotType ? Children[i].RedDotType : RedDotType;
                } 
            }

            return count;
        }

        // 添加子节点的方法
        public void AddChild(RedDotNode node)
        {
            // 如果子节点列表为空，则创建一个新的列表
            if (Children == null) Children = new List<RedDotNode>();
            // 设置子节点的父节点为当前节点
            node.Parent = this;
            // 将子节点添加到列表中
            Children.Add(node);
            // 如果子节点激活，则设置当前节点激活
            if (node.IsActive)
                IsActive = true;
        }

        // 获取激活子节点数量的递归方法
        private int GetActivityChildCount(RedDotNode root)
        {
            int count = 0;
            // 遍历子节点列表
            for (int i = 0; i < root.Children.Count; i++)
            {
                var child = Children[i];
                var leaf = child as RedDotLeaf;
                // 如果子节点是叶子节点
                if (leaf != null)
                {
                    // 递归调用获取激活子节点数量的方法
                    count += GetActivityChildCount(child);
                }
                else
                {
                    // 如果子节点激活，则计数加一
                    if (leaf.IsActive)
                    {
                        count++;
                    }
                }
            }

            return count;
        }



        // 清空状态改变委托列表的方法
        public void ClearAllCB()
        {
            OnStatusChanged.Clear();
        }

        // 添加状态改变委托的方法
        public void AddOnStatusChangedCB(Action<bool> cb)
        { 
            // 如果委托不在列表中，则添加
            if (!OnStatusChanged.Contains(cb))
                OnStatusChanged.Add(cb);
            else
                Debug.Log("AddOnStatusChangedCB repeated");
        }

        // 移除状态改变委托的方法
        public void RemoveOnStatusChangedCB(Action<bool> cb)
        {
            // 如果委托在列表中，则移除
            if (OnStatusChanged.Contains(cb))
                OnStatusChanged.Remove(cb); 
        }

        // 延迟移除状态改变委托的方法
        public void DelayRemoveOnStatusChangedCB(Action<bool> cb)
        {
            //ModuleEngine.StartCoroutine(DelayRemoveOnStatusChangedCBInternal(cb));
        }

        // 延迟移除状态改变委托的内部方法，使用协程实现
        private IEnumerator DelayRemoveOnStatusChangedCBInternal(Action<bool> cb)
        {
            yield return 1;
            RemoveOnStatusChangedCB(cb);
        }

        // 调用传入的回调方法并传入当前节点
        public void Call(Action<RedDotNode> callback)
        {
            callback?.Invoke(this);
        }
         
    }
}
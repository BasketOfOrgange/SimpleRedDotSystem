using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
// ���������ռ�
namespace Assets.Scripts
{
    // ������ RedDotNode
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
        public RedDotRoot Root { get; protected set; }// ��ȡ�����ø��ڵ������ 
        public RedDotNode Parent { get; protected set; } // ��ȡ�����ø��ڵ������ 
        public List<RedDotNode> Children { get; protected set; }  // ��ȡ�������ӽڵ��б������


        public bool ReChangeStatus = false;  // ��ʾ�Ƿ���Ҫ���¸ı�״̬�ı�־

        private bool _isActive;   // ��ʾ��ǰ�ڵ��Ƿ񼤻�

        // ��ȡ�����ü���״̬�����ԣ�������ʱ������һЩ���Դ�������
        public bool IsActive
        {
            get => _isActive;
            protected set
            {
                //debugCode
                _isActive = value;
            }
        }

        public long RedCount { get; protected set; }   // �������
        public int RedDotType { get; set; }//�������0��̾�ţ�1���֣�2�Զ��壬3�Զ��塾��ʵ����ע�͡�
        public virtual bool IsLeaf { get { return false; } }  // �ж��Ƿ�ΪҶ�ӽڵ�����ԣ�Ĭ�Ϸ��� false��������������д

        public List<Action<bool>> OnStatusChanged = new List<Action<bool>>();   // ��״̬�ı�ʱִ�е�ί���б�

        public Action<long> OnActivityCountChanged;   // ��������ı�ʱִ�е�ί��

        public bool ClickDisapperAndClicked = false;       // �����ʧ���ѱ�����ı�־


        // �����ڵ�ķ��������Դ�����ڵ㡢���ڵ�ͳ�ʼ����״̬
        public void OnCreate(RedDotRoot root = null, RedDotNode parent = null, bool isactive = false)
        {
            // ����и��ڵ�
            if (parent != null)
            {
                // ���ø��ڵ�
                Parent = parent;
                // ��������˸��ڵ�
                if (root != null)
                {
                    Root = root;
                }
            }

            // ���ü���״̬
            IsActive = isactive;
            // ���ú������
            RedCount = 0;
        }

       
        // �ͷŽڵ���Դ�ķ������������п���д
        public virtual void OnRelease()
        {
        }

        // ���ӽڵ�״̬�ı�ʱ���õķ���
        public virtual void OnChildStatusChange(RedDotNode childNode)
        {
            // ��������ʧ�Ҳ���Ҫ���¸ı�״̬����ֱ�ӷ���
            //if (ClickDisapperAndClicked && !ReChangeStatus) return;
         
            
            // ��ȡ�����ӽڵ������
            long count = GetActiveCount();
            RedCount = count;
            // ����ӽڵ㼤���ҵ�ǰ�ڵ�δ����
            if (childNode.IsActive && !IsActive)
            {
                // ���õ�ǰ�ڵ㼤��״̬Ϊ�ӽڵ�ļ���״̬
                IsActive = childNode.IsActive;
                // ����״̬�ı�ķ���
                OnStatusChaned();
                return;
            }

            // ����ӽڵ�δ�����ҵ�ǰ�ڵ㼤��
            if (!childNode.IsActive && IsActive)
            {
                // ��������ӽڵ�����Ϊ 0
                if (count <= 0)
                {
                    // ���õ�ǰ�ڵ�δ����
                    IsActive = false;
                    // ����״̬�ı�ķ���
                    OnStatusChaned();
                }
            }
            // ��ȡ�������
            var redCount = GetRedNum();
            // �����������ı�
            if (RedCount != redCount)
            {
                // ����״̬�ı�ķ���
                OnStatusChaned();
            }

            // ���û�����ı��ί��
            OnActivityCountChanged?.Invoke(count);
        }

        // ״̬�ı�ʱ���õķ���
        public virtual void OnStatusChaned()
        {
            //// ��������ʧ�Ҳ���Ҫ���¸ı�״̬����ֱ�ӷ���
            //if (ClickDisapperAndClicked && !ReChangeStatus) return;
            // ����״̬�ı��ί���б�����
            OnStatusChanged?.ForEach(x => x.Invoke(IsActive));
            // ����и��ڵ㣬����ø��ڵ���ӽڵ�״̬�ı䷽��
            if (Parent != null) Parent.OnChildStatusChange(this);
        }

        // ��ȡ��������ķ�����������������д
        public virtual long GetRedNum()
        {
            return RedCount;
        }

        // ��ȡ�����ӽڵ������ķ���
        public virtual long GetActiveCount()
        {
            RedDotType = 0;
            int count = 0;
            // ���û���ӽڵ��򷵻� 0
            if (Children == null) return 0;
            // �����ӽڵ��б�
            for (int i = 0; i < Children.Count; i++)
            {
                // ����ӽڵ㼤��������һ
                if (Children[i].IsActive) 
                {
                    count++;
                    RedDotType = RedDotType < Children[i].RedDotType ? Children[i].RedDotType : RedDotType;
                } 
            }

            return count;
        }

        // ����ӽڵ�ķ���
        public void AddChild(RedDotNode node)
        {
            // ����ӽڵ��б�Ϊ�գ��򴴽�һ���µ��б�
            if (Children == null) Children = new List<RedDotNode>();
            // �����ӽڵ�ĸ��ڵ�Ϊ��ǰ�ڵ�
            node.Parent = this;
            // ���ӽڵ���ӵ��б���
            Children.Add(node);
            // ����ӽڵ㼤������õ�ǰ�ڵ㼤��
            if (node.IsActive)
                IsActive = true;
        }

        // ��ȡ�����ӽڵ������ĵݹ鷽��
        private int GetActivityChildCount(RedDotNode root)
        {
            int count = 0;
            // �����ӽڵ��б�
            for (int i = 0; i < root.Children.Count; i++)
            {
                var child = Children[i];
                var leaf = child as RedDotLeaf;
                // ����ӽڵ���Ҷ�ӽڵ�
                if (leaf != null)
                {
                    // �ݹ���û�ȡ�����ӽڵ������ķ���
                    count += GetActivityChildCount(child);
                }
                else
                {
                    // ����ӽڵ㼤��������һ
                    if (leaf.IsActive)
                    {
                        count++;
                    }
                }
            }

            return count;
        }



        // ���״̬�ı�ί���б�ķ���
        public void ClearAllCB()
        {
            OnStatusChanged.Clear();
        }

        // ���״̬�ı�ί�еķ���
        public void AddOnStatusChangedCB(Action<bool> cb)
        { 
            // ���ί�в����б��У������
            if (!OnStatusChanged.Contains(cb))
                OnStatusChanged.Add(cb);
            else
                Debug.Log("AddOnStatusChangedCB repeated");
        }

        // �Ƴ�״̬�ı�ί�еķ���
        public void RemoveOnStatusChangedCB(Action<bool> cb)
        {
            // ���ί�����б��У����Ƴ�
            if (OnStatusChanged.Contains(cb))
                OnStatusChanged.Remove(cb); 
        }

        // �ӳ��Ƴ�״̬�ı�ί�еķ���
        public void DelayRemoveOnStatusChangedCB(Action<bool> cb)
        {
            //ModuleEngine.StartCoroutine(DelayRemoveOnStatusChangedCBInternal(cb));
        }

        // �ӳ��Ƴ�״̬�ı�ί�е��ڲ�������ʹ��Э��ʵ��
        private IEnumerator DelayRemoveOnStatusChangedCBInternal(Action<bool> cb)
        {
            yield return 1;
            RemoveOnStatusChangedCB(cb);
        }

        // ���ô���Ļص����������뵱ǰ�ڵ�
        public void Call(Action<RedDotNode> callback)
        {
            callback?.Invoke(this);
        }
         
    }
}
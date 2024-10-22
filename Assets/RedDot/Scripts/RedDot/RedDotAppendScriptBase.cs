using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts
{

    //挂载基类*-,可以被通知，也会在出现的时候主动拿数据进行刷新，这个不对检测器进行调用，只有在开始的时候和事件通知的时候进行检测数据塔 
    //这个挂载的脚本只会对自己进行修改，不会关注兄弟和叶子节点
    public class RedDotAppendScriptBase : MonoBehaviour
    {

        [Header("<color=red>红点ID</color>")] public int Index;
        [Header("红点类型")] public RedDotType dotType;
        public GameObject DotGo;
        public GameObject? DotGoWithCount;
        public GameObject? CustomReddot1;
        public GameObject? CustomReddot2;
        [Header("数量txt（可选）")] public Text? CountTxt;

        //从数据塔拿数据，在打开的时候进行数据刷新
        private RedDotNode Node { get { return RedDotManager.Get(Index); } }

        private void OnDisable()
        {
            if (Node != null)
            {
                Node.RemoveOnStatusChangedCB(OnDataStatusChange);
            }
            OnGameObjectDisable();
        }
        private void OnEnable()
        {
            if (Node == null)
            {
                Debug.LogError("reddOT cant find:"+ Index);
                return;
            }
            if (Node != null)
            {
                Node.RemoveOnStatusChangedCB(OnDataStatusChange);
                Node.AddOnStatusChangedCB(OnDataStatusChange);
            }
            OnGameObjectActive();
            CheckToShow();
        }
        //当这个物体（必须是红点的父节点或者是可以支配红点的物体）显示与否的时候进行判断，父物体都不显示，说明红点一定不会显示
        protected virtual void OnGameObjectActive() { }
        protected virtual void OnGameObjectDisable() { }

        //注册进数据塔来进行相应
        private void OnDataStatusChange(bool status)
        {
            if (Node == null)
            {
                DotGo.SetActive(false);
                Debug.Log("<color=greed>数据塔居然没有数据：</color>" + Index);
                return;
            } 

            CheckToShow();
        }
        public void CheckToShow_Publick()
        {
            CheckToShow();
        }
        private void CheckToShow()
        {
            if (DotGo != null)
                DotGo.SetActive(false);
            if (DotGoWithCount != null)
                DotGoWithCount.SetActive(false);
            if (CustomReddot1 != null)
                CustomReddot1.SetActive(false);
             if (CustomReddot2 != null)
                CustomReddot2.SetActive(false);

            Debug.Log(gameObject.name );
            if (Node.RedDotType == 0)
            {
                SetType0();
            }
            else if (Node.RedDotType == 1)
            {
                bool ok = SetType1();
                if (!ok) {SetType0(); }
            }
            else if (Node.RedDotType == 2)
            {

                bool ok = SetType2();
                if (!ok) { SetType0(); }
            }   
            else if (Node.RedDotType == 3)
            {

                bool ok = SetType3();
                if (!ok) { SetType0(); }
            } 
        }
        private void SetType0()
        {
            DotGo.SetActive(Node.IsActive);
        }
        private bool SetType1()
        {
            RefreshCount();
            if (DotGoWithCount != null)
            {
                DotGoWithCount.SetActive(Node.IsActive);
                return true;
            } 
                //Debug.LogError(gameObject.name + "设置了带数字的红点，但是没有配置带数字的红点底图");
            return false;
        }
        private bool SetType2()
        {
            if (CustomReddot1 != null)
            {
                CustomReddot1.SetActive(Node.IsActive);
                return true;
            } 
                //Debug.LogError(gameObject.name + "设置了自定义的红点1，但是没有配置红点底图");
            return false ;


        }
        private bool SetType3()
        {
            if (CustomReddot2 != null)
            {
                CustomReddot2.SetActive(Node.IsActive);

            } 
                //Debug.LogError(gameObject.name + "设置了自定义的红点2，但是没有配置红点底图");
            return false;
        }
        private void RefreshCount()
        {
            var count = Node.GetRedNum();
            if (CountTxt != null)
                CountTxt.text = count > 0 ? count.ToString() : string.Empty;
            //else
                //Debug.LogError(gameObject.name+"没有配置带数量的红点txt");
        }
    }
}
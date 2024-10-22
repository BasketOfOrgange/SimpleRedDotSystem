using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts
{

    //���ػ���*-,���Ա�֪ͨ��Ҳ���ڳ��ֵ�ʱ�����������ݽ���ˢ�£�������Լ�������е��ã�ֻ���ڿ�ʼ��ʱ����¼�֪ͨ��ʱ����м�������� 
    //������صĽű�ֻ����Լ������޸ģ������ע�ֵܺ�Ҷ�ӽڵ�
    public class RedDotAppendScriptBase : MonoBehaviour
    {

        [Header("<color=red>���ID</color>")] public int Index;
        [Header("�������")] public RedDotType dotType;
        public GameObject DotGo;
        public GameObject? DotGoWithCount;
        public GameObject? CustomReddot1;
        public GameObject? CustomReddot2;
        [Header("����txt����ѡ��")] public Text? CountTxt;

        //�������������ݣ��ڴ򿪵�ʱ���������ˢ��
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
        //��������壨�����Ǻ��ĸ��ڵ�����ǿ���֧��������壩��ʾ����ʱ������жϣ������嶼����ʾ��˵�����һ��������ʾ
        protected virtual void OnGameObjectActive() { }
        protected virtual void OnGameObjectDisable() { }

        //ע�����������������Ӧ
        private void OnDataStatusChange(bool status)
        {
            if (Node == null)
            {
                DotGo.SetActive(false);
                Debug.Log("<color=greed>��������Ȼû�����ݣ�</color>" + Index);
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
                //Debug.LogError(gameObject.name + "�����˴����ֵĺ�㣬����û�����ô����ֵĺ���ͼ");
            return false;
        }
        private bool SetType2()
        {
            if (CustomReddot1 != null)
            {
                CustomReddot1.SetActive(Node.IsActive);
                return true;
            } 
                //Debug.LogError(gameObject.name + "�������Զ���ĺ��1������û�����ú���ͼ");
            return false ;


        }
        private bool SetType3()
        {
            if (CustomReddot2 != null)
            {
                CustomReddot2.SetActive(Node.IsActive);

            } 
                //Debug.LogError(gameObject.name + "�������Զ���ĺ��2������û�����ú���ͼ");
            return false;
        }
        private void RefreshCount()
        {
            var count = Node.GetRedNum();
            if (CountTxt != null)
                CountTxt.text = count > 0 ? count.ToString() : string.Empty;
            //else
                //Debug.LogError(gameObject.name+"û�����ô������ĺ��txt");
        }
    }
}
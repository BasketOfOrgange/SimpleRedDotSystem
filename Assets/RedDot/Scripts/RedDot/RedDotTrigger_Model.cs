using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
namespace Assets.Scripts
{

    public enum RedDotTriggerType
    {
        None = 0,
        Temp = 1001,  

        Test111=9001,
        Test112=9002,
        Test121=9003,

    }
    public class RedDotTrigger_Model { }
 
  
      //Ä£°å
    public class RedDotTrigger_Temp : RedDotTriggerBase
    {
        public override RedDotTriggerData CheckRedDot()
        {
            bool can = false; 


            RedDotTriggerData res = new RedDotTriggerData();
            res.isActive = can;
            res.Count = 1;
            res.Type = 0;
            return res;
        }
    }

    public class RedDotTrigger_111 : RedDotTriggerBase
    {
        public override RedDotTriggerData CheckRedDot()
        {
            bool can = RdTester.TryGetData("test111", out var outRes);


            RedDotTriggerData res = new RedDotTriggerData();
            res.isActive = can;
            res.Count = outRes;
            res.Type = 0;
            return res;
        }
    }
    public class RedDotTrigger_112 : RedDotTriggerBase
    {
        public override RedDotTriggerData CheckRedDot()
        {
            bool can = RdTester.TryGetData("test112", out var outRes);


            RedDotTriggerData res = new RedDotTriggerData();
            res.isActive = can;
            res.Count = outRes;
            res.Type = 0;
            return res;
        }
    }
       public class RedDotTrigger_121 : RedDotTriggerBase
    {
        public override RedDotTriggerData CheckRedDot()
        {
            bool can = RdTester.TryGetData("test121", out var outRes);


            RedDotTriggerData res = new RedDotTriggerData();
            res.isActive = can;
            res.Count = outRes;
            res.Type = 0;
            return res;
        }
    }

    public class TempDotTrigger : RedDotTriggerBase
    {

        protected void AddNetMsg(uint id)
        {
            //if (!RefreshNetMsg.TryGetValue(type, out var list))
            //{
            //    list = new List<uint>();
            //    RefreshNetMsg[type] = list;
            //}
            //list.Add(id);
        }

        protected void FocusTopic()
        {
            //if (_focusTopic.Contains(topic)) return;
            //_focusTopic.Add(topic);
        }

        public void FocusEvent()
        {
            //if (_focusRed.Contains(redId)) return;
            //_focusRed.Add(redId);
        }

        private void RedUpdateListener()
        {
            //if (message is EventRedDotUpdate update && _focusRed.Contains(update.redId))
            //{
            //    _dirty = true;
            //}
        }
        private void TopicListener()
        {
            //if (message is EventMsgReceive msgReceive && _focusTopic.Contains(msgReceive.topic))
            //{
            //    _dirty = true;
            //}
        }


        protected float _duration = 1;
        float _tempTime = 1;

        private void InvokePerSeconds()
        {
            if (_tempTime >= _duration)
            {
                CheckPerSconds();
                _tempTime = 0;
            }
            _tempTime += Time.deltaTime;
        }

        protected virtual void CheckPerSconds()
        {

        }

        public virtual void OnUpdate()
        {
            InvokePerSeconds();
        }

        public override RedDotTriggerData CheckRedDot()
        {
            throw new System.NotImplementedException();
        }

        //~RedDotTriggerBase()
        //{
        //    //foreach (var kv in RefreshNetMsg)
        //    //{
        //    //    foreach (var t in kv.Value)
        //    //        AppNetReceive.Instance.Remove(kv.Key, t, OnRefresh);
        //    //}
        //    //EventManager.Instance.RemoveListener<EventMsgReceive>(TopicListener);
        //    //EventManager.Instance.RemoveListener<EventRedDotUpdate>(RedUpdateListener);
        //}
    }



}
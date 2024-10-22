using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public struct RedDotTriggerData
    {
        public bool isActive;
        public int Count;
        public int Type;//0 Ä¬ÈÏ 1Êý×Ö

        public static RedDotTriggerData DefaltNo { get { return new RedDotTriggerData() { isActive=false,Count=0,Type=1}; } }
        public static  RedDotTriggerData DefaltYesOne { get { return new RedDotTriggerData() { isActive=true,Count=1,Type=1}; } }
        
    }
    //ÖÕ¶Ë¼ì²âÂß¼­
    public abstract class RedDotTriggerBase  
    {  

        public bool IsActive { get { return CheckRedDot().isActive; } }
        public abstract RedDotTriggerData CheckRedDot();
    }

}
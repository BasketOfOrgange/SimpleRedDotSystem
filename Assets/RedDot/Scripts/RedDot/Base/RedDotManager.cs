using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    //框架脚本
	public partial class RedDotManager
	{
        //数据塔和缓存是配置好的文件，在一开始就加入进来，不会change的


        private static Dictionary<int,  RedDotRoot> _redDotTreeMap =new Dictionary<int, RedDotRoot> (); //所有的根节点--这位是数据塔

        private static Dictionary<int, RedDotNode> _redDotMap = new Dictionary<int, RedDotNode> ();//id-key 所有的节点-这是快速查找的缓存

        private static Dictionary<int, RedDotTriggerBase> _redDotCheckMap = new Dictionary<int, RedDotTriggerBase>(); //红点检测

        private static Dictionary<string, List<int>> _redDotActions = new Dictionary<string, List<int>>();
         

        public static Dictionary<int, RedDotRoot> Roots  => _redDotTreeMap;  
      
        public static RedDotNode Get(int nodeId )
        {
            if (_redDotMap.TryGetValue(nodeId, out var res))
            {
                return res;
            } 
            return null;
        }

        public static RedDotTriggerBase GetChecker(int dotId)
        {
            _redDotCheckMap.TryGetValue(dotId, out RedDotTriggerBase checker);
            return checker;
        } 


        public static void AddNodeToMap(RedDotNode node)
        {
            if (!_redDotMap.TryGetValue(node.Data.Id, out _))
            { 
                _redDotMap.Add(node.Data.Id, node);
            }

            else
            {
                Debug.Log("<color=green>已经exist key </color>"+node.Data.Id+node.Data.tempStr);
                _redDotMap[node.Data.Id] = node;
            }
        }

        public static void AddToRootMap(RedDotRoot root)
        {
            _redDotTreeMap.Add(root.Data.Id,root);
            AddNodeToMap(root);
        }

        public static void OnCreate( )
        {
            _redDotTreeMap.Clear();
            _redDotMap.Clear();
            RegisterData(); 
            LoadPlayerActionConfig();
        }
        //检测所有树
        public static void CheckAllTerminalTrigger()
        {
            Debug.Log("全检测终端红点");
            foreach(var i in _redDotMap)
            {
                if(i.Value is RedDotLeaf lf)
                {
                    lf.DoCheckTrigger();
                }
            }
        } 
       
        //玩家操作
       public static void OnPlayerDoSomething(string useActionName)
        {
            if (_redDotActions.ContainsKey(useActionName) == false)
            {
                Debug.LogError($"没有配置这个事件 {useActionName}");
                return;
            }
            string triggerLog = $"玩家触发事件 {useActionName}";
            //找到所有影响的节点
            var lst = _redDotActions[useActionName];
            foreach(var i in lst)
            {
                var dot = Get(i);
                if (dot is RedDotLeaf l)
                    l.DoCheckTrigger();
                //dot.OnStatusChaned();
                triggerLog += $"[{i}]";
            }
            Debug.Log(triggerLog);

        }

        public static void OnDestroy()
        {
            _redDotTreeMap.Clear();
            _redDotMap.Clear();
            _redDotCheckMap.Clear(); 
        }

    }
}
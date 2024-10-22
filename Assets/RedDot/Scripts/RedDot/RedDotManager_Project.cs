using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    //扩充脚本
	public partial class RedDotManager   
	{

        private static void RegisterData()
        { 
            var db = Resources.Load<RedDotTree>("RedDot/RedDotDataBase");

            TreeReaderLog = "";
            foreach (var i in db.trees)
            {
                HandleRedDotData(i);
            } 

            TowerReaderLog = "";
            foreach (var i in _redDotTreeMap)
            {
                TowerReaderLog += $"【{i.Key}】";
                var m = i.Value;
                TempHandleRedDotData(m);
            }
            Debug.Log(TowerReaderLog);
        }

        //将数据转化为数据塔
        private static string TreeReaderLog = "";
        public static void HandleRedDotData(RedDotData data, int indentLevel = 0, RedDotNode father = null)
        {
            var tarFather = father;
            //构建数据塔
            if (indentLevel == 0)
            {
                var root = RedDotUtility.CreateRoot(data);

                if (tarFather == null)
                    tarFather = root;
                else
                    Debug.LogError("根节点怎么会有父呢？");

            }
            else
            { 
                if (data.childs != null&&data.childs.Count>0)
                {
                    tarFather = RedDotUtility.CreateNode(data, tarFather);
                }
                else
                {
                    tarFather = RedDotUtility.CreateLeaf(data, tarFather, GetTrigger(data.TerminalTrigger));
                }
            }

            //递归
            TreeReaderLog += $"[{data.Id}]  \n";
            if (data.childs != null && data.childs.Count > 0)
            {
                TreeReaderLog += $"[{data.Id}]==>";
                foreach (var child in data.childs)
                {
                    HandleRedDotData(child, indentLevel + 1, tarFather);
                }
            }
        }

        //玩家操作回调
        private static  void LoadPlayerActionConfig()
        {
            string addLog = "";
            var db = Resources.Load<RedDotPlayerActionData>("RedDot/RedDotActionEffect");
            _redDotActions = new Dictionary<string, List<int>>();
            foreach (var i in db.Effectors)
            {
                if (_redDotActions.ContainsKey(i.ActionName))
                {
                    _redDotActions[i.ActionName] = i.EffectRedDotId;
                    Debug.LogError("重复的id"+i.ActionName); 
                }
                else
                {
                    _redDotActions.Add(i.ActionName, i.EffectRedDotId);
                }
                addLog += $"添加事件：【{i.ActionName}】[{i.EffectRedDotId.Count}]";
            }

            Debug.Log(addLog);
        }

        private static RedDotTriggerBase GetTrigger(RedDotTriggerType type)
        {
            switch (type)
            {
                case RedDotTriggerType.Temp:
                    return new RedDotTrigger_Temp();  
                case RedDotTriggerType.Test111:
                    return new RedDotTrigger_111();  
                case RedDotTriggerType.Test112:
                    return new RedDotTrigger_112();  
                case RedDotTriggerType.Test121:
                    return new RedDotTrigger_121();  
                default:
                    return null;
            }
        }


        //TEMP输出
        private static string TowerReaderLog = "";
        public static void TempHandleRedDotData(RedDotNode data, int indentLevel = 0, RedDotNode father = null)
        {
            //递归
            TowerReaderLog += $"[{data.Data.Id}]  \n";
            if (data.Children != null && data.Children.Count > 0)
            {
                TowerReaderLog += $"[{data.Data.Id}]==>";
                foreach (var child in data.Children)
                {
                    TempHandleRedDotData(child, indentLevel + 1, father);
                }
            }
        }

    }
}
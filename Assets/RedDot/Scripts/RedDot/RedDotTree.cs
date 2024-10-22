using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "RedDotDataBase", menuName = "创建红点数据")]
    public class RedDotTree : ScriptableObject
    {
        public List<RedDotData> trees;

    }
}
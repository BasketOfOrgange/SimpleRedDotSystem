using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "RedDotDataBase", menuName = "�����������")]
    public class RedDotTree : ScriptableObject
    {
        public List<RedDotData> trees;

    }
}
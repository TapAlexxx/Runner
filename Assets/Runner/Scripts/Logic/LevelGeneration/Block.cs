using System;
using UnityEngine;

namespace Scripts.Logic.LevelGeneration
{

    [Serializable]
    public class Block
    {
        public BlockType BlockType;
        public GameObject Prefab;
    }

}
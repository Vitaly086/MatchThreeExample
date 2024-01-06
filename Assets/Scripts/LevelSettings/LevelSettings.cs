using System;
using UnityEngine;

namespace LevelSettings
{
    [Serializable]
    public class LevelSettings
    {
        [field: SerializeField]
        public int ItemTypesNumber { get; set; }
    }
}
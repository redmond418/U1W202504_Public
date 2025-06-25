using System;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class IndexWordTag : IWordTag, IEquatable<IndexWordTag>
    {
        [SerializeField] private int index;
        
        public int Index  => index;

        public bool Equals(IndexWordTag other) => Index == other.Index;
    }
}

using System;

namespace U1W
{
    [Serializable]
    public class RareWordTag : IWordTag, IEquatable<RareWordTag>
    {
        public bool Equals(RareWordTag other) => true;
    }
}

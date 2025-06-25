using System;
using Alchemy.Inspector;

namespace U1W
{
    [Serializable, HorizontalGroup]
    public struct ConvertibleCharactersPair
    {
        public string a;
        public string i;

        public bool TryConvert(string origin, out string converted)
        {
            if(origin == a) 
            {
                converted = i;
                return true;
            }
            if(origin == i)
            {
                converted = a;
                return true;
            }
            converted = string.Empty;
            return false;
        }
    }
}

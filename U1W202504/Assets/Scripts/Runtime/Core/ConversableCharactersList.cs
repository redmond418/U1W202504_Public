using UnityEngine;

namespace U1W
{
    [CreateAssetMenu(fileName = "Characters", menuName = "ScriptableObject/Characters List")]
    public class ConversableCharactersList : ScriptableObject
    {
        [SerializeField] private ConvertibleCharactersPair[] pairs;

        public ConvertibleCharactersPair[] Pairs => pairs;

        public bool TryConvert(string origin, out string converted)
        {
            foreach (var pair in pairs)
            {
                if(pair.TryConvert(origin, out converted)) return true;
            }
            converted = null;
            return false;
        }
    }
}

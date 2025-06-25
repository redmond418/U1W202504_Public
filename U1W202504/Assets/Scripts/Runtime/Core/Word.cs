using Alchemy.Inspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace U1W
{
    [CreateAssetMenu(fileName = "Word", menuName = "ScriptableObject/Word")]
    public class Word : ScriptableObject
    {
        #if UNITY_EDITOR
        [ValidateInput(nameof(ValidationSetName), "単語を設定してください")]
        #endif
        [SerializeField, HorizontalGroup("Name")] private string word;
        [SerializeField, HorizontalGroup("Name")] private bool nameChange = true;
        [SerializeField, SerializeReference] private IWordTag[] tags;

        public string WordName => word;
        public IWordTag[] Tags => tags;

        #if UNITY_EDITOR
        private bool ValidationSetName(string name)
        {
            if (word == null || word == "") 
            {
                word = this.name;
                return false;
            }
            if (!nameChange) return true;
            string path = AssetDatabase.GetAssetPath(this);
            AssetDatabase.RenameAsset(path, name);
            return true;
        }
        #endif
    }
}

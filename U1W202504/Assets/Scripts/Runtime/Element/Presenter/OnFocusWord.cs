using R3;
using UnityEngine;

namespace U1W
{
    public class OnFocusWord : MonoBehaviour
    {
        [SerializeField] private Focusable focusable;
        [SerializeField] private WordCharactersViewer wordCharactersViewer;

        private void Reset()
        {
            focusable = GetComponent<Focusable>();
            wordCharactersViewer = GetComponent<WordCharactersViewer>();
        }

        private void Awake()
        {
            focusable.IsFocused
                .Subscribe(wordCharactersViewer.SetVisible)
                .AddTo(this);
            focusable.IsFocused
                .Where(isFocused => isFocused)
                .Subscribe(wordCharactersViewer.Word, (_, word) => KnownWordsHolder.AddWord(word))
                .AddTo(this);
        }
    }
}

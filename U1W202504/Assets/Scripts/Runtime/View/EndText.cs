using UnityEngine;

namespace U1W
{
    [DefaultExecutionOrder(10)]
    public class EndText : MonoBehaviour
    {
        [SerializeField] private WordCharactersViewer wordCharactersViewer;

        private void Awake()
        {
            wordCharactersViewer.SetVisible(true);
            wordCharactersViewer.SetFlippable(1, false);
        }
    }
}

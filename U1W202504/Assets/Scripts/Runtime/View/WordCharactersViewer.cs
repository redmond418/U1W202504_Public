using System.Collections.Generic;
using R3;
using UnityEngine;
using ZeroMessenger;

namespace U1W
{
    public class WordCharactersViewer : MonoBehaviour
    {
        [SerializeField] private Word word;
        [SerializeField] private FlippableCharacter characterPrefab;
        [SerializeField] private ConversableCharactersList convertibleList;
        [SerializeField] private Transform charactersParent;
        [SerializeField] private Vector2 offset;
        [SerializeField] private Vector2 interval;

        private List<FlippableCharacter> characters = new();

        private Subject<int> flipEvent = new();
        public Observable<int> OnFlip => flipEvent;
        private Subject<int> clickEvent = new();
        public Observable<int> OnClick => clickEvent;

        private ReactiveProperty<bool> isClicked = new(false);
        public ReadOnlyReactiveProperty<bool> IsClicked => isClicked;

        public Word Word => word;

        private void Awake()
        {
            flipEvent.AddTo(this);
            clickEvent.AddTo(this);
            isClicked.AddTo(this);

            for (int i = 0; i < word.WordName.Length; i++)
            {
                var instance = Instantiate(characterPrefab, charactersParent);
                characters.Add(instance);
                instance.OnFlip
                    .Subscribe((i, flipEvent), (_, items) => items.flipEvent.OnNext(items.i))
                    .AddTo(this);
                instance.OnClick
                    .Subscribe((i, clickEvent), (_, items) => items.clickEvent.OnNext(items.i))
                    .AddTo(this);

                string defaultCharacter = word.WordName[i].ToString();
                instance.DefaultCharacter = defaultCharacter;
                string flippedCharacter;
                bool isFlippable = convertibleList.TryConvert(defaultCharacter, out flippedCharacter);
                instance.SetFlippable(isFlippable);
                instance.FlippedCharacter = flippedCharacter;
            }
            SetCharactersPosition();

            OnClick
                .Subscribe((isClicked, characters), (_, items) => 
                {
                    MessageBroker<TutorialProgressMessage>.Default.Publish(new(1));
                    items.isClicked.Value = true;
                    foreach (var character in items.characters)
                    {
                        character.SetFlippable(false);
                    }
                })
                .AddTo(this);
        }

        private void OnDisable()
        {
            for (int i = 0; i < word.WordName.Length; i++)
            {
                var instance = characters[i];
                string defaultCharacter = word.WordName[i].ToString();
                string flippedCharacter;
                bool isFlippable = convertibleList.TryConvert(defaultCharacter, out flippedCharacter);
                instance.SetFlippable(isFlippable);
            }
            isClicked.Value = false;
            SetVisible(false);
        }

        private void SetCharactersPosition()
        {
            for (int i = 0; i < characters.Count; i++)
            {
                float fromCenter = i - (characters.Count - 1) / 2f;
                //カメラに近づけてRayの優先度を上げる
                characters[i].transform.localPosition = (Vector3)(offset + interval * fromCenter) + Vector3.back;
            }
        }

        public void SetVisible(bool visible)
        {
            if(isClicked.Value) visible = true;
            for (int i = 0; i < characters.Count; i++)
            {
                characters[i].SetVisible(visible);
            }
        }

        public void SetFlippable(int index, bool flippable)
        {
            characters[index].SetFlippable(flippable);
        }
    }
}

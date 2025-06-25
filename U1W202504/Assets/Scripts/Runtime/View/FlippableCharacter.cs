using System;
using System.Threading;
using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Animation;
using LitMotion.Extensions;
using R3;
using R3.Triggers;
using TMPro;
using UnityEngine;

namespace U1W
{
    public class FlippableCharacter : MonoBehaviour
    {
        [SerializeField] private SerializableReactiveProperty<bool> isFlippable = new(true);
        [SerializeField] private string defaultCharacter;
        [SerializeField] private string flippedCharacter;
        [SerializeField] private ObservableEventTrigger observableEventTrigger;
        [SerializeField] private TMP_Text characterText;
        [SerializeField] private TMP_Text characterOutlineText;
        [SerializeField] private TMP_Text flippedCharacterText;
        [SerializeField] private LitMotionAnimation FlipAnimation;
        [SerializeField] private float flipTime;
        [SerializeField] private Transform flipTransform;
        [SerializeField] private Collider2D raycastCollider2D;
        [Title("Outline")]
        [SerializeField] private string outlinePropertyName;
        [SerializeField] private float outlineEnabledDialate;
        [SerializeField] private float outlineDisabledDialate;
        [SerializeField] private float outlineDuration;
        [SerializeField] private Ease outlineEase;

        private Material characterOutlineMaterial;
        private MotionHandle outlineMotionHandle;

        private ReactiveProperty<bool> isVisible = new(false);

        private ReactiveProperty<bool> isSelected = new(false);
        public ReadOnlyReactiveProperty<bool> IsSelected => isSelected;
        private Subject<Unit> clickEvent = new();
        public Observable<Unit> OnClick => clickEvent;
        private ReactiveProperty<bool> isFlipped = new(false);
        public ReadOnlyReactiveProperty<bool> IsFlipped => isFlipped;
        private Subject<Unit> flipEvent = new();
        public Observable<Unit> OnFlip => flipEvent;

        public string DefaultCharacter
        {
            get => defaultCharacter;
            set
            {
                characterText.text = value;
                characterOutlineText.text = value;
                defaultCharacter = value;
            }
        }

        public string FlippedCharacter
        {
            get => flippedCharacter;
            set
            {
                flippedCharacterText.text = value;
                flippedCharacter = value;
            }
        }

        private void Awake()
        {
            isVisible.AddTo(this);
            isSelected.AddTo(this);
            isFlipped.AddTo(this);
            clickEvent.AddTo(this);
            flipEvent.AddTo(this);

            characterText.text = defaultCharacter;
            characterOutlineText.text = defaultCharacter;
            flippedCharacterText.text = flippedCharacter;

            ApplyVisible();
            Observable.Merge(isVisible, isFlippable)
                .Subscribe(_ => ApplyVisible()).AddTo(this);
            

            characterOutlineMaterial = characterOutlineText.fontMaterial;
            characterOutlineMaterial.SetFloat(outlinePropertyName, outlineDisabledDialate);
            observableEventTrigger.OnMouseEnterAsObservable()
                .Where((isFlippable, isVisible), (_, items) => items.isFlippable.Value && items.isVisible.Value)
                .Subscribe(isSelected, (_, isSelected) => isSelected.Value = true)
                .AddTo(this);
            observableEventTrigger.OnMouseExitAsObservable()
                .Where(isFlippable, (_, isFlippable) => isFlippable.Value)
                .Subscribe(isSelected, (_, isSelected) => isSelected.Value = false)
                .AddTo(this);
            
            isSelected
                .Where(isFlippable, (_, isFlippable) => isFlippable.Value)
                .Subscribe(PlaySelectedAnimation)
                .AddTo(this);
            observableEventTrigger.OnMouseDownAsObservable()
                .Where(_ => UncontrollableManager.IsControllable)
                .Where((isFlippable, isVisible), (_, items) => items.isFlippable.Value && items.isVisible.Value)
                .Where(isFlippable, (_, isFlippable) => isFlippable.Value)
                .SubscribeAwait(async (_, cancellationToken) => await PlayFlipAnimation(cancellationToken))
                .AddTo(this);
        }

        void OnDisable()
        {
            if(FlipAnimation.IsActive) FlipAnimation.Stop();
            flipTransform.localRotation = Quaternion.identity;
            isFlipped.Value = false;
            isVisible.Value = false;
            characterOutlineMaterial.SetFloat(outlinePropertyName, outlineDisabledDialate);
        }

        private void PlaySelectedAnimation(bool isSelected)
        {
            if(outlineMotionHandle.IsPlaying()) outlineMotionHandle.Cancel();
            float dialateStart = characterOutlineMaterial.GetFloat(outlinePropertyName);
            float dialateEnd = isSelected ? outlineEnabledDialate : outlineDisabledDialate;
            outlineMotionHandle = LMotion.Create(dialateStart, dialateEnd, outlineDuration)
                .WithEase(outlineEase)
                .BindToMaterialFloat(characterOutlineMaterial, outlinePropertyName)
                .AddTo(this);
        }

        private async UniTask PlayFlipAnimation(CancellationToken cancellationToken)
        {
            clickEvent.OnNext(default);
            FlipAnimation.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(flipTime), false, PlayerLoopTiming.Update, cancellationToken);
            flipEvent.OnNext(default);
            isFlipped.Value = true;

            ApplyVisible();
        }

        public void SetVisible(bool visible)
        {
            isVisible.Value = visible;
        }

        private void ApplyVisible()
        {
            characterText.enabled = isVisible.Value && !isFlipped.Value;
            characterOutlineText.enabled = isVisible.Value && !isFlipped.Value;
            flippedCharacterText.enabled = isVisible.Value && isFlipped.Value;
            raycastCollider2D.enabled = isVisible.Value && isFlippable.Value;
        }

        public void SetFlippable(bool flippable) => isFlippable.Value = flippable;
    }
}

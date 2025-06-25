using R3;
using R3.Triggers;
using UnityEngine;
using ZeroMessenger;

namespace U1W
{
    public class Focusable : MonoBehaviour
    {
        [SerializeField] private ObservableEventTrigger observableEventTrigger;

        private ReactiveProperty<bool> isFocused = new();
        public ReadOnlyReactiveProperty<bool> IsFocused => isFocused;

        private void Awake()
        {
            observableEventTrigger.OnMouseEnterAsObservable()
                .Subscribe(_ => SetFocus(true))
                .AddTo(this);
        }

        public void SetFocus(bool isFocused)
        {
            if(this.isFocused.Value == isFocused) return;
            if(isFocused && !UncontrollableManager.IsControllable) return;
            if(isFocused) MessageBroker<TutorialProgressMessage>.Default.Publish(new(0));
            this.isFocused.Value = isFocused;
        }

        void OnDisable()
        {
            SetFocus(false);
        }
    }
}

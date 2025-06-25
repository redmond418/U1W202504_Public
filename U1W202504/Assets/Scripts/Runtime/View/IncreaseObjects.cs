using R3;
using UnityEngine;

namespace U1W
{
    public class IncreaseObjects : MonoBehaviour
    {
        [SerializeField] private GameObject[] gameObjects;

        private ReactiveProperty<int> activeCount = new(0);
        public ReadOnlyReactiveProperty<int> ActiveCount => activeCount;

        public void Increase()
        {
            activeCount.Value++;
            if(activeCount.Value > gameObjects.Length) return;
            int index = 0;
            while(index < gameObjects.Length && gameObjects[index].activeSelf) index++;
            gameObjects[index].SetActive(true);
        }

        public void Increase(int index)
        {
            if(index < 0 || index >= gameObjects.Length) return;
            if(gameObjects[index].activeSelf) 
            {
                Increase();
                return;
            }
            activeCount.Value++;
            if(activeCount.Value > gameObjects.Length) return;
            gameObjects[index].SetActive(true);
        }
        public void Decrease()
        {
            if(activeCount.Value <= 0) return;
            activeCount.Value--;
            if(activeCount.Value >= gameObjects.Length) return;
            int index = gameObjects.Length - 1;
            while(index >= 0 && !gameObjects[index].activeSelf) index--;
            gameObjects[index].SetActive(false);
        }

        public void Decrease(int index)
        {
            if(index < 0 || index >= gameObjects.Length) return;
            if(!gameObjects[index].activeSelf) 
            {
                Decrease();
                return;
            }
            activeCount.Value--;
            if(activeCount.Value >= gameObjects.Length) return;
            gameObjects[index].SetActive(false);
        }
    }
}

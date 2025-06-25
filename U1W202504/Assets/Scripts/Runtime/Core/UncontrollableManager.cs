using System.Linq;
using ObservableCollections;
using UnityEngine;

namespace U1W
{
    public class UncontrollableManager : MonoBehaviour
    {
        private static UncontrollableManager instance;
        private static UncontrollableManager Instance
        {
            get
            {
                if(instance == null) 
                {
                    instance = FindAnyObjectByType<UncontrollableManager>();
                    instance.Initialize();
                }
                return instance;
            }
        }
        
        private bool isInitialized;

        private ObservableList<UncontrollableOrder> orders;

        private void Awake()
        {
            if(instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Initialize()
        {
            if(isInitialized) return;
            isInitialized = true;
            orders = new();
        }

        public static bool IsControllable
        {
            get
            {
                if(Instance == null) return true;
                return Instance.orders.Count <= 0;
            }
        }

        public static void AddOrder(UncontrollableOrder uncontrollableOrder)
        {
            if(Instance == null) return;
            Instance.orders.Add(uncontrollableOrder);
            print(Instance.orders.Count);
        }

        public static void RemoveOrder(UncontrollableOrder uncontrollableOrder)
        {
            if(Instance == null) return;
            if(!Instance.orders.Contains(uncontrollableOrder)) return;
            Instance.orders.Remove(uncontrollableOrder);
            print(Instance.orders.Count);
        }
    }

    public class UncontrollableOrder
    {
        //参照さえあればいいので、特に情報は持たない
    }
}
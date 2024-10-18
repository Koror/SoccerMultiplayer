using System;
using Ball.Data;
using Ball.Views;
using Mirror;
using UnityEngine;
using Zenject;

namespace Ball.Pool
{
    public class BallPool : MonoBehaviour
    {
        [SerializeField] private BallView _prefab;
        [SerializeField] private int _startCapacity;
        [SerializeField] private int _maxCapacity;
        [SerializeField] private float _minBulletLife;
        [SerializeField] private float _maxBulletLife;
        
        private Pool<GameObject> _pool;
        private int _currentCount;
        private int _pooledCount;

        private DiContainer _diContainer;
        
        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        private void Start()
        {
            InitializePool();
            NetworkClient.RegisterPrefab(_prefab.gameObject, SpawnHandler, UnspawnHandler);
        }

        public GameObject SpawnHandler(SpawnMessage msg) => Get(msg.position, msg.rotation);
        public void UnspawnHandler(GameObject spawned) => Return(spawned);
        
        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            _pooledCount++;
            var prefab = _pool.Get();
            prefab.transform.position = position;
            prefab.transform.rotation = rotation;
            prefab.gameObject.SetActive(true);
            if (prefab.TryGetComponent(out BallView view))
            {
                var life = Mathf.Lerp(_maxBulletLife, _minBulletLife,(float)_pooledCount/(float)_maxCapacity);
                view.SetLife(life);
            }
            return prefab;
        }

        public void Return(GameObject spawned)
        {
            _pooledCount--;
            spawned.gameObject.SetActive(false);
            _pool.Return(spawned);
        }
        
        private void InitializePool()
        {
            _pool = new Pool<GameObject>(CreateNew, _startCapacity);
        }
        
        private GameObject CreateNew()
        {
            var view = _diContainer.InstantiatePrefabForComponent<BallView>(_prefab, transform).gameObject;
            view.name = $"{_prefab.name}_pooled_{_currentCount}";
            view.SetActive(false);
            _currentCount++;
            return view;
        }
        
        private void OnDestroy()
        {
            NetworkClient.UnregisterPrefab(_prefab.gameObject);
        }
    }
}
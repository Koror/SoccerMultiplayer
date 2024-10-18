using System;
using Ball.Data;
using Ball.Pool;
using Mirror;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ball.Views
{
    public class BallView : NetworkBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _renderer;

        public int PlayerId => _playerId;

        private int _playerId;
        private float _life;
        private BallPool _ballPool;
        private IDisposable _timerStream;
        
        [SyncVar(hook = nameof(SetColor))]
        private Color _color;
        
        [Inject]
        private void Construct(BallPool ballPool)
        {
            _ballPool = ballPool;
        }
        
        public void Initialize(BallData data)
        {
            _rigidbody.AddForce(transform.forward * data.Force,ForceMode.Impulse);
            _playerId = data.PlayerId;
            _color = data.Color;
            
            _timerStream?.Dispose();
            _timerStream = Observable.Timer(TimeSpan.FromSeconds(_life)).Subscribe(x => DestroySelf());
        }

        [Server]
        public void DestroySelf()
        {
            _timerStream?.Dispose();
            NetworkServer.UnSpawn(gameObject);
            _ballPool.Return(gameObject);
        }

        [Server]
        public void SetLife(float life)
        {
            _life = life;
        }
        
        private void SetColor(Color oldColor, Color newColor)
        {
            _renderer.material.color = newColor;
        }

        private void OnDestroy()
        {
            _timerStream?.Dispose();
        }
    }
}
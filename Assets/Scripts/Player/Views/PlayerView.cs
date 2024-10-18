using System;
using Ball.Data;
using Ball.Pool;
using Ball.Views;
using Mirror;
using Player.Storage;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player.Views
{
    public class PlayerView : NetworkBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private MeshRenderer _rendererHead;
        
        public string Name => _name;
        public ReactiveProperty<int> ScoreChanged = new();
        
        private BallPool _ballPool;
        private int _playerId;
        private float _minForce = 5f;
        private float _maxForce = 80f;
        private float _charge;
        private float _maxCharge=3f;
        
        [SyncVar(hook = nameof(SetColor))]
        private Color _color;
        [SyncVar]
        private string _name;
        [SyncVar(hook = nameof(OnAddScore))]
        private int _score;

        //only server injection
        [Inject]
        private void Construct(BallPool ballPool)
        {
            _ballPool = ballPool;
        }

        public void Initialize(Color color, string name, int playerID)
        {
            _name = name;
            _playerId = playerID;
            _color = color;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            PlayerStorage.Instance.Add(netId,this);
        }

        private void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }
            
            if (_head.localRotation.eulerAngles.x > 70 && _head.localRotation.eulerAngles.x < 150)
            {
                var rotation = _head.localRotation.eulerAngles;
                rotation.x = 70f;
                rotation.y = 0f;
                rotation.z = 0f;
                _head.localRotation = Quaternion.Euler(rotation);
            }
            if (_head.localRotation.eulerAngles.x < 290 && _head.localRotation.eulerAngles.x > 150)
            {
                var rotation = _head.localRotation.eulerAngles;
                rotation.x = 290;
                rotation.y = 0f;
                rotation.z = 0f;
                _head.localRotation = Quaternion.Euler(rotation);
            }
            
            _root.Rotate(Vector3.up,Mouse.current.delta.x.value * _rotationSpeed * Time.deltaTime);
            _head.Rotate(-Vector3.right,Mouse.current.delta.y.value * _rotationSpeed * Time.deltaTime);
            

            if (Input.GetMouseButton(0))
            {
                _charge += Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0))
            {
                var force = Mathf.Lerp(_minForce, _maxForce, _charge / _maxCharge);
                CmdFireBall(_shootPoint.position,_shootPoint.forward,force);
                _charge = 0;
            }
        }

        [Command]
        private void CmdFireBall(Vector3 position, Vector3 direction, float force)
        {
            var ballData = new BallData(force,_playerId,_color);
            var rotation = Quaternion.LookRotation(direction);
            var ball = _ballPool.Get(position, rotation);
            var view = ball.GetComponent<BallView>();
            view.Initialize(ballData);
            NetworkServer.Spawn(ball);
        }

        [Server]
        public void AddScore(int score)
        {
            _score += score;
        }

        public void OnAddScore(int oldScore, int newScore)
        {
            Debug.Log($"PlayerID: {_playerId} score: {newScore}");
            ScoreChanged.Value = newScore;
        }

        private void SetColor(Color oldColor, Color newColor)
        {
            _renderer.material.color = newColor;
            _rendererHead.material.color = newColor;
        }
    }
}
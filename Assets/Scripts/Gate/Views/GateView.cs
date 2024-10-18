using Ball.Views;
using Mirror;
using UnityEngine;
using System;

namespace Gate.Views
{
    public class GateView : NetworkBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;
        
        public Action<int,int> Goal;
        
        private float _distance = 20; 
        private float _speed = 5;

        private int _playerId;
        private int _direction;
        //for opposed positions gates
        private int _invertDirection;
        private Vector3 _startPosition;
        
        [SyncVar(hook = nameof(SetColor))]
        private Color _color;

        public void Initialize(int playerId, Color color, float speed, float distance)
        {
            _playerId = playerId;
            color.a = 0.4f;
            _direction = 1;
            _color = color;
            _speed = speed;
            _distance = distance;
        }

        private void Start()
        {
            _startPosition = transform.position;
            var directionRight = transform.right;
            _invertDirection = (int)(directionRight.x + directionRight.y + directionRight.z);
        }

        [Server]
        private void Update()
        {
                transform.position += transform.right * _direction *_invertDirection * _speed * Time.deltaTime;

                var diff = transform.position - _startPosition;
                var dist = diff.x + diff.y + diff.z;
                if (_direction == 1 && dist >= _distance)
                {
                    _direction = -1;
                }
                else if(_direction == -1 && dist <= -_distance)
                {
                    _direction = 1;
                }
        }
        
        [ServerCallback]
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                var ball = other.GetComponent<BallView>();
                var ballPlayerId = ball.PlayerId;
                Goal?.Invoke(ballPlayerId, _playerId);
                ball.DestroySelf();
            }
        }
        
        private void SetColor(Color oldColor, Color newColor)
        {
            _renderer.material.color = newColor;
        }
    }
}
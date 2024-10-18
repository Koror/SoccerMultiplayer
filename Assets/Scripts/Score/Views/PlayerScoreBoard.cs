using UnityEngine;

namespace Score.Views
{
    public class PlayerScoreBoard : MonoBehaviour
    {
        [SerializeField] private Transform _scoreRoot;

        public Transform ScoreRoot => _scoreRoot;
    }
}
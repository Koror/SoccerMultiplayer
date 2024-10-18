using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Score.Views
{
    public class PlayerScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private int _score;
        private string _name;
        
        public void Initialize(ReactiveProperty<int> scoreObservable, string name)
        {
            _name = name;
            scoreObservable.Subscribe(UpdateScore);
            UpdateScore(scoreObservable.Value);
        }
        
        public void UpdateScore(int score)
        {
            _score = score;
            _text.text = $"{_name}: {_score}";
        }
    }
}
using Score.Views;
using UnityEngine;

namespace Score.Configs
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "Configs/ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {
        public PlayerScoreView Prefab;
        public PlayerScoreBoard PlayerScoreBoardPrefab;
    }
}
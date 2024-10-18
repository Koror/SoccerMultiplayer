using Gate.Views;
using UnityEngine;

namespace Gate.Config
{
    [CreateAssetMenu(fileName = "GateConfig", menuName = "Configs/GateConfig")]
    public class GateConfig : ScriptableObject
    {
        public GateView Prefab;
        public float Distance = 20; 
        public float Speed = 5;
    }
}
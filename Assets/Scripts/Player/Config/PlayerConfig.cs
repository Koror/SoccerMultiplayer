using Player.Views;
using UnityEngine;

namespace Player.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public PlayerView Prefab;
    }
}
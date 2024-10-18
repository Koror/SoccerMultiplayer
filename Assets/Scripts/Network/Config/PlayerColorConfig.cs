using System;
using System.Collections.Generic;
using System.Linq;
using Network.Enums;
using UnityEngine;

namespace Network.Config
{
    [CreateAssetMenu(fileName = "PlayerColorConfig", menuName = "Configs/PlayerColorConfig")]
    public class PlayerColorConfig : ScriptableObject
    {
        public List<PlayerColorData> Colors;

        public Color GetColorByType(CharacterColorType type)
        {
            return Colors.FirstOrDefault(x => x.ColorType == type)!.Color;
        }
    }

    [Serializable]
    public class PlayerColorData
    {
        public CharacterColorType ColorType;
        public Color Color;
    }
}
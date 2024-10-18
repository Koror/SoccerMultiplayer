using UnityEngine;

namespace Ball.Data
{
    public class BallData
    {
        public float Force;
        public int PlayerId;
        public Color Color;

        public BallData(float force, int playerId, Color color)
        {
            Force = force;
            PlayerId = playerId;
            Color = color;
        }
    }
}
using UnityEngine;

namespace Scripts.Logic.LevelGeneration
{

    public class Direction
    {
        public Vector3 Vector;
        public Vector3 Rotation;

        public Direction(Vector3 vector, Vector3 rotation)
        {
            Vector = vector;
            Rotation = rotation;
        }
    }

}
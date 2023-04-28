using System;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.Game
{
    public class GameFactory : IGameFactory
    {
        public void CreatePlayer()
        {
            Debug.Log("player");
        }

        public void Clear()
        {
        }
    }
}
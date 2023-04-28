using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.Game
{
    public interface IGameFactory
    {
        void CreatePlayer();
        void Clear();
        GameObject Player { get; }
    }
}
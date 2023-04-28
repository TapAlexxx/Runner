using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.Game
{
    public interface IGameFactory
    {
        GameObject Player { get; }
        GameObject Hud { get; }
        void CreatePlayer(Transform spawnPointTransform);
        void Clear();
        GameObject CreateHud();
    }
}
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.Game
{
    public interface IGameFactory
    {
        GameObject Player { get; }
        GameObject Hud { get; }
        void CreatePlayer();
        void Clear();
        GameObject CreateHud();
    }
}
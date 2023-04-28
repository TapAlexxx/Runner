using Scripts.Logic.CameraControl;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.Game
{
    public interface IGameFactory
    {
        GameObject Player { get; }
        GameObject Hud { get; }
        CameraStateChanger CameraStateChanger { get; }
        void CreatePlayer(Transform spawnPointTransform);
        void Clear();
        GameObject CreateHud();
        CameraStateChanger CreateCamera();
    }
}
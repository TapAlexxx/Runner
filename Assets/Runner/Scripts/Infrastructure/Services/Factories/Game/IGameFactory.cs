using Scripts.Logic.CameraControl;
using Scripts.Logic.LevelGeneration;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.Game
{
    public interface IGameFactory
    {
        GameObject Player { get; }
        GameObject Hud { get; }
        CameraStateChanger CameraStateChanger { get; }
        LevelGenerator LevelGenerator { get; }
        void CreatePlayer(Transform spawnPointTransform);
        void Clear();
        GameObject CreateHud();
        CameraStateChanger CreateCamera();
        LevelGenerator CreateLevelGenerator();
    }
}
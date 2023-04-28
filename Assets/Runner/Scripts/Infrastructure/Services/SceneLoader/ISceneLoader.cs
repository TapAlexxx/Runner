using System;

namespace Scripts.Infrastructure.Services.SceneLoader
{

    public interface ISceneLoader
    {
        void Load(string name, Action onLevelLoad);
    }

}
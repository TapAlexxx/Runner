using UnityEngine;

namespace Scripts.Infrastructure.Services.InstantiatorService
{

    public interface IInstantiator
    {
        Transform CreateUiRoot(string uiRootPath);
        GameObject InstantiatePrefab(GameObject prefab, Transform parent);
        GameObject InstantiateFromPath(string path);
    }

}
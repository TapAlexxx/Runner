using UnityEngine;

namespace Scripts.Infrastructure.Services.InstantiatorService
{

    public interface IInstantiator
    {
        Transform InstantiateFromPath(string uiRootPath);
        GameObject InstantiatePrefab(GameObject prefab, Transform parent);
    }

}
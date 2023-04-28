using UnityEngine;

namespace Scripts.Infrastructure.Services.InstantiatorService
{

    public class Instantiator : MonoBehaviour, IInstantiator
    {
        public Transform InstantiateFromPath(string uiRootPath)
        {
            GameObject prefab = Resources.Load(uiRootPath) as GameObject;
            GameObject instantiate = Instantiate(prefab);
            return instantiate.transform;
        }

        public GameObject InstantiatePrefab(GameObject prefab, Transform parent)
        {
            GameObject instantiate = Instantiate(prefab, parent);
            return instantiate;
        }
    }

}
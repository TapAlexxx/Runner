using UnityEngine;

namespace Scripts.Infrastructure.Services.InstantiatorService
{

    public class Instantiator : MonoBehaviour, IInstantiator
    {
        public Transform CreateUiRoot(string uiRootPath)
        {
            GameObject prefab = Resources.Load(uiRootPath) as GameObject;
            GameObject instantiate = Instantiate(prefab);
            return instantiate.transform;
        }

        public GameObject InstantiateFromPath(string path)
        {
            GameObject prefab = Resources.Load(path) as GameObject;
            GameObject instantiate = Instantiate(prefab);
            return instantiate;
        }

        public GameObject InstantiatePrefab(GameObject prefab, Transform parent)
        {
            GameObject instantiate = Instantiate(prefab, parent);
            return instantiate;
        }
    }

}
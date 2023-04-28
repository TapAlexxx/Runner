using Scripts.Infrastructure.Services.StaticData;
using Scripts.Infrastructure.Services.Window;
using Scripts.StaticData.Window;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private const string UiRootPath = "UI/UiRoot";

        private readonly IInstantiator _instantiator;
        private readonly IStaticDataService _staticData;

        private Transform _uiRoot;

        public UIFactory(IStaticDataService staticDataService, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _staticData = staticDataService;
        }

        public void CreateUiRoot()
        {
            _uiRoot = _instantiator.InstantiateOnActiveScene(UiRootPath).transform;
        }

        public RectTransform CrateWindow(WindowTypeId windowTypeId)
        {
            WindowConfig config = _staticData.ForWindow(windowTypeId);
            GameObject window = _instantiator.InstantiatePrefab(config.Prefab, _uiRoot);
            return window.GetComponent<RectTransform>();
        }
    }

    public interface IInstantiator
    {
        Transform InstantiateOnActiveScene(string uiRootPath);
        GameObject InstantiatePrefab(GameObject configPrefab, Transform uiRoot);
    }

    public class Instantiator : MonoBehaviour, IInstantiator
    {
        public Transform InstantiateOnActiveScene(string uiRootPath)
        {
            GameObject prefab = Resources.Load(uiRootPath) as GameObject;
            GameObject instantiate = Instantiate(prefab);
            return instantiate.transform;
        }

        public GameObject InstantiatePrefab(GameObject configPrefab, Transform uiRoot)
        {
            GameObject instantiate = Instantiate(configPrefab, uiRoot);
            return instantiate;
        }
    }
}
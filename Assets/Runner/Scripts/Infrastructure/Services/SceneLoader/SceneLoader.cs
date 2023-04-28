using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Infrastructure.Services.SceneLoader
{

    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        public void Load(string name, Action onLevelLoad)
        {
            StartCoroutine(LoadLevel(name, onLevelLoad));
        }

        private IEnumerator LoadLevel(string name, Action onLevelLoad)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);
            while (!waitNextScene.isDone)
                yield return null;

            onLevelLoad?.Invoke();
        }
    }

}
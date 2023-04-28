using UnityEngine;

namespace Scripts.Infrastructure.Bootsrapper
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper gameBootstrapper;
        
        private void Awake()
        {
            if (FindObjectOfType<GameBootstrapper>())
            {
                Destroy(gameObject);
                return;
            }

            GameBootstrapper bootstrapper = Instantiate(gameBootstrapper);
            bootstrapper.Initialize();
        }
    }
}
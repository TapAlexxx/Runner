using Scripts.Infrastructure.Services.Window;
using Scripts.Logic.LevelGeneration.Blocks;
using Scripts.Logic.PlayerControl.HealthControl;
using Scripts.Logic.PlayerControl.StateControl;
using UnityEngine;

namespace Scripts.Logic.PlayerControl
{

    public class WinLoseControl : MonoBehaviour
    {
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerStateControl playerStateControl;
        
        private IWindowService _windowService;
        private bool _opened;

        private void OnValidate()
        {
            playerHealth = GetComponentInChildren<PlayerHealth>();
            playerStateControl = GetComponentInChildren<PlayerStateControl>();
        }

        public void Initialize(IWindowService windowService)
        {
            _windowService = windowService;
            _opened = false;
        }

        private void Start()
        {
            playerHealth.Dead += OpenLosePanel;
            playerHealth.Revived += TryClosePanel;
        }

        private void OnDestroy()
        {
            playerHealth.Dead -= OpenLosePanel;
            playerHealth.Revived -= TryClosePanel;
        }

        private void OpenLosePanel()
        {
            if(_opened)
                return;
            _windowService.Open(WindowTypeId.Lose);
            _opened = true;
        }

        private void TryClosePanel()
        {
            _windowService.TryCloseLastOpened();
            _opened = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_opened)
                return;
            
            if (other.TryGetComponent(out FinishBlock finishBlock))
            {
                playerStateControl.EnterWaitState();
                _windowService.Open(WindowTypeId.Finish);
                _opened = true;
            }
        }
    }

}
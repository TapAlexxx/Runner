using Scripts.Logic.PlayerControl.HealthControl;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Logic.Hud
{

    public class ReviveButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        private PlayerHealth _playerHealth;

        private void OnValidate()
        {
            button = GetComponentInChildren<Button>();
        }
        
        public void Initialize(GameObject player)
        {
            _playerHealth = player.GetComponent<PlayerHealth>();
        }
        
        private void Start()
        {
            button.onClick.AddListener(RevivePlayer);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(RevivePlayer);
        }

        private void RevivePlayer()
        {
            _playerHealth.Revive();
        }
    }

}
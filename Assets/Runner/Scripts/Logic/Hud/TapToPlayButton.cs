using DG.Tweening;
using Scripts.Logic.PlayerControl.StateControl;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Logic.Hud
{
    public class TapToPlayButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text text;
        
        private PlayerStateControl _stateControl;

        public void Initialize(GameObject player)
        {
            _stateControl = player.GetComponent<PlayerStateControl>();
        }

        private void Start()
        {
            button.onClick.AddListener(EnterRideState);
            text.DOFade(1f, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy() => 
            button.onClick.RemoveListener(EnterRideState);

        private void EnterRideState()
        {
            text.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
            _stateControl.EnterRunState();
        }
    }

}
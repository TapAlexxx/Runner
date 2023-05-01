using System.Collections.Generic;
using System.Linq;
using Scripts.Logic.PlayerControl.BlockControl;
using UnityEngine;

namespace Scripts.Logic.Hud
{

    public class PlayerShieldView : MonoBehaviour
    {
        [SerializeField] private List<GameObject> images;
        private DamageBlockObserver _blockObserver;

        public void Initialize(GameObject player)
        {
            _blockObserver = player.GetComponent<DamageBlockObserver>();
            _blockObserver.ShieldCountChanged += RefreshView;
        }

        private void Start()
        {
            RefreshView();
        }

        private void OnDestroy()
        {
            _blockObserver.ShieldCountChanged -= RefreshView;            
        }

        private void RefreshView()
        {
            ResetView();
            ActivateImages();
        }

        private void ResetView()
        {
            foreach (GameObject image in images)
                image.SetActive(false);
        }

        private void ActivateImages()
        {
            for (int i = 0; i < _blockObserver.ShieldCount; i++)
                images.FirstOrDefault(x => x.activeSelf == false)?.SetActive(true);
        }
    }

}
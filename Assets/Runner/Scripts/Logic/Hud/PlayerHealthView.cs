using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Logic.PlayerControl.HealthControl;
using UnityEngine;

namespace Scripts.Logic.Hud
{

    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private List<GameObject> images;
        private PlayerHealth _playerHealth;

        public void Initialize(GameObject player)
        {
            _playerHealth = player.GetComponent<PlayerHealth>();
            _playerHealth.HealthChanged += RefreshView;
        }

        private void Start()
        {
            RefreshView();
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= RefreshView;            
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
            for (int i = 0; i < _playerHealth.CurrentHealth; i++)
                images.FirstOrDefault(x => x.activeSelf == false)?.SetActive(true);
        }
    }

}
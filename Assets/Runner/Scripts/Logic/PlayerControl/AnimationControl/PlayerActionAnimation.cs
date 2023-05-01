using System;
using DG.Tweening;
using Scripts.Logic.PlayerControl.BoosterControl;
using Scripts.Logic.PlayerControl.HealthControl;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.AnimationControl
{

    public class PlayerActionAnimation : MonoBehaviour
    {
        [SerializeField] private BoosterCollector boosterCollector;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private Renderer renderer;
        
        private Vector3 _upScaled;
        private Vector3 _lowScaled;

        private void OnValidate()
        {
            boosterCollector = GetComponentInChildren<BoosterCollector>();
            playerHealth = GetComponentInChildren<PlayerHealth>();
        }

        private void Start()
        {
            _upScaled = Vector3.one * 1.2f;
            _lowScaled = Vector3.one * 0.8f;
            
            boosterCollector.SpeedCollected += AnimateSpeedCollect;
            boosterCollector.ShieldCollected += AnimateShieldCollect;
            boosterCollector.HealCollected += AnimateHealCollect;
            playerHealth.DamageApplied += AnimateDamageApply;
        }

        private void OnDestroy()
        {
            boosterCollector.SpeedCollected -= AnimateSpeedCollect;
            boosterCollector.ShieldCollected -= AnimateShieldCollect;
            boosterCollector.HealCollected -= AnimateHealCollect;
            playerHealth.DamageApplied -= AnimateDamageApply;
        }

        private void AnimateDamageApply()
        {
            AnimateAction(Color.red, _lowScaled);
        }

        private void AnimateHealCollect(int obj)
        {
            AnimateAction(Color.green, _upScaled);
        }

        private void AnimateShieldCollect(int obj)
        {
            AnimateAction(Color.cyan, _upScaled);
        }

        private void AnimateSpeedCollect(int obj)
        {
            AnimateAction(Color.yellow, _upScaled);            
        }

        private void AnimateAction(Color color, Vector3 scale)
        {
            renderer.materials[0].DOColor(color, 0.1f).SetLoops(4, LoopType.Yoyo);
            transform.DOScale(scale, 0.1f).SetLoops(4, LoopType.Yoyo);
        }
    }

}
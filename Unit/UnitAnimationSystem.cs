using System;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(Animator))]
    public class UnitAnimationSystem : MonoBehaviour
    {
        [Tooltip("NOT READY, dont use")]
        [SerializeField] private bool useCustomAnimator;
        private Animator _animator;
        private States _currentState;
        private UnitCustomAnimations _customAnimations;
        public enum States
        {
            None,
            Idle,
            Hit,
            Death
        }
        private void Awake()
        {
            if (useCustomAnimator)
            {
                _customAnimations = new UnitCustomAnimations();
                _customAnimations.GetDefaultLocals(Vector3.zero, new Vector3(0,0.43f,0),Vector3.zero);
            }
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (useCustomAnimator)
            {
                _customAnimations.GetUnit(transform);
            }
            SetAnimationState(States.Idle);
        }

        internal void SetAnimationState(States animationState)
        {
            var animInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (animInfo.loop && _currentState == animationState) return;
            if(useCustomAnimator)
                _customAnimations.PlayAnimation(animationState);
            else
                _animator.Play(animationState.ToString());
            _currentState = animationState;
        }
    }
}

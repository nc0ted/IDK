using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(Animator))]
    public class UnitAnimationSystem : MonoBehaviour
    {
        [Tooltip("This is more ready, use this")]
        [SerializeField] private bool useCustomAnimator;

        
        private Animator _animator;
        private States _currentState;
        private UnitInitializer _unitInitializer;
        private UnitCustomAnimations _unitCustomAnimations;
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
                _unitInitializer = new UnitInitializer();
                _unitInitializer.GetDefaultLocals(new Vector3(10.4f,9.6f,0), new Vector3(0,0.43f,0),Vector3.zero);
            }
            _unitCustomAnimations = _unitInitializer._unitCustomAnimations;
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (useCustomAnimator)
            {
                _unitInitializer.GetUnit(transform);
            }
            SetAnimationState(States.Idle);
        }

        internal void SetAnimationState(States animationState)
        {
            var animInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (animInfo.loop && _currentState == animationState) return;
            if(useCustomAnimator)
                _unitCustomAnimations.PlayAnimation(animationState);
            else
                _animator.Play(animationState.ToString());
            _currentState = animationState;
        }
    }
}

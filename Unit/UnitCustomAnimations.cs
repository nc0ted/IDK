using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static Unit.UnitAnimationSystem.States;
using Task = System.Threading.Tasks.Task;
using static AnimationsMath;

namespace Unit
{
    public class UnitCustomAnimations
    {
        private Vector3 _bodyDefaultLocal, _headDefaultLocal, _apparelDefaultLocal;
        private Transform _body, _head, _apparel;
        private Light2D _hitLight;
        private int _iteration;
        private static bool stopAnimations;

        internal void GetDefaultLocals(Vector3 body,Vector3 head,Vector3 apparel)
        {
            _bodyDefaultLocal = body;
            _headDefaultLocal = head;
            _apparelDefaultLocal = apparel;
        }
        internal void GetUnit(Transform unit)
        {
            _body = unit;
            _head = _body.Find("Head");
            _apparel = _body.Find("Apparel");
            _body.localPosition = _bodyDefaultLocal;
            _head.localPosition = _headDefaultLocal;
            _apparel.localPosition = _apparel.localPosition;
            _hitLight = _body.GetComponentInChildren<Light2D>();
        }
        
        internal async void PlayAnimation(UnitAnimationSystem.States state)
        {
            stopAnimations = true;
            Debug.Log(state);
            switch (state)
            {
                case Idle:
                    await PlayIdle();
                    break;
                case Hit:
                    await PlayHit();
                    break;
                case Death:
                    await PlayDeath();
                    break;
            }
        }
        private async Task PlayIdle()
        {
            stopAnimations = false;
            while (Application.isPlaying&&!stopAnimations)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (stopAnimations) break;
                    LerpY(false,_head);
                    await Task.Delay(10);
                }
                for (int i = 0; i < 100; i++)
                {
                    if (stopAnimations) break;
                    LerpY(true,_head);
                    await Task.Delay(10);
                }
            }
        }
        private async Task PlayHit()
        {
            stopAnimations = false;
            await EnableHitLight();
        }
        private async Task PlayDeath()
        {
            stopAnimations = false; 
            await EnableHitLight(3000);
            for (int i = 0; i < 100; i++)
            {
                LerpZRotation(false, _body);
                await Task.Delay(1);
            } 
        }
        private async Task EnableHitLight(int delayMs=300)
        {
            _hitLight.enabled = true;
            await Task.Delay(delayMs);
            _hitLight.enabled = false;
        }
    }
}

using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static Unit.UnitAnimationSystem.States;
using Task = System.Threading.Tasks.Task;

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
            Debug.Log(_head);
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
                Debug.Log("DEATH");
            //    if (stopAnimations) break;
                LerpZRotation(false, _body);
                await Task.Delay(1);
            } 
        }
        private static void LerpY(bool down,Transform transform,float speed=0.01f,float vectorPlus=0.05f)
        {
            if (stopAnimations) return;
            var localPosition = transform.localPosition;
            transform.localPosition = !down ? new Vector3(localPosition.x, math.lerp(localPosition.y, localPosition.y - vectorPlus, speed)) : new Vector3(transform.localPosition.x, math.lerp(transform.localPosition.y, transform.localPosition.y + vectorPlus, speed));
        }
        private static void LerpX(bool right,Transform transform,float speed=0.01f,float vectorPlus=0.05f)
        {
            if (stopAnimations) return;
            var localPosition = transform.localPosition;
            transform.localPosition = !right ? new Vector3(math.lerp(localPosition.x, localPosition.x -vectorPlus, speed),localPosition.y) : new Vector3(math.lerp(transform.localPosition.x, transform.localPosition.x + vectorPlus, speed),transform.localPosition.y);
        }
        private static void LerpZRotation(bool right,Transform transform,float endZRotation=90f)
        {
           //if (stopAnimations) return;
           var rotation = transform.rotation;
           transform.eulerAngles = new Vector3(rotation.x, rotation.y, math.lerp(rotation.z, endZRotation, 90f));
           Debug.Log(rotation.z + " ROTATION");

        }

        private void DecrementXPosition(bool right, Transform transform,float speed)
        {
            switch (right)
            {
                case true:
                    transform.position -= new Vector3(speed * Time.deltaTime,0,0);
                    break;
                case false:
                    transform.position += new Vector3(speed * Time.deltaTime,0,0);
                    break;
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
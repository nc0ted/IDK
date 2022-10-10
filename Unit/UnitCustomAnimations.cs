using UnityEngine;
using UnityEngine.Rendering.Universal;
using static Unit.UnitAnimationSystem.States;
using Task = System.Threading.Tasks.Task;
using static Unit.AnimationsMath;

namespace Unit
{
    public class UnitCustomAnimations
    {
        public Material BodyMaterial { get; set; } 
        public Material[] AllMaterials { get; set; }
        public Vector3 BodyDefaultLocal { get; set; }
        public Vector3 HeadDefaultLocal{ get; set; }
        public Vector3 ApparelDefaultLocal{ get; set; }
        public Transform Body{ get; set; }
        public Transform Head{ get; set; } 
        public Transform Apparel{ get; set; }
        public Transform Hair { get; set; }
        public Light2D HitLight{ get; set; }
        private static readonly int MaterialProp = Shader.PropertyToID("_SourceGlowDissolveFade");

        internal async void PlayAnimation(UnitAnimationSystem.States state)
        {
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
            while (Application.isPlaying)
            {
                for (int i = 0; i < 100; i++)
                {
                    LerpY(false,Head);
                    await Task.Delay(10);
                }
                for (int i = 0; i < 100; i++)
                {
                    LerpY(true,Head);
                    await Task.Delay(10);
                }
            }
        }
        private async Task PlayHit()
        {
            if (!Application.isPlaying) return;
            await EnableHitLight();
        }
        private async Task PlayDeath()
        {
            if (!Application.isPlaying) return;
            for (int i = 0; i < 100; i++)
            {
                if (!Application.isPlaying) return;
                LerpMaterial(AllMaterials,MaterialProp,0);
                LerpZRotation(false,Body);
                await Task.Delay(30);
            } 
        }
        private async Task EnableHitLight(int delayMs=100)
        {
            HitLight.enabled = true;
            await Task.Delay(delayMs);
            HitLight.enabled = false;
        }
    }
}

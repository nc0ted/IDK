using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Unity.Mathematics;

namespace Unit
{
    public struct AnimationsMath
    {
        internal static void LerpY(bool down, Transform transform, float speed = 0.01f, float vectorPlus = 0.05f)
        {
            if (transform == null) return;
            var localPosition = transform.localPosition;
            transform.localPosition = !down
                ? new Vector3(localPosition.x, math.lerp(localPosition.y, localPosition.y - vectorPlus, speed))
                : new Vector3(transform.localPosition.x,
                    math.lerp(transform.localPosition.y, transform.localPosition.y + vectorPlus, speed));
        }

        internal void LerpX(bool right, Transform transform, float speed = 0.01f, float vectorPlus = 0.05f)
        {
            if (transform == null) return;
            var localPosition = transform.localPosition;
            transform.localPosition = !right
                ? new Vector3(math.lerp(localPosition.x, localPosition.x - vectorPlus, speed), localPosition.y)
                : new Vector3(math.lerp(transform.localPosition.x, transform.localPosition.x + vectorPlus, speed),
                    transform.localPosition.y);
        }

        internal static void LerpZRotation(bool right, Transform transform, float endZRotation = 90f)
        {
            if (transform == null) return;
            if (Math.Abs(transform.eulerAngles.z - endZRotation) < 1f) return;
            transform.eulerAngles += new Vector3(0, 0, 5f);
        }

        internal static void DecrementXPosition(bool right, Transform transform, float speed)
        {
            switch (right)
            {
                case true:
                    transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                    break;
                case false:
                    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                    break;
            }
        }

        internal static void LerpMaterial(Material material,int propId,float endValue)
        {
            if (!Application.isPlaying) return;
            material.SetFloat(propId,math.lerp(material.GetFloat(propId),endValue,0.05f));
            Debug.Log(material.GetFloat(propId));
        }
        internal static void LerpMaterial([NotNull] IEnumerable<Material> materials,int propId,float endValue)
        {
            if (materials == null) return;
            foreach (var material in materials)
            {
                if (!Application.isPlaying) return;
                material.SetFloat(propId,math.lerp(material.GetFloat(propId),endValue,0.05f));
            }
        }
    }
}
public static class AnimationsMath
{
        private static void LerpY(bool down,Transform transform,float speed=0.01f,float vectorPlus=0.05f)
        {
            var localPosition = transform.localPosition;
            transform.localPosition = !down ? new Vector3(localPosition.x, math.lerp(localPosition.y, localPosition.y - vectorPlus, speed)) : new Vector3(transform.localPosition.x, math.lerp(transform.localPosition.y, transform.localPosition.y + vectorPlus, speed));
        }
        private static void LerpX(bool right,Transform transform,float speed=0.01f,float vectorPlus=0.05f)
        {
            var localPosition = transform.localPosition;
            transform.localPosition = !right ? new Vector3(math.lerp(localPosition.x, localPosition.x -vectorPlus, speed),localPosition.y) : new Vector3(math.lerp(transform.localPosition.x, transform.localPosition.x + vectorPlus, speed),transform.localPosition.y);
        }
        private static void LerpZRotation(bool right,Transform transform,float endZRotation=90f)
        {
           var rotation = transform.rotation;
           transform.eulerAngles = new Vector3(rotation.x, rotation.y, math.lerp(rotation.z, endZRotation, 90f));
           Debug.Log(rotation.z + " ROTATION");
        }
        private static void DecrementXPosition(bool right, Transform transform,float speed)
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
}
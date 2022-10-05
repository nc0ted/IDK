/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using UnityEngine;

namespace Grid
{
    public class Testing : MonoBehaviour
    {
        [SerializeField] private int width, height;
        private Pathfinding pathfinding;

        private void Awake() {
            pathfinding = new Pathfinding(width, height);
        }
    }
}

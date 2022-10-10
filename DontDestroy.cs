using UnityEngine;
public class DontDestroy : MonoBehaviour
{
    [SerializeField] private GameObject[] dontDestroyOnLoad;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
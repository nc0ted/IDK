using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxScale=15, minScale=5;
    [SerializeField] private bool enableMoveByKeyboard;
    
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel")>0&&cam.orthographicSize>=minScale)
            cam.orthographicSize -=  Time.deltaTime * scaleMultiplier;
        if(Input.GetAxis("Mouse ScrollWheel")<0&&cam.orthographicSize<=maxScale)
            cam.orthographicSize +=  Time.deltaTime * scaleMultiplier;
        
        if (!enableMoveByKeyboard) return;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.position +=
            new Vector3(horizontal * moveSpeed * Time.deltaTime, vertical * moveSpeed * Time.deltaTime);
    }
}

using System;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class UnitDirections : MonoBehaviour
{
    [Header("Body")] 
    [SerializeField] private Sprite bodySouth;
    [SerializeField] private Sprite bodyEast;

    [Header("Head")]
    [SerializeField] private Sprite headSouth;
    [SerializeField] private Sprite headEast; 
    [SerializeField] private Sprite headNorth;
    
    [Header("Hair")]
    [SerializeField] private Sprite hairSouth;
    [SerializeField] private Sprite hairEast;
    [SerializeField] private Sprite hairNorth;
    
    [Header("Apparel")] 
    [SerializeField] private Sprite apparelSouth;
    [SerializeField] private Sprite apparelEast;
    [SerializeField] private Sprite apparelNorth;
    
    [Header("Renderers")]
    [SerializeField] private SpriteRenderer apparelRenderer;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer headRenderer;
    [SerializeField] private SpriteRenderer hairRenderer;
    public Vector3 MoveDir { get; set; }

    private void Awake()
    {
        apparelRenderer = apparelRenderer.GetComponent<SpriteRenderer>();
        bodyRenderer = bodyRenderer.GetComponent<SpriteRenderer>();
        headRenderer = headRenderer.GetComponent<SpriteRenderer>();
        hairRenderer = hairRenderer.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
       var rot = Quaternion.LookRotation(Vector3.forward, MoveDir).normalized;
       if (rot.w > 0.9&&rot.z<0.5)
       {
           transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z,transform.rotation.w);
           apparelRenderer.sprite = apparelNorth;
           bodyRenderer.sprite = bodySouth;
           headRenderer.sprite = headNorth;
           hairRenderer.sprite = hairNorth;
       }
       if (rot.z > 0.9&&rot.w<0.5)
       {
           transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z,transform.rotation.w);
           apparelRenderer.sprite = apparelSouth;
           bodyRenderer.sprite = bodySouth;
           headRenderer.sprite = headSouth;
           hairRenderer.sprite = hairSouth;
       }
       if (math.abs(rot.z) > 0.3&&rot.w>0.3)
       {
           if(rot.z>0.3)
               transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z,transform.rotation.w);
           if(rot.z<0.3)
               transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z,transform.rotation.w);
           apparelRenderer.sprite = apparelEast;
           bodyRenderer.sprite = bodyEast;
           headRenderer.sprite = headEast;
           hairRenderer.sprite = hairEast;
       }
    }
}
using System;
using UnityEngine;


public class CommonTrap : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject particle;
    [SerializeField] private Transform particlePoint;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && col.GetComponent<UnitHealthSystem>())
        {
            col.GetComponent<UnitHealthSystem>().TakeDamage(damage);
        }
    }
}
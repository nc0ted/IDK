using NPC;
using Unit;
using static Unit.UnitAnimationSystem.States;
using UnityEngine;

public class UnitHealthSystem : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private bool destroyOnDeath;

    private bool _isDead;
    private NpcMovement _npcMovement;
    private UnitAnimationSystem _animationSystem;

    private void Awake()
    {
        _npcMovement = GetComponent<NpcMovement>();
        _animationSystem = GetComponent<UnitAnimationSystem>();
    }
    internal void TakeDamage(int damage)
    {
        if (_isDead) return;
        _npcMovement.SetSpeed(1);
        _animationSystem.SetAnimationState(Hit);
        health -= damage;
        if (health <= 0)
        {
            Die();
            return;
        }
        Invoke(nameof(SetSpeedBack),2f);
    }

    private void Die()
    {
        _isDead = true;
        _npcMovement.SetSpeed(0);
        _animationSystem.SetAnimationState(Death);
        if(destroyOnDeath)
            Destroy(gameObject,3);
        else
            Invoke(nameof(DeactivateUnit),3f);
    }

    private void DeactivateUnit()
    {
        gameObject.SetActive(false);
    }
    
    private void SetSpeedBack()
    {
        if(_isDead)return;
        _npcMovement.SetSpeed(6);
    }
}
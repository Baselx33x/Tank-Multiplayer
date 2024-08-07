using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    public float m_MaxHealth = 100f;
    
    public Action<Health> OnDead;


    private bool m_IsDead = false;
    private NetworkVariable<float > m_Health = new NetworkVariable<float >();

    public override void OnNetworkSpawn()
    {
        m_Health.Value = m_MaxHealth;
    }


    public void TakeDamage(float damage)
    {
        HandlHealth(-damage);
    }


    public void Heal(float healamount)
    {
        HandlHealth(healamount);
    }

    private void HandlHealth (float value)
    {
        if (m_IsDead) return;

        float newHealth = m_Health.Value + value;

        m_Health.Value = Mathf.Clamp(newHealth, 0f, m_MaxHealth);

        if (m_Health.Value <= 0f)
        {
            OnDead?.Invoke(this);
            m_IsDead = true;
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    public float m_MaxHealth = 100f;
    public event Action OnDead;
    public NetworkVariable<float > m_Health = new NetworkVariable<float >();


    private bool m_IsDead = false;

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
        if (!IsServer) return;

        if (m_IsDead) return;

        float newHealth = m_Health.Value + value;

        m_Health.Value = Mathf.Clamp(newHealth, 0f, m_MaxHealth);

        if (m_Health.Value <= 0f)
        {
            OnDead?.Invoke();
            m_IsDead = true;

            //Debug.Log("NetworkManager.LocalClient =  " + NetworkManager.LocalClient.ClientId + "  ::: " + "Dead");
            Destroy(gameObject);
        }
    }


}

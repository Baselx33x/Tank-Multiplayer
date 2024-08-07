using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

public class DisplyHealthUI : NetworkBehaviour
{

    [Header("References")]
    [SerializeField] private Health m_Health;
    [SerializeField] private Image m_Image;

    public override void OnNetworkSpawn()
    {
        if (!IsClient) return;

        m_Health.m_Health.OnValueChanged += OnHealhtChanged;
        OnHealhtChanged(0, m_Health.m_Health.Value);
    }

    public override void OnNetworkDespawn()
    {
        if (!IsClient) return;

        m_Health.m_Health.OnValueChanged -= OnHealhtChanged;
    }

    private void OnHealhtChanged(float previousValue, float newValue)
    {

        m_Image.fillAmount = (float)newValue / m_Health.m_MaxHealth;
    }


}

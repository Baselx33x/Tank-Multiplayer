using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class DisplyTextUI : NetworkBehaviour
{
    [SerializeField] private Collecte m_CollecteScript;
    [SerializeField] private TextMeshProUGUI m_Text;


    public override void OnNetworkSpawn()
    {
        m_CollecteScript.CoinValue.OnValueChanged += OnCoinValueChanged;
    }

   

    public override void OnNetworkDespawn()
    {
        m_CollecteScript.CoinValue.OnValueChanged -= OnCoinValueChanged;
    }

    private void OnCoinValueChanged(int previousValue, int newValue)
    {
        m_Text.text = "Coins : " +  m_CollecteScript.CoinValue.Value.ToString();
    }


}

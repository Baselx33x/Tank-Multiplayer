using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Collecte : NetworkBehaviour
{
    public NetworkVariable<int> CoinValue = new NetworkVariable<int>();

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Coin")
        {
            if (collision.attachedRigidbody.TryGetComponent<BaseCollectables>(out BaseCollectables Collectabels))
            {
                Collectabels.Collect();

                CoinCollectabels coin = Collectabels as CoinCollectabels;
                if (coin != null)
                {
                    if (!IsServer) return;

                    CoinValue.Value += coin.m_CoinValue;

                    Debug.Log(" ClientId : " + NetworkManager.LocalClient.ClientId + "  Has Collected : " + CoinValue.Value + "Coins");
                }

            }
        }
          
  
    }
}

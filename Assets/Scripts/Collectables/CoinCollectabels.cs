using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CoinCollectabels : BaseCollectables
{
    public int m_CoinValue = 10 ; 
    

    public override void Collect()
    {
        if (IsClient)
        {
            HideCollactable(true);
        } 

        if (IsServer)
        {
            HideCollactable(false);
        }
    }
}

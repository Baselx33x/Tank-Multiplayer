using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class BaseCollectables : NetworkBehaviour
{

    [SerializeField] private SpriteRenderer m_CollectableSprite;

    protected void HideCollactable(bool hide)
    {
        m_CollectableSprite.enabled = !hide;
    }

    public abstract void Collect();

   
}

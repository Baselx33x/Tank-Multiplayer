using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    [SerializeField] Button m_ServerButton;
    [SerializeField] Button m_HostButton;
    [SerializeField] Button m_ClientButton;



    private void Awake()
    {
        m_ServerButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
        m_HostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        m_ClientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }

}

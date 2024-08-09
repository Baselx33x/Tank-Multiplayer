using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class Chat : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ChatText; 
    [SerializeField] private TMP_InputField m_InputFieldText;
    

    void Update()
    {
        if (!String.IsNullOrEmpty(m_InputFieldText.text) && Input.GetKeyDown(KeyCode.Return))
        {
            CreateChat();
        }
    }


    private void CreateChat ()
    {
        SendToServerRpc(m_InputFieldText.text); 
    }



    [ServerRpc(RequireOwnership = false)]
    private void SendToServerRpc(FixedString128Bytes message)
    {
         string ServerMessage = "Player : " + message;
         SendToClientRpc(ServerMessage);
    }


    [ClientRpc]
    private void SendToClientRpc(FixedString128Bytes message)
    {
        m_ChatText.text +=  message.ToString() + "\n";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class JoinCodeView : MonoBehaviour
{
    private void Start()
    {
        var relayManager = FindObjectOfType<RelayManager>();
        if (NetworkManager.Singleton.IsHost)
        {
            GetComponentInChildren<TMPro.TMP_Text>().text = $"JoinCode: {relayManager.JoinCode}";
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

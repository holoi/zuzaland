using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Firework : MonoBehaviour
{
    [SerializeField] float m_Speed = 1.6f;

    [SerializeField] float m_Lifetime = 6f;

    private float m_Duration;

    private void Update()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            transform.position += m_Speed * Time.deltaTime * transform.forward;
            m_Duration += Time.deltaTime;
            if (m_Duration > m_Lifetime)
            {
                Destroy(gameObject);
            }
        }  
    }
}

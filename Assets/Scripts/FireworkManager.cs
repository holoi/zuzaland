using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FireworkManager : MonoBehaviour
{
    [SerializeField] GameObject m_FirePrefab;

    [SerializeField] Vector3 m_Offset = new(0f, 0f, 2f);

    public void Fire()
    {
        var camera = Camera.main;
        Debug.Log($"Camera position: {camera.transform.position}");
        Debug.Log($"Camera rotation: {camera.transform.rotation}");
        var firework = Instantiate(m_FirePrefab, camera.transform.position + camera.transform.rotation * m_Offset, camera.transform.rotation);
        firework.GetComponent<NetworkObject>().Spawn();
    }
}

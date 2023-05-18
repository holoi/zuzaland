using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FireworkManager : MonoBehaviour
{
    public Vector3 TargetPosition = new(0,0,0);

    [SerializeField] GameObject m_FirePrefab;

    [SerializeField] Vector3 m_Offset = new(0f, 0f, 2f);

    public void Fire()
    {
        var camera = Camera.main;
        Debug.Log($"Camera position: {camera.transform.position}");
        Debug.Log($"Camera rotation: {camera.transform.rotation}");

        //var fixedPosition = camera.transform.position + camera.transform.rotation * m_Offset;
        var fixedPosition = TargetPosition + RandomVector3(-1f,1f);

        var firework = Instantiate(m_FirePrefab, fixedPosition, camera.transform.rotation);
        firework.GetComponent<NetworkObject>().Spawn();
    }

    Vector3 RandomVector3(float min, float max)
    {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }
}

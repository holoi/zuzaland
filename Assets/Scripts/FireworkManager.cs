using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FireworkManager : NetworkBehaviour
{
    [SerializeField] GameObject m_Firework1Prefab;

    [SerializeField] GameObject m_Firework2Prefab;

    [SerializeField] GameObject m_Firework3Prefab;

    [SerializeField] Transform m_Monument;

    [SerializeField] GameObject m_Firework3Window;

    public string Firework3InputText { get; private set; }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void Fire1ServerRpc()
    {
        // Monument ground center position
        var monumentPos = m_Monument.position;
        // Add random deviation
        // Sizheng TODO
        Vector3 firePos = new Vector3(monumentPos.x + Random.Range(-3f, 3f), monumentPos.y, monumentPos.z + Random.Range(-3f, 3f));

        var fireworkInstance = Instantiate(m_Firework1Prefab, firePos, Quaternion.identity);
        fireworkInstance.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void Fire2ServerRpc()
    {
        // Monument ground center position
        var monumentPos = m_Monument.position;
        // Add random deviation
        // Sizheng TODO
        Vector3 firePos = new Vector3(monumentPos.x + Random.Range(-3f, 3f), monumentPos.y, monumentPos.z + Random.Range(-3f, 3f));

        var fireworkInstance = Instantiate(m_Firework2Prefab, firePos, Quaternion.identity);
        fireworkInstance.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void Fire3ServerRpc()
    {
        // Monument ground center position
        var monumentPos = m_Monument.position;
        // Add random deviation
        // Sizheng TODO
        Vector3 firePos = new Vector3(monumentPos.x + Random.Range(-3f, 3f), monumentPos.y, monumentPos.z + Random.Range(-3f, 3f));

        var fireworkInstance = Instantiate(m_Firework3Prefab, firePos, Quaternion.identity);
        fireworkInstance.GetComponent<TextFireWorksVFX>().InputText = Firework3InputText;
        fireworkInstance.GetComponent<TextFireWorksVFX>().SetInputText();
        fireworkInstance.GetComponent<NetworkObject>().Spawn();

    }

    public void OnChangeFirework3InputText(string text)
    {
        Firework3InputText = text;
        Fire3ServerRpc();
        m_Firework3Window.SetActive(false);
    }

    public void OnShowFirework3Window()
    {
        m_Firework3Window.SetActive(true);
    }
}

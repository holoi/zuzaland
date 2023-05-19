using System.Collections;
using System.Collections.Generic;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using System;
using Unity.Networking.Transport.Relay;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine.SceneManagement;

public class RelayManager : MonoBehaviour
{
    [SerializeField] int m_MaxConnections = 8;

    [SerializeField] GameObject m_ConnectingPanel;

    [SerializeField] GameObject m_BtnPanel;

    public string JoinCode { get; private set; }

    private string m_LobbyId;

    private async void Start()
    {
        DontDestroyOnLoad(this);

        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"Player Id: {AuthenticationService.Instance.PlayerId}");

            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

            m_ConnectingPanel.SetActive(false);
            m_BtnPanel.SetActive(true);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return;
        }

        // Hide the connecting panel and show the menu panel
    }

    public async void StartHost()
    {
        Allocation allocation;

        try
        {
            allocation = await RelayService.Instance.CreateAllocationAsync(m_MaxConnections);
        }
        catch (Exception e)
        {
            Debug.LogError($"Relay create allocation request failed {e.Message}");
            throw;
        }

        try
        {
            JoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        }
        catch (Exception e)
        {
            Debug.LogError($"Relay get join code request failed {e.Message}");
            throw;
        }

        var relayServerData = new RelayServerData(allocation, "dtls");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

        // Lobby
        try
        {
            var createLobbyOptions = new CreateLobbyOptions();
        }
        catch (Exception e)
        {

        }

        NetworkManager.Singleton.StartHost();
    }

    public async void StartClient()
    {
        if (JoinCode == null)
        {
            Debug.Log("Please provide a JoinCode to connect");
            return;
        }

        JoinAllocation allocation;

        try
        {
            allocation = await RelayService.Instance.JoinAllocationAsync(JoinCode);
        }
        catch (Exception e)
        {
            Debug.LogError($"Relay get join code request failed {e.Message}");
            throw;
        }

        var relayServerData = new RelayServerData(allocation, "dtls");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

        // Lobby
        try
        {
            var createLobbyOptions = new CreateLobbyOptions();
            createLobbyOptions.IsPrivate = false;
            createLobbyOptions.Data = new Dictionary<string, DataObject>()
            {
                {
                    "JoinCode", new DataObject(
                        visibility: DataObject.VisibilityOptions.Member,
                        value: JoinCode
                    )
                }
            };

            Lobby lobby = await Lobbies.Instance.CreateLobbyAsync("My Lobby", m_MaxConnections, createLobbyOptions);
            m_LobbyId = lobby.Id;
            StartCoroutine(HeartbeatLobbyCoroutine(15f));

        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
            throw;
        }

        NetworkManager.Singleton.StartClient();
    }

    private IEnumerator HeartbeatLobbyCoroutine(float waitTimeSeconds)
    {
        var delay = new WaitForSeconds(waitTimeSeconds);
        while (true)
        {
            Lobbies.Instance.SendHeartbeatPingAsync(m_LobbyId);
            yield return delay;
        }
    }

    public void OnChangeJoinCode(string code)
    {
        JoinCode = code;
    }

    private void OnClientConnected(ulong clientId)
    {
        m_BtnPanel.SetActive(false);
        Debug.Log($"JoinCode: {JoinCode}");
        SceneManager.LoadSceneAsync("Geospatial", LoadSceneMode.Single);
    }
}

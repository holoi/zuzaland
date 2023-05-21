using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyList : MonoBehaviour
{
    [SerializeField] Transform m_LobbyListRoot;

    [SerializeField] LobbyItem m_LobbyItemPrefab;

    bool m_IsRefreshing = false;

    bool m_IsJoining = false;

    private void OnEnable()
    {
        RefreshList();
    }

    public async void RefreshList()
    {
        if (m_IsRefreshing)
            return;

        m_IsRefreshing = true;

        try
        {
            var options = new QueryLobbiesOptions();
            options.Count = 25;
            options.Filters = new List<QueryFilter>()
            {
                new QueryFilter(field: QueryFilter.FieldOptions.AvailableSlots, op: QueryFilter.OpOptions.GT, value: "0"),
                new QueryFilter(field: QueryFilter.FieldOptions.IsLocked, op: QueryFilter.OpOptions.EQ, value: "0")
            };

            var lobbies = await Lobbies.Instance.QueryLobbiesAsync(options);
            Debug.Log($"Lobby count: {lobbies.Results.Count}");

            foreach (Transform child in m_LobbyListRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (Lobby lobby in lobbies.Results)
            {
                var lobbyInstance = Instantiate(m_LobbyItemPrefab, m_LobbyListRoot);
                lobbyInstance.Initialize(this, lobby);
                lobbyInstance.GetComponent<RectTransform>().localScale = Vector3.one;
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
            m_IsRefreshing = false;
            throw;
        }

        m_IsRefreshing = false;
    }

    public async void JoinAsync(Lobby lobby)
    {
        if (m_IsJoining)
            return;

        m_IsJoining = true;

        try
        {
            var joinedLobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobby.Id);
            string joinCode = joinedLobby.Data["JoinCode"].Value;

            var relayManager = FindObjectOfType<RelayManager>();
            relayManager.JoinCode = joinCode;
            await relayManager.StartClient();
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
            m_IsJoining = false;
            throw;
        }

        m_IsJoining = false;
    }
}

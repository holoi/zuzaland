using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;

public class LobbyItem : MonoBehaviour
{
    [SerializeField] TMP_Text m_LobbyNameText;

    [SerializeField] TMP_Text m_LobbyPlayersText;

    LobbyList m_LobbyList;

    Lobby m_Lobby;

    public void Initialize(LobbyList lobbyList, Lobby lobby)
    {
        m_LobbyList = lobbyList;
        m_Lobby = lobby;

        m_LobbyNameText.text = lobby.Name;
        m_LobbyPlayersText.text = $"{lobby.Players.Count}/{lobby.MaxPlayers}";
    }

    public void Join()
    {
        m_LobbyList.JoinAsync(m_Lobby);
    }
}

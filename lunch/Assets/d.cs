using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class d : MonoBehaviourPunCallbacks
{
    public void DisplayPlayerList()
    {
        // Check if we are in a room
        if (PhotonNetwork.CurrentRoom != null)
        {
            List<string> playerNames = new List<string>();

            // Iterate through each player in the room
            foreach (KeyValuePair<int, Player> playerEntry in PhotonNetwork.CurrentRoom.Players)
            {
                Player player = playerEntry.Value;
                playerNames.Add(player.NickName);  // Get each player's nickname
            }

            // Join all player names into a single string separated by new lines
            Debug.Log(string.Join("\n", playerNames));
        }
        else
        {
            Debug.Log("not in room");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        DisplayPlayerList();  // Update the player list when a new player joins
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        DisplayPlayerList();  // Update the player list when a player leaves
    }
}

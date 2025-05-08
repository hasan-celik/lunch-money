using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    private CreateAndJoin createAndJoinScript;
    
    public TextMeshProUGUI roomNameText;
    public TextMeshProUGUI isRoomOpenText;
    public TextMeshProUGUI roomPlayerCountText;
    
    private string roomName;
    private string isRoomOpen;
    private string roomPlayerCount;
    private string maxPlayerCount;

    public void SetRoomInfo(string _roomName, string _isRoomOpen, string _roomPlayerCount, string _maxPlayerCount, CreateAndJoin _createAndJoinScript)
    {
        roomName = _roomName;
        roomNameText.text = roomName;
        
        isRoomOpen = _isRoomOpen;
        isRoomOpenText.text = isRoomOpen;
        
        roomPlayerCount = _roomPlayerCount;
        maxPlayerCount = _maxPlayerCount;
        roomPlayerCountText.text = roomPlayerCount + "/" + maxPlayerCount;
        
        createAndJoinScript = _createAndJoinScript;
    }

    public void JoinRoom()
    {
        createAndJoinScript.JoinRoomInList(roomName);
    }
}
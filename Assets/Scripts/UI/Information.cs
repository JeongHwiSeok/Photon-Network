using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Information : MonoBehaviourPunCallbacks
{
    [SerializeField] string roomName;
    public TextMeshProUGUI roomInformation;

    public void ConnectRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void RoomData(string name, int currentStaff, int maxStaff)
    {
        roomName = name;

        roomInformation.fontSize = 50;
        roomInformation.text = name + " ( " + currentStaff + " / " + maxStaff + " )";
    }
}

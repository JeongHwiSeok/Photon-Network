using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime; // ��� ������ �������� �� �̺�Ʈ�� ȣ���ϴ� ���̺귯��
using UnityEngine.UI;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreateButton;
    public Transform roomParentTransform;
    public TMP_InputField roomNameInputField;
    public TMP_InputField roomPersonalInputField;

    // �� ����� �����ϱ� ���� �ڷᱸ��
    Dictionary<string, RoomInfo> roomDictionary = new Dictionary<string, RoomInfo>();

    private void Update()
    {
        if(roomNameInputField.text.Length > 0 && roomPersonalInputField.text.Length > 0)
        {
            roomCreateButton.interactable = true;
        }
        else
        {
            roomCreateButton.interactable = false;
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void InstantiateRoom()
    {
        // �� �ɼ��� ����
        RoomOptions roomOptions = new RoomOptions();

        // �ִ� �������� ���� ����
        roomOptions.MaxPlayers = byte.Parse(roomPersonalInputField.text);

        // ���� ���� ���� ����
        roomOptions.IsOpen = true;

        // �κ񿡼� �� ����� �����ų �� ����
        roomOptions.IsVisible = true;

        // ���� �����ϴ� �Լ�
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    }

    // �ش� �κ� �� ����� ���� ������ ������ ȣ��(�߰�, ����, ����) �Ǵ� �ݹ� �Լ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // ���� ����
        RemoveRoom();

        // ���� ������Ʈ
        UpdateRoom(roomList);

        // ���� ����
        CreateRoomObject();
    }

    public void RemoveRoom()
    {
        foreach(Transform roomTransform in roomParentTransform)
        {
            Destroy(roomTransform.gameObject);
        }
    }

    public void UpdateRoom(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            // �ش� �̸��� roomDictionary�� key ������ �����Ǿ� �ִٸ�?
            if(roomDictionary.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : (true) �뿡�� �����Ǿ��� ��
                if(roomList[i].RemovedFromList)
                {
                    roomDictionary.Remove(roomList[i].Name);
                }
                else
                {
                    roomDictionary[roomList[i].Name] = roomList[i];
                }
            }
            else
            {
                roomDictionary[roomList[i].Name] = roomList[i];
            }
        }
    }

    public void CreateRoomObject()
    {
        // roomDictionary�� ���� ���� Values���� ����ִٸ� roomInfo�� �־���
        foreach(RoomInfo roomInfo in roomDictionary.Values)
        {
            // room ������Ʈ�� ����
            GameObject room = Instantiate(Resources.Load<GameObject>("room"));

            // room ������Ʈ�� �θ� ������Ʈ�� ����
            room.transform.SetParent(roomParentTransform);

            // room�� ���� ������ �Է�
            room.GetComponent<Information>().RoomData(roomInfo.Name, roomInfo.PlayerCount, roomInfo.MaxPlayers);
        }
    }
}

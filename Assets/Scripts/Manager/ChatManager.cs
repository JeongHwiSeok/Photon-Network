using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField inputField;
    [SerializeField] Transform contentTransform;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(inputField.text.Length == 0)
            {
                return;
            }

            string chatting = PhotonNetwork.NickName + " : " + inputField.text;

            // photonView.RPC�� ù ��° �Ű����� : �����ų �Լ��� �̸�
            // photonView.RPC�� �� ��° �Ű����� : ȣ���� �Լ��� ���� �� �ִ� ����� �����մϴ�
            // photonView.RPC�� �� ��° �Ű����� : ������ �Լ��� �Ű�����

            photonView.RPC("Chatting", RpcTarget.All, chatting);
        }
    }

    [PunRPC]
    public void Chatting(string message)
    {
        GameObject content = Instantiate(Resources.Load<GameObject>("String"));

        content.GetComponent<Text>().text = message;

        content.transform.SetParent(contentTransform);

        inputField.text = " ";
    }
}
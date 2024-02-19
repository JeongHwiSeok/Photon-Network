using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Nickname : MonoBehaviourPunCallbacks
{
    [SerializeField] Camera playerCamera;
    [SerializeField] TextMeshProUGUI nickName;

    // Start is called before the first frame update
    void Start()
    {
        nickName.text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}

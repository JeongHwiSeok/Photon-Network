using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PhotonPlayer : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] float mouseX;
    [SerializeField] float rotateSpeed;

    [SerializeField] Vector3 direction;
    [SerializeField] Camera temporaryCamare;

    private void Awake()
    {
        speed = 5f;
        rotateSpeed = 1200f;
    }

    private void Start()
    {
        // 현재 플레이어가 나 자신이라면?
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            temporaryCamare.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    private void Update()
    {
        if (photonView.IsMine == false) return;

        Movement();

        Rotation();
    }

    public void Movement()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();

        // TransformDirection : direction의 방향으로 이동하는 함수.
        transform.position += transform.TransformDirection(direction) * speed * Time.deltaTime;
    }

    public void Rotation()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * rotateSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }
}

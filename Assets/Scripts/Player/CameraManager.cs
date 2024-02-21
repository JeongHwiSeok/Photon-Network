using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float cameraRotationLimit = 60f;
    [SerializeField] float scrollSpeed = 100.0f;
    [SerializeField] float sensitivity = 10f;

    [SerializeField] float currentRotationX;

    private void Start()
    {
        
    }

    private void Update()
    {
        RotateCamera();
    }

    public void RotateCamera()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRotation * sensitivity;

        currentRotationX -= cameraRotationX;

        currentRotationX = Mathf.Clamp(currentRotationX, -cameraRotationLimit, cameraRotationLimit);

        transform.eulerAngles = new Vector3(currentRotationX, 0, 0);
    }
}

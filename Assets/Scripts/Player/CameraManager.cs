using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float cameraRotationLimit = 60f;
    // [SerializeField] float scrollSpeed;
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

        currentRotationX -= cameraRotationX * Time.deltaTime;

        currentRotationX = Mathf.Clamp(currentRotationX, -cameraRotationLimit, cameraRotationLimit);

        transform.localEulerAngles = new Vector3(currentRotationX, 0, 0);
    }
}

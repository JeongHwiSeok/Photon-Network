using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOUSETYPE
{
    LOCK,
    FREE,
}

public class MouseManager : MonoBehaviour
{
    private void Start()
    {
        SetMouse(MOUSETYPE.LOCK);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetMouse(MOUSETYPE.FREE);
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetMouse(MOUSETYPE.LOCK);
        }
    }

    public void LockMouse()
    {
        SetMouse(MOUSETYPE.LOCK);
    }

    public void SetMouse(MOUSETYPE mouseType)
    {
        switch (mouseType)
        {
            case MOUSETYPE.LOCK : Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;

            case MOUSETYPE.FREE : Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
                
        }
    }
}

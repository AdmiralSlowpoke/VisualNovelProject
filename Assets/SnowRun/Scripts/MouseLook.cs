using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform playerBody;
    private float Sensitivity;

    float xRotation = 0f;
    void Start()
    {
        if (PlayerPrefs.HasKey("Sensitivity"))
            Sensitivity = 5f * PlayerPrefs.GetFloat("Sensitivity");
        else
            Sensitivity = 5f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime != 0f)
        {
            float mouseX = Input.GetAxis("Mouse X") * Sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * Sensitivity;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}

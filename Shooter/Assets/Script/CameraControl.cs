using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity;

    float xAxisLimit = 0;

    [SerializeField]
    Transform player, playerArms;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationAmountX = mouseX * mouseSensitivity;
        float rotationAmountY = mouseY * mouseSensitivity;

        xAxisLimit -= rotationAmountY;

        Vector3 rotationArms = playerArms.transform.rotation.eulerAngles;
        Vector3 rotationPlayer = player.transform.rotation.eulerAngles;

        rotationArms.x -= rotationAmountY;
        rotationArms.z = 0;
        rotationPlayer.y += rotationAmountX;

        if (xAxisLimit > 90)
        {
            rotationArms.x = 90;
            xAxisLimit = 90;
        }
        else if (xAxisLimit < -90)
        {
            rotationArms.x = 270;
            xAxisLimit = -90;
        }
        

        playerArms.rotation = Quaternion.Euler(rotationArms);
        player.rotation = Quaternion.Euler(rotationPlayer);
    }


    
}

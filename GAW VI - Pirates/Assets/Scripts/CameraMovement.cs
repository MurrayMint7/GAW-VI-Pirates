using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //For Camera
    public Transform PlayerCamera;
    public Vector2 sens;

    private Vector2 XYRotation;

    public GameObject projectile;

    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //All For Camera

        Vector2 MouseInput = new Vector2
        {
            x = Input.GetAxisRaw("Mouse X"),
            y = Input.GetAxisRaw("Mouse Y")

        };


        XYRotation.x -= MouseInput.y * sens.y;
        XYRotation.y += MouseInput.x * sens.x;

        XYRotation.x = Mathf.Clamp(XYRotation.x, -90f, 90f);
        transform.eulerAngles = new Vector3(0f, XYRotation.y, 0f);
        PlayerCamera.localEulerAngles = new Vector3(XYRotation.x, 0f, 0f);

        if (PlayerMovement.health <= 0)
        {


            panel.SetActive(true);
            Cursor.visible = true;
            sens = sens - sens;
            PlayerMovement.moveSpeed = PlayerMovement.moveSpeed - PlayerMovement.moveSpeed;

        }
    }
}

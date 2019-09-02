﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] public float sensitivity = 4.0f;
    [SerializeField] public float smoothing = 2.0f;

    private float minimumX = -60f;
    private float maximumX = 60f;

    private GameObject player;

    private Vector2 mouseLook;
    private Vector2 smoothV;

    // Start is called before the first frame update
    void Awake()
    {
        LockCursor();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MouseMovement();
    }

    private void LockCursor()
    {
        //Locks the mouse cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Lets you look around the world like a boss!
    void MouseMovement()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, minimumX, maximumX);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float gravityMultiplier = 1.0f;

    public List<XRController> controllers;

    private CharacterController characterController;
    private GameObject head;


    private void Awake()
    {
        characterController = GetComponentInParent<CharacterController>();
        head = GetComponentInParent<XRRig>().cameraGameObject;
    }

    private void Start()
    {
        PositionController();
    }

    private void FixedUpdate()
    {
        PositionController();
        CheckForInput();
        //ApplyGravity();

    }

    private void PositionController()
    {
        float headHeight = Mathf.Clamp(head.transform.localPosition.y, 1, 2);
        characterController.height = headHeight;

        Vector3 newCenter;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;

        newCenter.x = head.transform.position.x;
        newCenter.z = head.transform.position.z;

        characterController.center = newCenter;
    }

    private void CheckForInput()
    {
        foreach (XRController controller in controllers)
        {
            if (controller.enableInputActions)
                CheckForMovement(controller.inputDevice);
        }
    }

    private void CheckForMovement(InputDevice device)
    {
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
            StartMove(position);
    }

    private void StartMove(Vector2 position)
    {
        Vector3 direction = new Vector3(position.x, 0, position.y);
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

        direction = Quaternion.Euler(headRotation) * direction;

        Vector3 movement = direction * speed;
        characterController.Move(movement * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        Vector3 gravity = new Vector3(0, Physics.gravity.y * gravityMultiplier, 0);
        gravity.y *= Time.deltaTime;

        characterController.Move(gravity * Time.deltaTime);
    }
}

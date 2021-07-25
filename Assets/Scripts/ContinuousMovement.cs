using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public float speed = 1;
    public XRNode inputSourceRight;
    public XRNode inputSourceLeft;
    public float additionalHeight = 0.2f;

    private XRRig rig;
    private Vector2 inputAxisRight;
    private Vector2 inputAxisLeft;
    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(inputSourceRight);
        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxisRight);

        InputDevice leftController = InputDevices.GetDeviceAtXRNode(inputSourceLeft);
        leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxisLeft);

    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);

        Vector3 direction = headYaw * new Vector3(inputAxisRight.x, inputAxisLeft.y, inputAxisRight.y);
        character.Move(direction * Time.fixedDeltaTime * speed);
    }

    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}

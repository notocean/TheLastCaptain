using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (PlayerInfor))]
public class PlayerMovement : MonoBehaviour
{
    Vector3 moveDirection;

    [SerializeField] float rotateSpeed = 1.0f;
    [SerializeField] float shelveSpeed = 1.0f;
    [SerializeField] float maxSideDegree = 5f;
    [SerializeField] float maxDistanceRay;
    [SerializeField] LayerMask obstacle;
    [SerializeField] LayerMask unexplorer;
    Joystick joystick;
    float moveSpeed;
    float sideDegree = 0;
    bool isLeftRotate = false;
    bool isShelve = false;

    private void Awake() {
        moveDirection = transform.forward;
        joystick = GameObject.Find("Joystick").GetComponent<Joystick>();
    }

    private void Start() {
        moveSpeed = GetComponent<PlayerInfor>().GetSpeed();
    }

    private void Update() {
        RotatePlayer();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void RotatePlayer() {
        Vector3 joystickDir = joystick.Direction;
        if (Vector3.Angle(moveDirection, joystickDir) > 0.01f) {
            if (joystickDir != Vector3.zero) {
                isLeftRotate = moveDirection.x * joystickDir.z - moveDirection.z * joystickDir.x > 0 ? true : false;
                int sideRotation = isLeftRotate ? -1 : 1;
                moveDirection = Quaternion.Euler(0, sideRotation * rotateSpeed * Time.deltaTime, 0) * moveDirection;
                moveDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(moveDirection);
                isShelve = true;
            }
        }
        else {
            isShelve= false;
        }
        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, SideDegree(isShelve));
    }

    private float SideDegree(bool isShelve) {
        if (!isShelve) {
            if (Mathf.Abs(sideDegree) <= 0.05f)
                return 0;
            if (sideDegree > 0) {
                sideDegree -= shelveSpeed * Time.deltaTime;
            }
            else {
                sideDegree += shelveSpeed * Time.deltaTime;
            }
        }
        else {
            if (isLeftRotate && sideDegree < maxSideDegree) {
                sideDegree += shelveSpeed * Time.deltaTime;
            }
            else if (!isLeftRotate && sideDegree > -maxSideDegree) {
                sideDegree -= shelveSpeed * Time.deltaTime;
            }
        }
        return sideDegree;
    }

    private void MovePlayer() {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, maxDistanceRay, obstacle))
            return;
        Vector3 joystickDir = joystick.Direction;
        if (joystickDir != Vector3.zero) {
            Vector3 currentPos = transform.position + transform.forward * moveSpeed * Time.deltaTime;
            transform.position = currentPos;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (((1 << other.gameObject.layer) & unexplorer) != 0) {
            FOVManager fovManager = other.GetComponent<FOVManager>();
            if (fovManager != null) {
                fovManager.EnableFOV();
            }
        }
    }
}

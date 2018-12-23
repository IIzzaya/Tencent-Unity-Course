using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    public float rotateSpeed = 5f;

    public Transform Unrotate;
    private Animator myAnimator;
    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    void SmoothRotate(float yAngle) {
        var lerpQuaternion = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, yAngle, 0), rotateSpeed * Time.deltaTime);
        transform.rotation = lerpQuaternion;
    }

    private void Update() {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var movementVector = (new Vector2(h, v)).normalized;
        var yAngle = Vector2.Angle(Vector2.right, movementVector);
        if (movementVector.y < 0)
            yAngle = 360 - yAngle;

        if (h != 0 || v != 0) {
            myAnimator.SetBool("isMoving", true);
            SmoothRotate(180 - yAngle - 45);

            transform.position += transform.forward * speed * Time.deltaTime;
        } else {
            myAnimator.SetBool("isMoving", false);
        }

        if (Input.GetButton("Fire1")) {
            myAnimator.SetBool("isShooting", true);
        } else {
            myAnimator.SetBool("isShooting", false);
        }


        Unrotate.position = transform.position;
        Unrotate.rotation = Quaternion.identity;
    }
}
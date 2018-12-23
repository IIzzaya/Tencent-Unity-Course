using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    public float rotateSpeed = 5f;
    public float shootingMoveMultiplier = 1 / 3f;
    public float diveRollSpeedMultiplier = 1f;
    public AnimationCurve diveRollSpeedCurve;
    public float diveRollYPosMultiplier = 1f;
    public AnimationCurve diveRollYPosCurve;

    public bool isMoving = false;
    public bool isShooting = false;
    public bool isDiveRolling = false;
    public bool enableDiveRoll = true;

    public Transform Unrotate;
    private Animator myAnimator;
    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    void SmoothRotate(float yAngle) {
        var lerpQuaternion = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, yAngle, 0), rotateSpeed * Time.deltaTime);
        transform.rotation = lerpQuaternion;
    }

    public void CalDiveRollMultiplier(float duration) {
        diveRollSpeedMultiplier = diveRollSpeedCurve.Evaluate(duration);
        diveRollYPosMultiplier = diveRollYPosCurve.Evaluate(duration);
    }

    void DiveRollStart() {
        Debug.Log("Dive Roll Start");
        enableDiveRoll = false;
        isDiveRolling = true;
        myAnimator.SetBool("triggerDiveRoll", isDiveRolling);
    }

    public void DiveRollEnd() {
        Debug.Log("Dive Roll End");
        enableDiveRoll = true;
        isDiveRolling = false;
    }

    private void Update() {

        if (Input.GetButton("Fire1")) {
            isShooting = true;
            myAnimator.SetBool("isShooting", isShooting);

        } else {
            isShooting = false;
            myAnimator.SetBool("isShooting", isShooting);
        }

        if (isDiveRolling) {
            transform.position += transform.forward * speed * diveRollSpeedMultiplier * Time.deltaTime;
            var pos = transform.position;
            pos.y = 1f;
            pos.y *= diveRollYPosMultiplier;
            transform.position = pos;
            Unrotate.position = transform.position;
            Unrotate.rotation = Quaternion.identity;
            return;
        }
        
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var movementVector = (new Vector2(h, v)).normalized;
        var yAngle = Vector2.Angle(Vector2.right, movementVector);
        if (movementVector.y < 0)
            yAngle = 360 - yAngle;

        if (Input.GetButton("Fire2") && enableDiveRoll) {
            DiveRollStart();
        }

        if (h != 0 || v != 0) {
            isMoving = true;
            myAnimator.SetBool("isMoving", isMoving);
            SmoothRotate(180 - yAngle - 45);

            if (isShooting) {
                transform.position += transform.forward * speed * shootingMoveMultiplier * Time.deltaTime;
            } else {
                transform.position += transform.forward * speed * Time.deltaTime;
            }

        } else {
            isMoving = false;
            myAnimator.SetBool("isMoving", isMoving);
        }

        Unrotate.position = transform.position;
        Unrotate.rotation = Quaternion.identity;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色控制类
/// </summary>
public class PlayerController : MonoBehaviour {

    public float speed = 5f; // 移动速度
    public float rotateSpeed = 5f; // 旋转速度
    public float shootingMoveMultiplier = 1 / 3f; // 移动射击时的减速倍率
    public float diveRollSpeedMultiplier = 1f; // 翻滚时的速度变化倍率
    public AnimationCurve diveRollSpeedCurve; // 翻滚时的速度变化曲线
    public float diveRollYPosMultiplier = 1f; // 翻滚时的人物重心Y轴方向偏移倍率
    public AnimationCurve diveRollYPosCurve; // 翻滚时的人物重心Y轴方向偏移变化曲线

    public Transform weapon; // 武器的位置状态参数
    public Transform weaponIdle; // 武器处于戒备手持时的位置状态
    public Transform weaponShoot; // 武器处于射击时的位置状态
    private Weapon weaponInfo; // 武器信息的控制脚本

    public bool isDead = false; // 角色是否死亡
    public bool isMoving = false; // 角色是否在移动中
    public bool isShooting = false; // 角色是否在射击状态
    public bool enableShooting { // 角色能否进行射击
        get { return !isDiveRolling && !isDead; }
    }
    public bool isDiveRolling = false; // 角色是否在翻滚中
    public bool enableDiveRoll = true; // 角色能否进行翻滚操作

    public Transform Unrotate; // [相机追踪用]没有旋转方向变化的人物坐标
    private Animator myAnimator; // [角色动画控制用]
    private Rigidbody rb; // [角色刚体]使用刚体进行移动，可以进行碰撞除颤

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        weaponInfo = weapon.GetComponent<Weapon>();
    }

    void SmoothRotate(float yAngle) {
        var lerpQuaternion = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, yAngle, 0), rotateSpeed * Time.deltaTime);
        transform.rotation = lerpQuaternion;
    }

    /// <summary>
    /// 通过人工设置的翻滚时人物重心Y轴方向偏移变化曲线，来更好地拟合人物的动画动作
    /// </summary>
    /// <param name="duration">状态进行的百分比[0, 1]</param>
    public void CalDiveRollMultiplier(float duration) {
        diveRollSpeedMultiplier = diveRollSpeedCurve.Evaluate(duration);
        diveRollYPosMultiplier = diveRollYPosCurve.Evaluate(duration);
    }

    void CheckDiveRoll() {
        if (Input.GetButton("Fire2") && enableDiveRoll) {
            DiveRollStart();
        }
    }

    void DiveRollStart() {
        enableDiveRoll = false;
        isDiveRolling = true;
        myAnimator.SetBool("triggerDiveRoll", isDiveRolling);
    }

    void ProcessDiveRolling() {
        var pos = transform.position + transform.forward * speed * diveRollSpeedMultiplier * Time.deltaTime;
        pos.y = 1f;
        pos.y *= diveRollYPosMultiplier;
        transform.position = pos;
    }

    public void DiveRollEnd() {
        enableDiveRoll = true;
        isDiveRolling = false;
    }

    public void CheckShoot() {
        if (Input.GetButton("Fire1")) {
            if (enableShooting) {
                isShooting = true;
                weapon.position = weaponShoot.position;
                weapon.rotation = weaponShoot.rotation;
                weaponInfo.Fire(transform.rotation.eulerAngles.y);
            } else {
                isShooting = false;
            }
        } else {
            isShooting = false;
            weapon.position = weaponIdle.position;
            weapon.rotation = weaponIdle.rotation;
        }
        myAnimator.SetBool("isShooting", isShooting);
    }

    public void CheckMove() {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var movementVector = (new Vector2(h, v)).normalized;
        var yAngle = Vector2.Angle(Vector2.right, movementVector);
        if (movementVector.y < 0)
            yAngle = 360 - yAngle;
        yAngle = 180 - yAngle - 45;

        if (h != 0 || v != 0) {
            isMoving = true;
            myAnimator.SetBool("isMoving", isMoving);

            SmoothRotate(yAngle);

            if (isShooting) {
                transform.position += transform.forward * speed * shootingMoveMultiplier * Time.deltaTime;
            } else {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        } else {
            isMoving = false;
            myAnimator.SetBool("isMoving", isMoving);
        }
    }

    private void Update() {

        CheckShoot();

        if (isDiveRolling) {
            ProcessDiveRolling();
            return; // 翻滚过程不受控制，故直接退出
        }

        CheckDiveRoll();
        CheckMove();

    }

    private void LateUpdate() {
        Unrotate.position = transform.position;
        Unrotate.rotation = Quaternion.identity;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMoving : MonoBehaviour
{

    private Vector3 lastPos;//上一次运动停止的位置
    private float lastTime;//上一次运动停止的时间
                           // Use this for initialization

    void Start()
    {
        lastPos = transform.position;
        lastTime = 0;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastPos != transform.position)//如果上次静止的位置和当前位置不相同,就更新上次静止的位置和时间
        {
            lastTime = Time.time;
            lastPos = transform.position;
        }
    }
}

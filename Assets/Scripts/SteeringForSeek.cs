using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 靠近
/// </summary>
public class SteeringForSeek : Steering {
    //需要寻找的目标物体
    public GameObject target;
    //预期速度
    private Vector3 desiredVelocity;
    //获得被操控AI角色，以便查询这个AI角色的最大速度等信息；
    private Vehicle m_vehicle;
    //最大速度；
    private float maxSpeed;
    //是否仅在二维平面上运动
    private bool isPanar;

	// Use this for initialization
	void Start () {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        isPanar = m_vehicle.isPlanar;
	}

    //计算控制向量（控制力）
    public override Vector3 Force()
    {
        //计算预期速度
        desiredVelocity = (target.transform.position - transform.position).normalized * maxSpeed;
        if (isPanar) {
            desiredVelocity.y = 0;
        }
        //返回操控向量，即预期速度与当前速度的差
        return (desiredVelocity - m_vehicle.velocity);
    }
}

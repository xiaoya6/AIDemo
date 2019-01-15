using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抵达
/// </summary>
public class SteeringForArrive : Steering {
    public bool isPlaner = true;
    public float arrivalDistance = 0.3f;
    public float charavterRddius = 1.2f;
    //当与目标小于这个距离时，开始减速
    public float slowDownDistance;
    public GameObject target;
    private Vector3 desiredVelocity;
    private Vehicle m_vehicle;
    private float maxSpeed;

	// Use this for initialization
	void Start () {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        isPlaner = m_vehicle.isPlanar;
	}

    public override Vector3 Force()
    {
        Vector3 toTarget = target.transform.position - transform.position;
        //预期速度
        Vector3 desiredVelocity;
        //返回的控制向量
        Vector3 returnForce;
        if (isPlaner) {
            toTarget.y = 0;
        }
        float distance = toTarget.magnitude;
        //如果与目标之间的距离大于所设的减速半径
        if (distance > slowDownDistance)
        {
            //预期速度是AI角色与目标之间的距离
            desiredVelocity = toTarget.normalized * maxSpeed;
            //返回预期速度与当前速度的差；
            returnForce = desiredVelocity - m_vehicle.velocity;
        }
        else {
            //计算预期速度，并返回预期速度与当前速度的差
            desiredVelocity = toTarget - m_vehicle.velocity;
            returnForce = desiredVelocity - m_vehicle.velocity;
        }
        return returnForce;
    }

    private void OnDrawGizmos()
    {
        //在目标周围画白色线框求，显示减速范围
       // Gizmos.DrawWireSphere(target.transform.position, slowDownDistance);
    }

}

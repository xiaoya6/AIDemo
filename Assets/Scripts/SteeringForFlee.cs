using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 离开
/// </summary>
public class SteeringForFlee : Steering {

    public GameObject target;
    //设置使AI角色意识到危险并开始逃跑的范围；
    public float fearDistance = 20;
    private Vector3 desiredVelocity;
    private Vehicle m_vehicle;
    private float maxSpeed;

	// Use this for initialization
	void Start () {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
	}

    public override Vector3 Force()
    {
        Vector3 tmpPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 tmpTargetPos = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        Debug.Log(Vector3.Distance(tmpPos, tmpTargetPos));
        if (Vector3.Distance(tmpPos, tmpTargetPos) > fearDistance) {
            return Vector3.zero;
        }
        desiredVelocity = (transform.position - target.transform.position).normalized * maxSpeed;
        return (desiredVelocity - m_vehicle.velocity);
        
    }
}

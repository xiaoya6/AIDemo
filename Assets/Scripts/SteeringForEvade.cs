using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 逃避
/// </summary>
public class SteeringForEvade : Steering {
    public GameObject target;
    private Vector3 desiredVelocity;
    private Vehicle m_vehicle;
    private float maxSpeed;

	// Use this for initialization
	void Start () {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
	}

    public override Vector3 Force() {
        Vector3 toTarget = target.transform.position - transform.position;
        //向前预测的时间;
        float lookaheadTime = toTarget.magnitude / (maxSpeed + target.GetComponent<Vehicle>().velocity.magnitude);
        desiredVelocity = (transform.position - (target.transform.position + target.GetComponent<Vehicle>().velocity * lookaheadTime)).normalized * maxSpeed;
        return (desiredVelocity - m_vehicle.velocity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForCohesion : Steering {

    private Vector3 desiredVelocity;
    private Vehicle m_vehicle;
    private float maxSpeed;
    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
    }
    public override Vector3 Force()
    {
        Vector3 steeringForce = Vector3.zero;
        Vector3 centerOfMass = Vector3.zero;
        int neighborCount = 0;

        foreach (GameObject s in GetComponent<Radar>().neighbors)
        {
            //如果s不是当前AI角色
            if ((s != null) && (s != this.gameObject))
            {
                //将s得朝向向量加到averageDirection之中
                centerOfMass += s.transform.position;
                //邻居数量+1
                neighborCount++;
            }

            if (neighborCount > 0)
            {
                //将累加到得朝向向量除以邻居得个数，求出平均朝向向量
                centerOfMass /= (float)neighborCount;
                desiredVelocity = (centerOfMass - transform.position).normalized * maxSpeed;

                steeringForce = desiredVelocity-m_vehicle.velocity;
            }
        }
        return steeringForce;

    }
}

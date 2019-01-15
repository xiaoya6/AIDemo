using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForCollisionAvoidance : Steering {

    public bool isPlaner;
    private Vector3 desireVelocity;
    private Vehicle m_vehicle;
    private float maxSpeed;
    private float maxForce;
    //避免障碍所产生的控制力
    public float avoidanceForce;
    //能向前看到最大的距离
    public float MAX_SEE_AHEAD = 2.0f;
    //场景中的所有碰撞体组成的数组
    private GameObject[] allColliders;
    private void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        maxForce = m_vehicle.maxForce;
        isPlaner = m_vehicle.isPlanar;
        //如果避免障碍所产生的操控力大于最大操控力,将他截断到最大操作力
        if (avoidanceForce > maxForce) {
            avoidanceForce = maxForce;
            //存储场景中的所有碰撞提，即Tag为Obstacle的那些游戏体
            allColliders = GameObject.FindGameObjectsWithTag("obstacle");
        }
    }

    public override Vector3 Force()
    {
        RaycastHit hit;
        Vector3 force = Vector3.zero;
        Vector3 velocity = m_vehicle.velocity;
        Vector3 normalizedVelocity = velocity.normalized;
        //画出一条射线，需要考查与这条射线相交的碰撞体
        Debug.DrawLine(transform.position, transform.position + normalizedVelocity * MAX_SEE_AHEAD * (velocity.magnitude / maxSpeed));
        if (Physics.Raycast(transform.position, normalizedVelocity, out hit, MAX_SEE_AHEAD * velocity.magnitude / maxSpeed)) {
            //如果射线与某个碰撞体相交，表示可能与该碰撞体发生碰撞
            Vector3 ahead = transform.position + normalizedVelocity * MAX_SEE_AHEAD * (velocity.magnitude / maxSpeed);
            force = ahead - hit.collider.transform.position;
            force *= avoidanceForce;
            if (isPlaner) {
                force.y = 0;
            }

            
        }
        return Vector3.zero;
    }
}

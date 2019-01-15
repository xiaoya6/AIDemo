using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILocomotion : Vehicle {

    //AI角色的角色控制器
    private CharacterController controller;
    //AI角色的Rigidbody
    private Rigidbody theRigidbody;
    //AI角色每次的移动速度
    private Vector3 moveDistance;
    public bool displayTrack;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        theRigidbody = GetComponent<Rigidbody>();
        moveDistance = Vector3.zero;
        base.Start();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Debug.Log("AIlocomotion"+acceleration);
        //计算速度；
        velocity += acceleration * Time.fixedDeltaTime;
        //限制速度，要低于最大速度；
        if (velocity.sqrMagnitude > sqrMaxSpeed) {
            velocity = velocity.normalized * maxSpeed;
        }

        //计算AI角色的移动距离；
        moveDistance = velocity * Time.fixedDeltaTime;

        //如果要求AI角色在水平上移动，那么将y置为0；
        if (isPlanar) {
            velocity.y = 0;
            moveDistance.y = 0;
        }
        if (displayTrack) {
            Debug.DrawLine(transform.position, transform.position + moveDistance, Color.black, 30.0f);
        }
         
        //如果已经为AI角色添加了角色控制器，那么利用角色控制器使其移动；
        if (controller != null)
        {
            controller.SimpleMove(velocity);
        }
        //如果AI角色没有角色控制器，也没有Rigidbody；
        //或AI角色拥有Rigibody，但是要由动力学的方式控制他的运动；
        else if (theRigidbody == null || theRigidbody.isKinematic)
        {
            transform.position += moveDistance;
        }
        else {
            theRigidbody.MovePosition(theRigidbody.position + moveDistance);
        }
        //更新朝向。如果速度大于一个阔值（为了防止抖动）
        if (velocity.sqrMagnitude > 0.0001f) {
            //通过当前朝向与速度方向的插值，计算新的朝向；
            Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.deltaTime);
            //将y设置为0；
            if (isPlanar) {
                newForward.y = 0;
            }
            //将向前的方向设置为新的朝向；
            transform.forward = newForward;
        }

        //播放行走动画
        //gameObject.animation.Play("walk");
	}
}

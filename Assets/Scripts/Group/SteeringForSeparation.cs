using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForSeparation : Steering {

    //可接受的距离
    public float comfortDistance = 1f;
    //当AI角色与邻居之间的距离过近时的惩罚因子；
    public float multiplierInsideComfortDistance = 2;
	
	// Update is called once per frame
	public override Vector3 Force() {
        Vector3 steeringForce = Vector3.zero;
        foreach (GameObject s in GetComponent<Radar>().neighbors)
        {
            //如果s不是当前AI角色
            if ((s != null) && (s != this.gameObject)) {
                //计算这个邻居引起的操控力（可以认为是排斥力，大小与距离成反比）
                Vector3 toNeighbor = transform.position - s.transform.position;
                float length = toNeighbor.magnitude;
                steeringForce += toNeighbor.normalized / length;
                Debug.Log(string.Format("legth:{0},comforDistance:{1}", length, comfortDistance));
                //如果两者之间距离大于可接受距离，排斥力再诚意一个额外因子
                if (length < comfortDistance) {
                    steeringForce *= multiplierInsideComfortDistance;
                }
            }
        }
        return steeringForce;

    }
}

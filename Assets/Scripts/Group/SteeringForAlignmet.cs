using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForAlignmet : Steering {

    public override Vector3 Force()
    {
        Vector3 averageDirection = Vector3.zero;
        int neighborCount = 0;
        foreach (GameObject s in GetComponent<Radar>().neighbors)
        {
            //如果s不是当前AI角色
            if ((s != null) && (s != this.gameObject))
            {
                //将s得朝向向量加到averageDirection之中
                averageDirection += s.transform.forward;
                //邻居数量+1
                neighborCount++;
            }

            if (neighborCount > 0) {
                //将累加到得朝向向量除以邻居得个数，求出平均朝向向量
                averageDirection /= (float)neighborCount;
                averageDirection -= transform.position;
            }
        }
        return averageDirection;      
    }
}

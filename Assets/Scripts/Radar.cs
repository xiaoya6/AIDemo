using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {

    //碰撞体的数组
    public Collider[] colliders;
    //计时器
    private float timer = 0;
    //邻居列表
    public List<GameObject> neighbors;
    //检测时间间隔
    public float checkInterval = 0.3f;
    //设置邻域半径
    public float detectRadius = 10f;
    //设置检测哪一层的游戏对象
    public LayerMask layersChecked;
	// Use this for initialization
	void Start () {
        neighbors = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > checkInterval) {
            neighbors.Clear();
            //查找当前AI角色邻域内的所有碰撞体
            colliders = Physics.OverlapSphere(transform.position, detectRadius);
            //对于每个检测得到的碰撞体，获取Vehicle。并且加入邻居列表中
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<Vehicle>()) {
                    neighbors.Add(colliders[i].gameObject);
                }
            }
            timer = 0;
        }
	}
}

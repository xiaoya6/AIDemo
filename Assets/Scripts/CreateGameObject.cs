using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameObject : MonoBehaviour {

    public GameObject aiObj;
    public int createNum;
    public int createType;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < createNum; i++)
        {
            GameObject aiClone = GameObject.Instantiate(aiObj);
            aiClone.SetActive(true);
            aiClone.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), 0.05f, Random.Range(-0.5f, 0.5f));
        }
        

	}
	
}

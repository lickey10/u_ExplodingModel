using UnityEngine;
using System.Collections;

public class HitHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
            other.BroadcastMessage("ApplyDamage",50);
    }
}

using UnityEngine;
using System.Collections.Generic;

public class CC_PickupHandler : MonoBehaviour {

	public List<CC_Pickup> objs = new List<CC_Pickup>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public CC_Pickup GetItem(){
		if(objs.Count > 0)
			return objs[0];
		
		return null;
	}

	public void Empty(){
		objs.Clear();
	}

	void OnTriggerEnter(Collider obj){
		if(obj.gameObject.GetComponent<CC_Pickup>() != null)
			objs.Add(obj.gameObject.GetComponent<CC_Pickup>());
	}

	void OnTriggerExit(Collider obj){
		if(obj.gameObject.GetComponent<CC_Pickup>() != null)
			objs.Remove(obj.gameObject.GetComponent<CC_Pickup>());
	}

	void OnTriggerStay(Collider obj){
		return;
	}
}

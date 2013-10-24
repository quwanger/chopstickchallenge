using UnityEngine;
using System.Collections;

public class CC_Magnetism : CC_Behaviour {
	
	public float force = -10.0f;
	public float range = 10.0f;
		
	//public magnetTypes magnetType;
	
	void FixedUpdate () {
		//if(magnetType == Chopstick){
			foreach(CC_Sushi go in sushi) {
	       		go.rigidbody.AddExplosionForce (force, transform.position, range);
	  		}
		/*}else if(magnetType = Hand){
			foreach(CC_Chopstick cs in chopsticks) {
	       		cs.rigidbody.AddExplosionForce (force, transform.position, range);
	  		}
		}*/
	}
}

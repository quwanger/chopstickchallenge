using UnityEngine;
using System.Collections;

public class HeartRate : MonoBehaviour {

	 public float speed;
	 private float startPosX; 
	 private float startPosY; 
	 private float startPosZ; 
	 public TrailRenderer trail;
	 public bool isPlayable = true;
	// Use this for initialization
	void Start () {
		trail = this.GetComponent<TrailRenderer>();
		startPosX = this.transform.position.x;
		startPosY = this.transform.position.y;
		startPosZ = this.transform.position.z;
		StartMoving();
	}
	
    void Update()
    {
		//if(Input.GetKeyDown(KeyCode.W))
			//StartMoving();
		//StartMoving();
	}

	void StartMoving()
    {
       StartCoroutine(DoMovingController());
    }
 
	IEnumerator DoMovingController()
	{
		while(isPlayable)
		{
			StartCoroutine(DoMoving());
			yield return new WaitForSeconds(3.0f);
	
		}
	}
	
    IEnumerator DoMoving()
    {
			trail.startWidth = 0.5f;
			float yScaling = 0.3f;
		
			iTween.MoveBy(gameObject, iTween.Hash("x", 5, "time", 0.5f));
	        yield return new WaitForSeconds(0.1f);
	 
			iTween.MoveBy(gameObject, iTween.Hash("x", 1, "y", yScaling*5, "time", 0.5f));
	      	yield return new WaitForSeconds(0.1f);
	 
			iTween.MoveBy(gameObject, iTween.Hash("x", 1, "y", yScaling*-12, "time", 0.5f));
	      	yield return new WaitForSeconds(0.1f);
			
			iTween.MoveBy(gameObject, iTween.Hash("x", 1, "y", yScaling*+22, "time", 0.5f));
	      	yield return new WaitForSeconds(0.1f);
			
			iTween.MoveBy(gameObject, iTween.Hash("x", 1, "y", yScaling*-30, "time", 0.5f));
	      	yield return new WaitForSeconds(0.1f);
			
			iTween.MoveBy(gameObject, iTween.Hash("x", 2, "y", yScaling*+22, "time", 0.5f));
	      	yield return new WaitForSeconds(0.1f);
			
			iTween.MoveBy(gameObject, iTween.Hash("x", 1, "y", yScaling*-12, "time", 0.5f));
	      	yield return new WaitForSeconds(0.1f);
			
			iTween.MoveBy(gameObject, iTween.Hash("x", 1, "y", yScaling*+5, "time", 0.5f));
	      	yield return new WaitForSeconds(0.1f);
			
			iTween.MoveBy(gameObject, iTween.Hash("x", 3, "time", 0.5f));
	        yield return new WaitForSeconds(1.5f);
			
			trail.startWidth = 0.0f;
		
					this.transform.position = new Vector3 (startPosX, startPosY, startPosZ);
			//yield return new WaitForSeconds(1.0f);
    }
}


/*right 5 up 5      +5
right 5 down 12	  -7
right 5 up 22     +15
right 5 down 30   -15
right 10 up 22	  +7
right 5 down 12   -5
right 5 up 5      0*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostMove : MonoBehaviour {

	public Transform[] waypoints;
	GameObject gameManager;
	int cur =0;
    private SkeletonAnimation skeletonAnimation;

	public float speed = 0.3f;

	void Start () 
	{
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
	}

	void FixedUpdate () 
	{
		if(waypoints.Length <= 0)
			return;

		// Waypoint not reached yet? them move closer

		if(transform.position != waypoints[cur].position )
		{
			Vector2 p = Vector2.MoveTowards(transform.position, waypoints[cur].position,speed * Time.deltaTime);
			GetComponent<Rigidbody2D>().MovePosition(p);
		}
		else
		{
			// Waypoint reached, select next one

			cur = (cur + 1) % waypoints.Length;
		}

		// Animation

		Vector2 dir = waypoints[cur].position - transform.position;
        //GetComponent<Animator>().SetFloat("DirX", dir.x);
        //GetComponent<Animator>().SetFloat("DirY", dir.y);

        if (skeletonAnimation != null)
        {
            if (dir.x > 0.1)
                skeletonAnimation.AnimationName = "right";
            else if (dir.x < -0.1)
                skeletonAnimation.AnimationName = "left";
            else if (dir.y > 0.1)
                skeletonAnimation.AnimationName = "up";
            else if (dir.y < -0.1)
                skeletonAnimation.AnimationName = "down";
        }
	}

	void OnTriggerEnter2D(Collider2D co)
	{
		if(co.tag == "Player")
		{
			if( !co.gameObject.GetComponent<PacmanMove>().Active)
			{
				Destroy(co.gameObject);
				gameManager.GetComponent<GameManager>().UpdateLive(1);
			}
		}
	}
}

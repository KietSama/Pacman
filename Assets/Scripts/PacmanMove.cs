using UnityEngine;
using System.Collections;

public class PacmanMove : MonoBehaviour {

	public float speed = 0.4f;
	public int live = 3;
	Vector2 dest = Vector2.zero;
	public float invulnerablePeriod = 1f;

    private SkeletonAnimation skeletonAnimation;

	bool active;

	public bool Active
	{
		get { return active; }
	}

	void Start () 
	{
		dest = transform.position;
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
	}

	void Update()
	{

	}

	void FixedUpdate () 
	{
		// move closer to destination

		Vector2 p = Vector2.MoveTowards(transform.position,dest,speed * Time.deltaTime) ;
		GetComponent<Rigidbody2D>().MovePosition(p);

		if((Vector2)transform.position == dest)
		{
            if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
            {
                if(skeletonAnimation != null)
                    skeletonAnimation.AnimationName = "up"; 
                dest = (Vector2)transform.position + Vector2.up;
            }
            if (Input.GetKey(KeyCode.DownArrow) && valid(-Vector2.up))
            {
                dest = (Vector2)transform.position - Vector2.up;
                if (skeletonAnimation != null)
                    skeletonAnimation.AnimationName = "down";
            }
			if(Input.GetKey(KeyCode.LeftArrow) && valid(Vector2.left))
			{
				dest = (Vector2)transform.position + Vector2.left;
                if (skeletonAnimation != null)
                    skeletonAnimation.AnimationName = "left";
			}
			if(Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
			{
				dest = (Vector2)transform.position + Vector2.right;
                if (skeletonAnimation != null)
                    skeletonAnimation.AnimationName = "right";
			}
		}

		// Animation

		// Vector2 dir = dest - (Vector2) transform.position;
		// GetComponentInChildren<Animator>().SetFloat("DirX", dir.x);
		// GetComponentInChildren<Animator>().SetFloat("DirY", dir.y);
	}

	bool valid(Vector2 dir)
	{
		// cast line from 'next to pac-man' to 'Pac-Man'

		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);

		if(hit.collider.name != "Pacdot" )
		{
			return(hit.collider == GetComponent<Collider2D>()); 
		}

		return true;
	}

	public void Invulnerable(bool flag)
	{
		active = flag;
	}
}

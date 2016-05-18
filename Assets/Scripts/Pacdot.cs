using UnityEngine;
using System.Collections;

public class Pacdot : MonoBehaviour {

	public int point = 10;
	GameObject gameManager;

	void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}

	void OnTriggerEnter2D(Collider2D co)
	{
		if(co.tag == "Player")
		{
			if( !co.gameObject.GetComponent<PacmanMove>().Active)
			{
				gameManager.GetComponent<GameManager>().UpdateScore(point);
				Destroy(gameObject);
			}
		}
	}
}

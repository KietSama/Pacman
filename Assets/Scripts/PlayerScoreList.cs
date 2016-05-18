using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScoreList : MonoBehaviour {

	public GameObject PlayerScoreEntryPrefab;
	ScoreManager scoreManager;
	int lastChangeCounter;
	
	void Start () 
	{
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
	}

	void Update ()
	{
		if(lastChangeCounter == scoreManager.getChangeCounter())
			return;

		lastChangeCounter = scoreManager.getChangeCounter();

		while(this.transform.childCount > 0)
		{
			Transform c = this.transform.GetChild(0);
			//c.SetParent(null);
			Destroy(c);
		}

		string[] names = scoreManager.GetPlayerName();

		foreach(string name in names)
		{
			GameObject go = (GameObject) Instantiate(PlayerScoreEntryPrefab);
			go.transform.SetParent(this.transform);

            if(go.transform.Find("Username") !=null)
			go.transform.Find("Username").GetComponent<Text>().text = name;

            if (go.transform.Find("Kills") != null)
                go.transform.Find("Kills").GetComponent<Text>().text = scoreManager.GetScore(name,"Kill").ToString();

            if (go.transform.Find("Deaths") != null)
                go.transform.Find("Deaths").GetComponent<Text>().text = scoreManager.GetScore(name, "Death").ToString();

            if (go.transform.Find("Score") != null)
                go.transform.Find("Score").GetComponent<Text>().text = scoreManager.GetScore(name,"Score").ToString();
		}
	}
}

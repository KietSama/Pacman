using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DiaglogSaveScore : MonoBehaviour {

    public InputField name;
    public GameObject gameManager;
    public GameObject scoreManager;

	public void OnClickSave()
    {
        int score = gameManager.GetComponent<GameManager>().GetScore;
        scoreManager.GetComponent<ScoreManager>().SaveScore(name.text, score.ToString());

        Application.LoadLevel("Menu");
    }

    public void OnClickCancel()
    {
        Application.LoadLevel("Menu");
    }
}

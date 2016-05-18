using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static int score = 0;
    private int getScore;
    public Text scoreText;
    public Text liveText;
    public GameObject DialogSaveScore;

    public GameObject player;
    Vector3 playerPosition;
    int live;
    float invul;
    //SpriteRenderer sr;
    MeshRenderer mr;
    int correctLayer;
    GameObject go;

    public int GetScore
    {
        get
        {
            getScore = score;
            return getScore;
        }
    }

    void Start()
    {
        scoreText.text = "Score: " + score;
        liveText.text = "Live: " + player.GetComponent<PacmanMove>().live;
        playerPosition = player.transform.position;
        live = player.GetComponent<PacmanMove>().live;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.LoadLevel("Menu");

        invul -= Time.deltaTime;
        if (invul <= 0)
        {
            player.gameObject.layer = 10;
            if (mr != null)
            {
                mr.enabled = true;
                go.GetComponent<PacmanMove>().Invulnerable(false);
            }
        }
        else
        {
            if (mr != null)
                mr.enabled = !mr.enabled;
        }

    }

    public void UpdateScore(int point)
    {
        score += point;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLive(int point)
    {
        live -= point;
        if (live >= 0)
        {
            liveText.text = "Live: " + live;
            go = (GameObject)Instantiate(player, playerPosition, Quaternion.identity);
            //invul = go.GetComponent<PacmanMove>().invulnerablePeriod;
            //sr = go.GetComponentInChildren<SpriteRenderer>();
            //go.GetComponent<PacmanMove>().Invulnerable(true);

            invul = go.GetComponent<PacmanMove>().invulnerablePeriod;
            mr = go.GetComponentInChildren<MeshRenderer>();
            go.GetComponent<PacmanMove>().Invulnerable(true);
        }
        if (live < 0)
        {
            //ClearScore();
            GameObject dialog = (GameObject)Instantiate(DialogSaveScore);
            dialog.transform.SetParent(this.transform.parent);
            dialog.transform.localPosition = Vector3.zero;
        }
    }

    public void ClearScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }
}

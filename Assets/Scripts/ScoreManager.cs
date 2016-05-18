using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	Dictionary< string, Dictionary<string, int>> playerScore;
    string[] chars = new string[10];
    int num = 10;
    int countChar;

    enum ScoreType
    {
        Kill,
        Death,
        Score
    }

    //public InputField name;
    //public InputField score;

    int changeCounter = 0;

	void Start()
	{
		Init();

        //SetScore("Quill18","Kill",9999);
        //SetScore("Quill18","Death",9999);
        //SetScore("Quill18","Score",9999);

        //SetScore("Quill17","Kill",10548);
        //SetScore("Quill17","Score",10548);

        //SetScore("Quill16","Death",666);

        //SetScore("Quill15","Kill",666);
        //SetScore("Quill15","Score",666);

        LoadSave();
	}

	void Init()
	{
		if(playerScore != null)
			return;

		playerScore = new Dictionary<string, Dictionary<string, int>> ();
	}

	public int GetScore(string username, string scoreType)
	{
		Init();

		if(playerScore.ContainsKey(username) == false)
			// We have no score record at all for this username
			return 0;

		if(playerScore[username].ContainsKey(scoreType) == false)
			return 0;

		return playerScore[username][scoreType];
	}

	public void SetScore(string username, string scoreType, int value)
	{
		Init();

		if(playerScore.ContainsKey(username) == false)
			playerScore[username] = new Dictionary<string, int>();

		playerScore[username][scoreType] = value;

		changeCounter++;
	}

	public void ChangeScore(string username, string scoreType, int amount)
	{
		Init();

		int currScore = GetScore(username,scoreType);
		SetScore(username,scoreType,currScore + amount);

		changeCounter++;
	}

	public string[] GetPlayerName()
	{
		Init();
		return playerScore.Keys.ToArray();
	}

	public string[] GetPlayerName(string ScoreType)
	{
		Init();
		return playerScore.Keys.OrderBy( n => GetScore(n,ScoreType) ).ToArray();
	}

	public int getChangeCounter()
	{
		return changeCounter;
	}

    public void SaveScore(string name, string score)
    {
        if (countChar < chars.Length)
        {
            for (int y = 0; y < chars.Length; y++)
            {
                string tempName = PlayerPrefs.GetString(y.ToString());
                if (tempName == name)
                {
                    PlayerPrefs.SetString(name, score);
                    return;
                }
            }

            for (int i = 0; i < chars.Length; i++)
            {
                string temp = PlayerPrefs.GetString(i.ToString());
                if (temp == "")
                {
                    PlayerPrefs.SetString(i.ToString(), name);
                    PlayerPrefs.SetString(name, score);
                    chars[i] = name;
                    return;
                }
            }
        }
        else
        {
            int min = 0;
            string idmin = null;

            for (int i = 0; i < chars.Length; i++)
            {
                if (int.Parse(score) > int.Parse(PlayerPrefs.GetString(chars[i])))
                {
                    min = int.Parse(PlayerPrefs.GetString(chars[i]));
                    idmin = i.ToString();
                }
            }

            if (idmin == null)
            {
                PlayerPrefs.SetString(idmin, name);
                PlayerPrefs.SetString(name, score);
            }

            if (idmin != "")
            {
                PlayerPrefs.SetString(idmin, name);
                PlayerPrefs.SetString(name, score);
            }
        }
        
    }

    public void LoadSave()
    {
        countChar = 0;
        for (int i = 0; i < chars.Length; i++)
        {
            string temp = PlayerPrefs.GetString(i.ToString());
            if (temp != "")
            {
                countChar++;
                chars[i] = temp;
            }
        }

        for (int y = 0; y < chars.Length; y++)
        {
            //Debug.Log(PlayerPrefs.GetString(y.ToString()) + " " +  PlayerPrefs.GetString(chars[y]));
            if (PlayerPrefs.GetString(y.ToString()) != "")
            {
                SetScore(
                    PlayerPrefs.GetString(y.ToString()),
                    ScoreType.Score.ToString(),
                    int.Parse(PlayerPrefs.GetString(chars[y]))
                    );
            }
        }
    }

    public void OnClickClear()
    {
        PlayerPrefs.DeleteAll();

        LoadSave();
    }

    public void OnClickExit()
    {
        Application.LoadLevel("Menu");
    }
}
 
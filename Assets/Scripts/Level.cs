using System;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    //[Range(.1f,3f)][SerializeField] private float gameSpeed = 1.0f;
    [SerializeField] private TMP_Text[] scoreTexts;
    [SerializeField] private int score = 0;
    [SerializeField] private int progressiveScore = 4;
    private int levelCount;

    private void Awake()
    {
        levelCount++;
        if (levelCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        ScoreUpdate(score);
        InvokeRepeating("PassiveScore", 1, 1);
    }

    private void PassiveScore()
    {
        ScoreUpdate(progressiveScore);
    }

    public void ScoreUpdate(int value)
    {
        score += value;
        for (int i=0; i<scoreTexts.Length; i++)
        {
            scoreTexts[i].text = score.ToString();
        }
    }

    public int GetScore()
    {
        CancelInvoke();
        return score;
    }
    private void Update()
    {
        //Time.timeScale = gameSpeed;
    }

    public void DestroyLevel()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

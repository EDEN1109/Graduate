/*
 * 게임 점수화면 관련 스크립트 입니다.
 * 
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private const int monsterNum = 4;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private TextMesh[] monsterScore = new TextMesh[monsterNum * 2 - 2];
    [SerializeField]
    private TextMesh totalScore;
    private int[] killMonsters = new int[monsterNum * 2];
    private float[] timeMonsters = new float[monsterNum * 2];
    private int score = 0;

    public static ScoreManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        ScoreManager.instance = this;
        InitScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetScore(int monsterCode, float excerciseTime)
    {
        score++;
        killMonsters[monsterCode]++;
        timeMonsters[monsterCode] += excerciseTime;
        PlayerPrefs.SetInt(monsterCode + "Kill", killMonsters[monsterCode]);
        PlayerPrefs.SetFloat(monsterCode + "Time", timeMonsters[monsterCode]);

        scoreText.text = "SCORE : " + score;
        //Debug.Log(className + "Kill : " + killMonsters[monsterCode]);
        //Debug.Log(className + "Time : " + timeMonsters[monsterCode]);
    }

    public void SetTotalScore()
    {
        int totalKill = 0;
        float totalTime = 0f;
        string[] monsterType = { "상하", "좌우", "원", "팔자", "원근", "사면" };

        for (int i = 0; i < (monsterNum - 1) * 2 - 1; i++)
        {
            int killNum = 0;
            float timeVal = 0f;

            if (PlayerPrefs.HasKey(i + "Kill"))
            {
                killNum = PlayerPrefs.GetInt(i + "Kill");
            }
            if (PlayerPrefs.HasKey(i + "Time"))
            {
                timeVal = PlayerPrefs.GetFloat(i + "Time");
            }

            monsterScore[i].text = monsterType[i] + "운동 횟수 " + killNum + " 운동시간 " + timeVal.ToString("N1") + "초";

            totalKill += killNum;
            totalTime += timeVal;
        }

        int slopeKill = 0;
        float slopeTime = 0f;
        for(int i = 6; i < 8; i++)
        {
            if (PlayerPrefs.HasKey(i + "Kill"))
            {
                slopeKill += PlayerPrefs.GetInt(i + "Kill");
            }
            if (PlayerPrefs.HasKey(i + "Time"))
            {
                slopeTime += PlayerPrefs.GetFloat(i + "Time");
            }
        }
        monsterScore[5].text = monsterType[5] + "운동 횟수 " + slopeKill + " 운동시간 " + slopeTime.ToString("N1") + "초";

        totalKill += slopeKill;
        totalTime += slopeTime;

        totalScore.text = "눈 운동 총합 횟수 " + totalKill + " 운동시간 " + totalTime.ToString("N1") + "초";
        // 추후 눈 깜박임, 명암운동 코드 필요
    }

    public void SaveScore()
    {
        for(int i = 0; i < monsterNum * 2; i++)
        {
            int loadKillNum = 0;
            float loadTimeVal = 0f;
            int nowKillNum;
            float nowTimeVal;

            if(PlayerPrefs.HasKey(i + "LoadKill"))
            {
                loadKillNum = PlayerPrefs.GetInt(i + "LoadKill");
            }
            if (PlayerPrefs.HasKey(i + "LoadTime"))
            {
                loadTimeVal = PlayerPrefs.GetFloat(i + "LoadTime");
            }

            nowKillNum = PlayerPrefs.GetInt(i + "Kill");
            nowTimeVal = PlayerPrefs.GetFloat(i + "Time");

            PlayerPrefs.SetInt(i + "LoadKill", loadKillNum + nowKillNum);
            PlayerPrefs.SetFloat(i + "LoadTime", loadTimeVal + nowTimeVal);

            PlayerPrefs.DeleteKey(i + "Kill");
            PlayerPrefs.DeleteKey(i + "Time");

            // 저장되어 있던 값과 저장할 값 출력
            //Debug.Log(i + "LoadKill : " + loadKillNum);
            //Debug.Log(i + "NowKill : " + nowKillNum);
            //Debug.Log(i + "LoadTime : " + loadTimeVal);
            //Debug.Log(i + "NowTime : " + nowTimeVal);

        }

        PlayerPrefs.Save();
    }

    private void InitScore()
    {
        for(int i = 0; i < monsterNum * 2; i++)
        {
            killMonsters[i] = 0;
            timeMonsters[i] = 0;
        }
    }
}

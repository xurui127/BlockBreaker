using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [Header("------------- Player Settings -------------")]
    [SerializeField] private int lives = 3;
    private int highScore = 0;
    private int points = 0;

    /// <summary>
    /// Levels 
    /// </summary>
    [Header("------------- Level -------------")]

    [SerializeField] private int bricks = 0;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private int currentLevel = 0;
    private int reStartLevel;
    private GameObject levelObj;
    public bool timerOn = false;

    //Ball Buffer 
    [Header("------------- Game Settings -------------")]
    [SerializeField] private BallManager ballManager;
    [SerializeField] private BallBehaviour ball;
    [SerializeField] private PaddleBehaviour player;
    [SerializeField] private TransationEffect transationEffect;
    [SerializeField] public GameObject buffer;
    [SerializeField] private GameObject extraBall;
    [SerializeField] private float befferTimer;
    [SerializeField] private float ballHitDuration;
    [SerializeField] private float ballHitStrength;

    public bool isBuffing;
    public int sumBall = 1;

    // UI 
    [Header("------------- UI -------------")]
    [SerializeField] private SelectMenu selectMenu;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI highScoreTxt;
    [SerializeField] private TextMeshProUGUI stateTXT;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private TextMeshProUGUI liveTxt;
    [SerializeField] private string preTextScore = "SCORE: ";
    [SerializeField] private string preTextHightScore = "HIGH SCORE: ";
    [SerializeField] private string preTextLvl = "LEVEL : ";
    [SerializeField] private string preTextLive = "LIVES : ";
    [Header("------------- Timer -------------")]
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private string preTextTimer = "TIME: ";
    [SerializeField] public float timeLeft;
    [SerializeField] private float[] totalTimeleft;
    private enum BuffeStates
    {
        DEAULT = 0,
        MEGABALL = 1,
        MUTIBALL = 2,
        SPEEDBALL = 3,
        TINYBALL = 4,
        MEGAPADDLE = 5,
        TINYPADDLE = 6
    }
    [SerializeField] private BuffeStates states = BuffeStates.DEAULT;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }


    }
    void Start()
    {
        ballManager = BallManager.instance;
        transationEffect = TransationEffect.instance;
        scoreTxt.text = preTextScore + points.ToString("D6");
        liveTxt.text = preTextLive + lives.ToString("D1");
        //LoadLevel(currentLevel);
        StartCoroutine( LevelLoadForSeconds(currentLevel));
        highScoreTxt.text = preTextHightScore + LoadHightScore().ToString("D6");
        timeLeft = totalTimeleft[currentLevel];
        isBuffing = false;

    }
    private void Update()
    {
        if (TimerCounter() == true)
        {
            Death();
        }

        BufferTimer();

        if (Input.GetButton("Cancel"))
        {
            selectMenu.GamePause();
            selectMenu.PanelToggle(2);
        }
    }
    #region Timer
    bool TimerCounter()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                timerOn = false;
                return true;
            }
        }
        return false;
    }
    void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerTxt.text = string.Format(preTextTimer + "{0:00} : {1:00}", minutes, seconds);
    }
    private void TimerReset()
    {
        timerOn = false;
        timeLeft = totalTimeleft[currentLevel];
        UpdateTimer(timeLeft);
    }

    public float TimerBoundary(float multiplier)
    {
        timeLeft += timeLeft * multiplier;
        if (timeLeft >= totalTimeleft[currentLevel])
        {
            return timeLeft = totalTimeleft[currentLevel];
        }
        return timeLeft;
    }
    #endregion
    private void Init()
    {
        ball.Init();
        BehaviourReset();
    }
    #region Level
    private void LoadLevel(int level)
    {
        //if (levelObj)
        //{
        //    Destroy(levelObj);
        //    //DestroyImmediate(buffer,true);
        //    ballManager.DestroyExtrlBall();
        //    BuffeTimerReset();

        //}
        //BehaviourReset();
        stateTXT.gameObject.SetActive(false);
        bricks = 0;
        sumBall = 1;
        levelObj = Instantiate(levels[level]);

        //levelObj = Instantiate(levels[currentLevel]);
    }
    private IEnumerator ShowLevelInfo(int level)
    {
        levelTxt.gameObject.SetActive(true);
        levelTxt.text = preTextLvl + (level+1).ToString("D2");
       // levelTxt.text = string.Format(preTextLvl + "{0}" , level.ToString());
        yield return new WaitForSecondsRealtime(2f);
        levelTxt.gameObject.SetActive(false);
        yield return null;
    }

    private IEnumerator LevelLoadForSeconds(int level)
    {
        if (levelObj)
        {
            Destroy(levelObj);
            //DestroyImmediate(buffer,true);
            ballManager.DestroyExtrlBall();
            BuffeTimerReset();
        }
        StartCoroutine(ShowLevelInfo(currentLevel));
        ball.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(2f);
        ball.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        LoadLevel(level);
        yield return null;
    }

    public void AddBrick()
    {
        bricks++;
    }
    public void GoNextLevel()
    {
        if (bricks <= 0)
        {
            if (currentLevel < levels.Length - 1)
            {
                currentLevel++;
                reStartLevel = currentLevel;

            }
            else
            {

            }

            StartCoroutine(LevelLoadForSeconds(currentLevel));
            Init();


            //LoadLevel(currentLevel);
            // BehaviourReset();
            TimerReset();
        }
    }
    public void ReduceBrick()
    {
        bricks--;
    }
    #endregion
    #region Score 
    public void AddPoints()
    {
        points += 100;

        // bricks--;

        scoreTxt.text = preTextScore + points.ToString("D6");
        if (points > highScore)
        {
            highScore = points;
            highScoreTxt.text = preTextHightScore + highScore.ToString("D6");
        }
        //if (bricks <= 0)
        //{
        //    if (currentLevel < levels.Length - 1)
        //    {
        //        currentLevel++;
        //        reStartLevel = currentLevel;

        //    }
        //    else
        //    {

        //    }

        //    Init();
        //    LoadLevel(currentLevel);
        //    TimerReset();
        //}
    }
    public void SaveHightScore()
    {
        PlayerPrefs.SetInt("hightscore", highScore);

    }
    public int LoadHightScore()
    {

        highScore = PlayerPrefs.GetInt("hightscore", 0);
        return highScore;
    }
    #endregion
    #region Game Logic
    private void GameOver()
    {
        ball.gameObject.SetActive(false);
        if (points >= highScore)
        {
            SaveHightScore();

        }
        //scoreTxt.text = "GAME OVER \n" + preTextScore + points.ToString("D6");
        endPanel.SetActive(true);
    }
    public void Death()
    {
        lives--;
        liveTxt.text = preTextLive + lives.ToString("D1");
        if (lives > 0)
        {
            Init();
            ball.gameObject.SetActive(true);
            //BehaviourReset();
            timerOn = false;
            BuffeTimerReset();
            ballManager.DestroyExtrlBall();
            timeLeft = totalTimeleft[currentLevel];
        }
        else
        {
            timerOn = false;
            GameOver();
        }


    }
    public void ReStart()
    {

        
        lives = 3;
        liveTxt.text = preTextLive + lives.ToString("D1");
        //LoadLevel(reStartLevel);
        StartCoroutine(LevelLoadForSeconds(reStartLevel));
        Init();
        ball.ActiveBall();

    }
    public void StartOver()
    {
        currentLevel = 0;
        lives = 3;
        liveTxt.text = preTextLive + lives.ToString("D1");
        // LoadLevel(currentLevel);
        //LevelLoadForSeconds(currentLevel);
        StartCoroutine(LevelLoadForSeconds(currentLevel));
        Init();
        ball.ActiveBall();

    }
    #endregion
    #region Buffe Setting
    public void InitBuffer(Transform parent)
    {
        int powerRate = Random.Range(10, 100);
        if (powerRate <= 18) //18
        {
            if (isBuffing == false && states != BuffeStates.MUTIBALL && bricks >= 8)
            {
                Instantiate(buffer, parent.position, Quaternion.identity);
            }
        }
    }
    public void ChangeStates()
    {
        states = (BuffeStates)Random.Range(1, 6);
        //states = BuffeStates.SPEEDBALL;
        //Debug.Log(states);
    }
    public void BallBehaviours()
    {
        //-----------BehavioursBall ---------------
        //ball.MegaBall();
        //sumBall = 3;
        //states = BuffeStates.MutiBalls;
        //isBuffing = true;
        //ball.MultiBalls();
        //ball.SpeedBall();
        //player.MegaPaddle();
        //player.TinyPaddle();
        //ball.TinyBall();

        switch (states)
        {
            case BuffeStates.DEAULT:
                BehaviourReset();
                Debug.Log("In Default");
                break;
            case BuffeStates.MEGABALL:
                isBuffing = true;
                ball.MegaBall();
                Debug.Log("In MegaBall");

                break;
            case BuffeStates.MUTIBALL:

                sumBall = 3;
                ball.MultiBalls();
                Debug.Log("In MutiBalls");

                break;
            case BuffeStates.SPEEDBALL:
                isBuffing = true;
                ball.SpeedBall();
                Debug.Log("In SpeedBall");

                break;
            case BuffeStates.TINYBALL:
                isBuffing = true;
                ball.TinyBall();
                Debug.Log("In TinyBall");

                break;
            case BuffeStates.MEGAPADDLE:
                isBuffing = true;
                player.MegaPaddle();
                Debug.Log("In MegaPaddle");

                break;
            case BuffeStates.TINYPADDLE:
                isBuffing = true;
                player.TinyPaddle();
                Debug.Log("In TinyPaddle");

                break;
        }
    }
    private void BehaviourReset()
    {
        sumBall = 1;
        ball.ResetBall();
        player.ResetPaddle();
        isBuffing = false;
    }

    private bool BufferTimer()
    {
        if (isBuffing == true && states != BuffeStates.MUTIBALL)
        {
            befferTimer -= Time.deltaTime;
            stateTXT.gameObject.SetActive(true);
            float seconds = Mathf.FloorToInt(befferTimer % 60);
            stateTXT.text = string.Format(states.ToString() + " \n" + "{00} S", seconds);
            if (befferTimer <= 0)
            {
                BuffeTimerReset();
                //BehaviourReset();
                ball.NormalBall();
                player.NormalPaddle();
                return false;
            }
        }
        else if (isBuffing == true && states == BuffeStates.MUTIBALL)
        {
            stateTXT.gameObject.SetActive(true);
            stateTXT.text = string.Format(states.ToString() + " \n" + "{00}", sumBall);
        }

        return true;
    }

    private void BuffeTimerReset()
    {
        isBuffing = false;
        states = BuffeStates.DEAULT;
        stateTXT.gameObject.SetActive(false);
        //BallBehaviours();
        befferTimer = 10;
    }

    public void IsShakeCamera()
    {
        if (states == BuffeStates.SPEEDBALL || states == BuffeStates.MEGABALL)
        {
            transationEffect.ShakeCaemera(ballHitDuration, ballHitStrength);
        }
    }

    #endregion
}

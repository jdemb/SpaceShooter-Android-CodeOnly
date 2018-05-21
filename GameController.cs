using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public Vector3 spawnValues2;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public bool hp;
    private GameObject clone;
    public GameObject HPaura;

    public TextMeshPro scoreText;
    public int score;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText bossText;
    public TextMeshPro bosshp;
    public GameObject restartButton;

    public bool gameOver;
    private bool bossOver;
    private bool restart;
    public bool TimeCheck;
    private int waves;
    public BGscroller bgscroller;
    public BossFight bossFight;
    public GameObject boss;
    GameObject hazard;
    public DragMe dragme;
    private int oldScore;
    static readonly object locker = new object();


    void Start()
    {
        restartButton.SetActive(false);
        bosshp.text = "";
        bossText.text = "";
        waves = 1;
        TimeCheck = false;
        hp = false;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        Updatescore(score);
        StartCoroutine (FightController());
    }

    void Update()
    {
      //  if(restart)
     //   {
     //       if(Input.GetKeyDown (KeyCode.R))
     //       {
      //          Application.LoadLevel(Application.loadedLevel);
      //      }
    //    }
        if (TimeCheck && dragme.checkFire())
        {
            
                Time.timeScale = 0.2F;
            
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        if (TimeCheck==false && dragme.checkFire())
        {
           
                Time.timeScale = 1.0F;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        if (gameOver || bossOver )
        {
            //restartText.text = "Press 'R' for Restart";
            restartButton.SetActive(true);
            restart = true;
            
        }
    }

    IEnumerator FightController()
    {
        yield return new WaitForSeconds(startWait);

        yield return StartCoroutine(SpawnWaves(1, false));
        yield return StartCoroutine(SpawnWaves(3, true));
        yield return StartCoroutine(spawnMines());
        yield return StartCoroutine(SpawnWaves(2, true));
        spawnEnemyRocketShooters();
        yield return new WaitForSeconds(7);
        yield return  StartCoroutine(SpawnWaves(2, true));
        yield return new WaitForSeconds(2);
        spawnMinerSpawners(1);
        yield return new WaitForSeconds(3);
        spawnMinerSpawners(2);
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(SpawnWaves(2, true));
        yield return StartCoroutine(spawnMines());
        spawnEnemyRocketShooters();
        yield return new WaitForSeconds(17);

        bossText.text = "Boss Incoming";
        bgscroller.scrollSpeed = -200;
        yield return new WaitForSeconds(3);
        bossText.text = "";
        bgscroller.scrollSpeed = -0.35f;
        Instantiate(boss, boss.transform.position, boss.transform.rotation);      
    }

    void spawnMinerSpawners(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            hazard = hazards[14];
            Instantiate(hazard, spawnPosition, spawnRotation);
        }
    }

    void spawnEnemyRocketShooters()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z-3.5f);
        Quaternion spawnRotation = Quaternion.identity;
        hazard = hazards[12];
        Instantiate(hazard, spawnPosition, spawnRotation);
        spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z-3.5f);
        spawnRotation = Quaternion.identity;
        Instantiate(hazard, spawnPosition, spawnRotation);
    }

    IEnumerator SpawnWaves(int amountOfWaves, bool extended)
    {
        for(int i = 0; i < amountOfWaves; i++)
        {
            for (int j = 0; j < hazardCount; j++)
            {
                if (!extended)
                {
                    hazard = hazards[Random.Range(0, 5)];
                }
                else
                {
                    hazard = hazards[Random.Range(0, 11)];
                }
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    IEnumerator spawnMines()
    {
        Vector3 spawnPosition = new Vector3(spawnValues2.x, spawnValues2.y, spawnValues2.z);
        Quaternion spawnRotation = Quaternion.identity;
        hazard = hazards[13];
        for (int i = 0; i < 10; i++)
        {
            Instantiate(hazard, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(waveWait);
    }

    public void Addscore(int newScoreValue)
    {
        score += newScoreValue;
        StartCoroutine(Updatescore(score));
    }

    public IEnumerator Updatescore(int newScore)
    {
        lock(locker)
        {
            for (int i = 1; i <= newScore - oldScore; i++)
            {
                int newNumber = oldScore + i;
                int tmp = 8 - newNumber.ToString().Length;
                scoreText.text = new string('0', tmp) + newNumber.ToString();
                yield return new WaitForSeconds(0.05f);
            }
            oldScore = newScore;
        }
    }

    public void GameOver()
    {
            gameOverText.text = "Game Over";
            gameOver = true;
    }
    public void BossOver()
    {
        gameOverText.text = "You did it!";
        bossOver = true;

    }
    public void HigherHP()
    {
        if (hp == false)
        {
            hp = true;
            clone = (GameObject)Instantiate(HPaura, transform.position, transform.rotation);
        }
    }
    public void LowerHP()
    {
        hp = false;
        Destroy(clone);
    }
    public void SlowMotion()
    {
        if(TimeCheck==false)
        StartCoroutine(SlowMo());
    }
    IEnumerator SlowMo()
    {
        TimeCheck = true;
        
        yield return new WaitForSecondsRealtime(3);
        TimeCheck = false;
        
        yield return new WaitForSecondsRealtime(1);
        
    }
    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
}

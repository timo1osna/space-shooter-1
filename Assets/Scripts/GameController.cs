using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
	public float waveTextWait;
	public float difficulty;


    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
	public GUIText waveText;
	public GUIText finalScoreText; 


	public static GameController instance;

    private bool gameOver;
    private bool restart;
	private int waveCounter;

    public int score;

    void Start()
    {

		difficulty = 1.0f;
		waveCounter = 1;
        gameOver = false;
        restart = false;
		finalScoreText.text = "";
		waveText.text = "";
        gameOverText.text = "";
        restartText.text = "";
        StartCoroutine( SpawnWaves() );
        score = 0;
        UpdateScore();
		instance = this;
    }

    void Update()
    {
        if( restart )
        {
            if( Input.GetKeyDown(KeyCode.R) )
            {
                //Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while(true)
        {
			waveText.text = "Wave " + waveCounter;
			yield return new WaitForSeconds(waveTextWait);
			waveText.text = "";
			waveCounter++;

            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

			difficulty = difficulty + 0.1f ;
			

            if( gameOver )
            {
                restartText.text = "Press 'R' for Restart!";
                restart = true;
                break;
            }

        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }



    public void GameOver()
    {
			
		gameOverText.text = "Game over!";
		finalScoreText.text = "Your final Score is " + score;
		gameOver = true;

    }
}

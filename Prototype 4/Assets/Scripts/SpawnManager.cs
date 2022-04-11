using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;

    public int enemyCount = 1;
    public int waveNumber = 1;

    public GameObject powerupPrefab;

    public Text waveText;
    public Text winText;

    public bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {
       SpawnEnemyWave(waveNumber);
       SpawnPowerup(1);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length; 

        if(Input.GetKeyDown(KeyCode.R) && hasWon)
        {
             Time.timeScale = 1f;
             SceneManager.LoadScene("Prototype 4 Finished");
        }

        if(enemyCount == 0)
        {
            waveNumber++;
            waveText.text = "Current Wave: " + waveNumber;
            if(waveNumber > 10)
            {
                Time.timeScale = 0f;
                winText.gameObject.SetActive(true);
                hasWon = true;
                
            }
            SpawnEnemyWave(waveNumber);
            SpawnPowerup(1);
        }
    }

    private void SpawnPowerup(int powerupsToSpawn)
    {
        for (int i = 0; i < powerupsToSpawn; i++)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }
}

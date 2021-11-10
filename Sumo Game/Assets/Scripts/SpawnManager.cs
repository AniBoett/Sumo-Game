using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float spawnRange = 9;
	public int enemyCount;
	public int waveNumber = 1;
    
    // Start is called before the first frame update
    void Start()
    {
		//Game gets increasingly harder
       SpawnEnemyWave(waveNumber);
    }

	void SpawnEnemyWave(int enemiesToSpawn) 
	{
		for (int i = 0; i < enemiesToSpawn; i++) 
		{  
			//enemies spawn at random locations
			Instantiate(enemyPrefab, GenerateSparnPosition(), enemyPrefab.transform.rotation);
		}
	}

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
		if (enemyCount == 0)
		{
			waveNumber++;
			SpawnEnemyWave(waveNumber);
		}
    }

	private Vector3 GenerateSparnPosition () {
		float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
		return randomPos; }
}

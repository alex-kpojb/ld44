using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnController : MonoBehaviour
{
    public GameStateSO stateSO;

    GameObject[] SpawnersGO = new GameObject[] { };
    List<Vector2> SpawnerPositions = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        SpawnersGO = GameObject.FindGameObjectsWithTag("Spawner");

        foreach (var item in SpawnersGO)
        {
            SpawnerPositions.Add(item.transform.position);
        }

        StartCoroutine(StartSpawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator StartSpawning()
    {
        while (true)
        {

            float pauseBeforeSpawnRandom = Random.Range(stateSO.pauseBeforeSpawnMin, stateSO.pauseBeforeSpawnMax);
            yield return new WaitForSeconds(pauseBeforeSpawnRandom);

            if (stateSO.mobsCurrentCounter < stateSO.mobsMaxScene)
            {
                if (stateSO.mobsSpawnedScene < stateSO.mobsMeleeTestWave[stateSO.currentWave])
                {
                    stateSO.mobsCurrentCounter++;
                    stateSO.mobsSpawnedScene++;
                    int spawnerRandomId = Random.Range(0, SpawnerPositions.Count);
                    Instantiate(stateSO.prefabMelee, SpawnerPositions[spawnerRandomId], Quaternion.identity);
                }
                else
                {
                    while (stateSO.mobsCurrentCounter > 0)
                        yield return null;

                    if (stateSO.currentWave < stateSO.mobsMeleeTestWave.Count - 1)
                    {
                        stateSO.mobsCurrentCounter = 0;
                        stateSO.mobsSpawnedScene = 0;
                        stateSO.currentWave++;
                        yield return new WaitForSeconds(stateSO.pauseBeforeWave);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}

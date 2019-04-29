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
        stateSO.currentWave = 0;

        stateSO.mobsCurrentCounter = 0;
        stateSO.mobsMeleeSpawned = 0;
        stateSO.mobsTurretSpawned = 0;
        stateSO.mobsCreeperSpawned = 0;

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
            if (stateSO.mobsCurrentCounter + 3 < stateSO.mobsMaxScene)
            {
                float pauseBeforeSpawnRandom = Random.Range(stateSO.pauseBeforeSpawnMin, stateSO.pauseBeforeSpawnMax);

                yield return new WaitForSeconds(pauseBeforeSpawnRandom);
                StartCoroutine(SpawnMewlee());
                StartCoroutine(SpawnTurret());
                StartCoroutine(SpawnCreeper());

                if (stateSO.mobsMeleeSpawned >= stateSO.mobsMeleeTestWave[stateSO.currentWave]
                    & stateSO.mobsTurretSpawned >= stateSO.mobsTurretTestWave[stateSO.currentWave]
                    & stateSO.mobsCreeperSpawned >= stateSO.mobsCreeperTestWave[stateSO.currentWave])
                {
                    while (stateSO.mobsCurrentCounter > 0)
                        yield return null;

                    if (stateSO.currentWave < stateSO.mobsMeleeTestWave.Count - 1)
                    {
                        stateSO.mobsCurrentCounter = 0;
                        stateSO.mobsMeleeSpawned = 0;
                        stateSO.mobsTurretSpawned = 0;
                        stateSO.mobsCreeperSpawned = 0;
                        stateSO.currentWave++;
                        yield return new WaitForSeconds(stateSO.pauseBeforeWave);
                    }
                    else
                    {
                        
                        break;
                    }
                }
            }
            yield return null;
        }
        stateSO.chestPrice *= 2;
        SceneController.instance.NextScene();
        yield return null;
    }

    IEnumerator SpawnMewlee()
    {
        float pauseBeforeSpawnRandom = Random.Range(stateSO.pauseBeforeSpawnMin, stateSO.pauseBeforeSpawnMax);
        yield return new WaitForSeconds(pauseBeforeSpawnRandom);
        if (stateSO.mobsMeleeSpawned < stateSO.mobsMeleeTestWave[stateSO.currentWave])
        {
            stateSO.mobsCurrentCounter++;
            stateSO.mobsMeleeSpawned++;
            int spawnerRandomId = Random.Range(0, SpawnerPositions.Count);
            Instantiate(stateSO.prefabMelee, SpawnerPositions[spawnerRandomId], Quaternion.identity);
        }
        yield return null;
    }

    IEnumerator SpawnTurret()
    {
        float pauseBeforeSpawnRandom = Random.Range(stateSO.pauseBeforeSpawnMin, stateSO.pauseBeforeSpawnMax);
        yield return new WaitForSeconds(pauseBeforeSpawnRandom);
        if (stateSO.mobsTurretSpawned < stateSO.mobsTurretTestWave[stateSO.currentWave])
        {
            stateSO.mobsCurrentCounter++;
            stateSO.mobsTurretSpawned++;
            int spawnerRandomId = Random.Range(0, SpawnerPositions.Count);
            Instantiate(stateSO.prefabTurret, SpawnerPositions[spawnerRandomId], Quaternion.identity);
        }
        yield return null;
    }

    IEnumerator SpawnCreeper()
    {
        float pauseBeforeSpawnRandom = Random.Range(stateSO.pauseBeforeSpawnMin, stateSO.pauseBeforeSpawnMax);
        yield return new WaitForSeconds(pauseBeforeSpawnRandom);
        if (stateSO.mobsCreeperSpawned < stateSO.mobsCreeperTestWave[stateSO.currentWave])
        {
            stateSO.mobsCurrentCounter++;
            stateSO.mobsCreeperSpawned++;
            int spawnerRandomId = Random.Range(0, SpawnerPositions.Count);
            Instantiate(stateSO.prefabCreeper, SpawnerPositions[spawnerRandomId], Quaternion.identity);
        }
        yield return null;
    }
}

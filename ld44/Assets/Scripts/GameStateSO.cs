using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateSO", menuName = "GameStateSO", order = 51)]
public class GameStateSO : ScriptableObject
{
    //default
    float moneyDefault = 0;
    float sceneDefault = 0;
    float jumpForceDefault = 800f;
    float maxJumpsDefault = 1;
    float walkForceDefault = 0.1f;
    float dashMaxDefault = 1;
    float dashTimeDefault = 0.1f;
    float dashSpeedDefault = 18f;

    public GameObject prefabMelee;
    public GameObject prefabTurret;
    public GameObject prefabCreeper;

    public GameObject prefabBonus;
    public GameObject prefabBullet;

    public float moneyCurrent = 0;
    public float sceneCurrent = 0;

    //another settings

    //testScene
    public float pauseBeforeWave = 1f;
    public float pauseBeforeSpawnMin = 0.2f;
    public float pauseBeforeSpawnMax = 0.9f;

    public float mobsMaxScene = 25f;
    public float mobsMeleeSpawned = 0;
    public float mobsTurretSpawned = 0;
    public float mobsCreeperSpawned = 0;
    public float mobsCurrentCounter = 0;

    public int currentWave = 0;

    public List<float> mobsMeleeTestWave = new List<float>();
    public List<float> mobsTurretTestWave = new List<float>();
    public List<float> mobsCreeperTestWave = new List<float>();

    //Player settings
    public float jumpForce = 800f;
    public float maxJumps = 2;
    public float walkForce = 0.2f;
    public float dashMax = 2;
    public float dashTime = 0.2f;
    public float dashSpeed = 25f;

    private void OnEnable()
    {
        Cheat();
        //Reset();
    }

    private void OnDisable()
    {

    }

    void Cheat()
    {
        moneyCurrent = 500;
        sceneCurrent = 0;
        mobsMeleeSpawned = 0;
        mobsTurretSpawned = 0;
        mobsCreeperSpawned = 0;
        mobsCurrentCounter = 0;
        mobsCurrentCounter = 0;
        currentWave = 0; //0-2

        jumpForce = 800f;
        maxJumps = 2;
        walkForce = 0.2f;
        dashMax = 2;
        dashTime = 0.1f;
        dashSpeed = 25f;
}

    void Reset()
    {
        moneyCurrent = moneyDefault;
        sceneCurrent = sceneDefault;
        mobsMeleeSpawned = 0;
        mobsTurretSpawned = 0;
        mobsCreeperSpawned = 0;
        mobsCurrentCounter = 0;
        mobsCurrentCounter = 0;
        currentWave = 0;

        jumpForce = jumpForceDefault;
        maxJumps = maxJumpsDefault;
        walkForce = walkForceDefault;
        dashMax = dashMaxDefault;
        dashTime = dashTimeDefault;
        dashSpeed = dashSpeedDefault;
    }
}



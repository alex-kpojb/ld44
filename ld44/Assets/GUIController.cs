using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIController : MonoBehaviour
{
    public GameStateSO stateSO;
    public TMP_Text textMoney;
    public TMP_Text textWave;
    public TMP_Text textcurrentMobs;
    public TMP_Text textmelee;
    public TMP_Text textturret;
    public TMP_Text textcreeper;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textMoney.text = $"${stateSO.moneyCurrent}";
        textWave.text = $"{stateSO.currentWave+1}/{stateSO.mobsMeleeTestWave.Count}";

        textcurrentMobs.text = $"Mobs left: {stateSO.mobsCurrentCounter}";
        textmelee.text = $"{stateSO.mobsMeleeSpawned}/{stateSO.mobsMeleeTestWave[stateSO.currentWave]}";
        textturret.text = $"{stateSO.mobsTurretSpawned}/{stateSO.mobsTurretTestWave[stateSO.currentWave]}";
        textcreeper.text = $"{stateSO.mobsCreeperSpawned}/{stateSO.mobsCreeperTestWave[stateSO.currentWave]}";
    }
}

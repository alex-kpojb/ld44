using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIController : MonoBehaviour
{
    public GameStateSO stateSO;
    public TMP_Text textMoney;
    public TMP_Text textWave;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textMoney.text = $"${stateSO.moneyCurrent}";
        textWave.text = $"{stateSO.currentWave+1}/{stateSO.mobsMeleeTestWave.Count}";
    }
}

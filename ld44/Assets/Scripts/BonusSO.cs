using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BonusSO", menuName = "BonusSO", order = 51)]
public class BonusSO : ScriptableObject
{
    [System.Serializable]
    public struct Bonus
    {
        public Sprite sprite;
        public string name;
        public int price;
        public float value;
    }

    public List<Bonus> Bonuses = new List<Bonus>();

}

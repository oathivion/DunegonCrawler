using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRing : MonoBehaviour
{
    [SerializeField] private int speedModifyer;
    private StatsSaveSystem statsSaveSystem;

    void Awake () {
        statsSaveSystem = FindObjectOfType<StatsSaveSystem>();

        statsSaveSystem.ModifyStat("dexterity", speedModifyer);
    }
}

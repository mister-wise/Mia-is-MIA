using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{

    [SerializeField] public int actionPoints;
    public bool gamePhaseB {get; private set;}
    public bool gameEnded {get; private set;}

    public void ReduceAP (int amount)
    {
        actionPoints -= amount;
        OnAPChange();
    }


    public void OnAPChange ()
    {
        if (actionPoints <= 0 && !gamePhaseB )
        {
            gamePhaseB = true;
        }
        if (actionPoints <= 0 && gamePhaseB)
        {
            gameEnded = true;
        }
    }




}

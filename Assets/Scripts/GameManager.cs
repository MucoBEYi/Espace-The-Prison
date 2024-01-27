using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameState;        //true = oyun çalýþýyor




    private void Start()
    {
        gameState = true;
    }


    //oyun bittiðinde çalýþacak komutlar
    public void GameWin()
    {
        gameState = false;
        print("oyunu KAZANDIN");

    }

}


//düne kadar bu script dopdoluydu vay bee
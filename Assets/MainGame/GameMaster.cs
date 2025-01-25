using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    public UnityEvent GameOver;

    private int lives = 3;

    private int trucksCount = 0;

    private int mailPerTruck = 5;
    private float timePerTruck = 45f;

    private int mailIncreaseRange = 2;
    private int timeDecreaseRange = 3;

    private int mailPerTruckMax = 25;
    private int timePerTruckMinimum = 10;


    private IEnumerator truckSpawnRoutine;


    private List<bool> trucksSlots = new List<bool>()
    {
        false,
        true,
        false,
        true
    };


    void Start()
    {
        InvokeRepeating("SpawnTruck", 0f, 3f);
    }

    private void SpawnTruck()
    {
        Debug.Log("y");

        trucksCount++;
        int searchIndex = Random.Range(0, trucksSlots.Count);

        while (trucksSlots[searchIndex] == false)
        {
            searchIndex = LoopIndex(searchIndex, trucksSlots.Count - 1);
        }

        // spawn truck 
        bool spawnable = trucksSlots[searchIndex];
    }



    private int LoopIndex(int index, int max)
    {
        index++;

        if (index > max)
        {
            index = 0;
        }

        return index;
    }


    public void TimedoutTruck()
    {
        lives--;

        if (lives <= 0)
        {
            GameOver.Invoke();
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    public UnityEvent GameOver;
    public UnityEvent LifeLost;

    private int lives = 3;

    private int trucksCount = 0;

    private int mailPerTruck = 5;
    private float timePerTruck = 3f;

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
        StartCoroutine(SpawnTruckRoutine());
    }

    IEnumerator SpawnTruckRoutine()
    {
        SpawnTruck();
        yield return new WaitForSeconds(timePerTruck);

        StartCoroutine(SpawnTruckRoutine());
    }


    private void SpawnTruck()
    {
        Debug.Log("Spawned Truck: " + timePerTruck);

        int searchIndex = Random.Range(0, trucksSlots.Count);

        while (trucksSlots[searchIndex] == false)
        {
            searchIndex = LoopIndex(searchIndex, trucksSlots.Count - 1);
        }

        // spawn truck 
        bool spawnable = trucksSlots[searchIndex];


        trucksCount++;
        timePerTruck += Random.Range(0, timeDecreaseRange);
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


    public void FinishedTruck()
    {

    }

    public void TimedoutTruck()
    {
        lives--;
        LifeLost.Invoke();

        if (lives <= 0)
        {
            GameOver.Invoke();
        }
    }



}

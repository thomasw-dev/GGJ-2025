using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    public static int lives = 3;
    public static int score = 0;

    public int currentLives = 3; // view lives in Inspector
    public int currentScore = 0; // view score in Inspector

    [SerializeField] private int trucksCount = 2;
    [SerializeField] private int phaseIndex = 0;

    public List<MailSpawnManager> mailSlotsLevel;
    public List<Truck> truckSlotsLevel;

    public UnityEvent GameOver;
    public UnityEvent LifeLost;

    void Start()
    {
        lives = 3;
        score = 0;
        StopAllCoroutines();
        StartCoroutine(RunPhaseRoutine());
    }

    void Update()
    {
        currentLives = lives;
        currentScore = score;
    }

    IEnumerator RunPhaseRoutine()
    {
        yield return new WaitForSeconds(1);

        RunPhase();
    }

    public void RunPhase()
    {
        PhaseData.Phase phase = PhaseData.Phases[phaseIndex];

        for (int m = 0; m < phase.MailSlots.Count; m++) 
        {
            mailSlotsLevel[m].HandleSpawningColor(phase.MailSlots[m]);
        }

        trucksCount = 0;
        for (int t = 0; t < phase.TruckSlots.Count; t++)
        {
            if (phase.TruckSlots[t].Color != MailType.Colors.None)
            {
                trucksCount++;
                truckSlotsLevel[t].SpawnTruck(phase.TruckSlots[t].Color, phase.TruckSlots[t].MailNeeded, phase.TruckSlots[t].TimeAllowed);
            }
        }
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

    public void HandleTruckDecrease()
    {
        trucksCount--;

        if (trucksCount <= 0)
        {
            phaseIndex++;
            StartCoroutine(RunPhaseRoutine());
        }
    }

    public void FinishedTruck()
    {
        HandleTruckDecrease();
    }

    public void TimedoutTruck()
    {
        lives--;
        LifeLost?.Invoke();

        if (lives <= 0)
        {
            GameOver?.Invoke();
        }

        HandleTruckDecrease();
    }

    public void MailGotten()
    {
        score += 100;
    }
}

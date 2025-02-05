using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    public static int lives = 4;
    public static int score = 0;

    public int currentLives = 4; // view lives in Inspector
    public int currentScore = 0; // view score in Inspector

    [SerializeField] private int trucksCount = 2;
    [SerializeField] private int phaseIndex = 0;

    [SerializeField] TMP_Text phaseDisplay;
    [SerializeField] TMP_Text scoreDisplay;

    // Allow setting custom phase index from main menu
    public static int setPhaseIndex = 9;

    public List<MailSpawnManager> mailSlotsLevel;
    public List<Truck> truckSlotsLevel;

    public UnityEvent GameOver;
    public UnityEvent LifeLost;

    void Start()
    {
        lives = 4;
        score = 0;
        phaseIndex = setPhaseIndex;
        StopAllCoroutines();
        StartCoroutine(RunPhaseRoutine());
    }

    void Update()
    {
        currentLives = lives;
        currentScore = score;

        phaseDisplay.text = $"Phase {phaseIndex + 1}";
        scoreDisplay.text = score.ToString();
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

    public void HandleTruckDecrease()
    {
        trucksCount--;

        if (trucksCount <= 0)
        {
            // If it is at the final phase, keep repeating the final phase
            // TODO: Game Win after the final phase?
            if (phaseIndex < PhaseData.Phases.Count - 1)
            {
                phaseIndex++;
            }
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

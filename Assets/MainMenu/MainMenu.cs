using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider phaseSlider;
    [SerializeField] Transform bubbleParent;
    [SerializeField] GameObject bubblePrefab;
    [SerializeField] int bubbleCount = 50;

    List<Vector3> generatedPositions = new List<Vector3>();
    float minX = -7.5f;
    float maxX = 7.5f;
    float minY = -3.5f;
    float maxY = 4.25f;
    float minDistance = 0.5f;

    void Start()
    {
        GameMaster.setPhaseIndex = 0;

        // Spawn bubbles
        GenerateRandomPositions(bubbleCount);
        for (int i = 0; i < generatedPositions.Count; i++)
        {
            GameObject bubble = Instantiate(bubblePrefab, generatedPositions[i], Quaternion.Euler(0, 0, Random.Range(0, 360)));
            bubble.transform.SetParent(bubbleParent);
        }
    }

    public void GetSliderValue()
    {
        GameMaster.setPhaseIndex = (int)phaseSlider.value;
    }

    void GenerateRandomPositions(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 newPos = GetRandomPosition();

            bool validPosition = CheckValidPosition(newPos);

            while (!validPosition)
            {
                newPos = GetRandomPosition();
                validPosition = CheckValidPosition(newPos);
            }

            generatedPositions.Add(newPos);
        }

        Vector3 GetRandomPosition()
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = 0f;

            return new Vector3(x, y, z);
        }

        bool CheckValidPosition(Vector3 newPos)
        {
            foreach (Vector3 existingPos in generatedPositions)
            {
                if (Vector3.Distance(newPos, existingPos) < minDistance)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

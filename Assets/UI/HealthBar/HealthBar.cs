using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public List<HealthItem> healthItems;
    public HealthItem healthItem;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        for (int c = 0; c < transform.childCount; c++) 
        { 
            Destroy(transform.GetChild(c).gameObject);
        }
        healthItems = new List<HealthItem>();

        for (int i = 0; i < GameMaster.lives; i++)
        {
            healthItems.Add(Instantiate(healthItem, this.transform));
        }

        index = healthItems.Count - 1;
    }

    public void LoseLife()
    {
        if (index >= 0)
        {
            healthItems[index].LoseLife();
            index--;
        }
    }

    
}

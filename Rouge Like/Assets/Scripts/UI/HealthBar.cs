using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider mySlider;
    PlayerStatsManager myPlayerStats;
    void Start()
    {
        mySlider = GetComponent<Slider>();
        myPlayerStats = FindObjectOfType<PlayerController>().GetComponent<PlayerStatsManager>();
        mySlider.maxValue = myPlayerStats.GetHealth();
        mySlider.value = myPlayerStats.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        mySlider.value = myPlayerStats.GetHealth();
    }
}

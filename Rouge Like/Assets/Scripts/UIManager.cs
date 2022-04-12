using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    private PlayerStatsManager myPlayerStats;
    private int myUIHealth;
    private GameObject myPlayer;
    public TMP_Text myHealthText;
    public TMP_Text myEnemiesAliveText;


    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        myPlayerStats = FindObjectOfType<PlayerController>().GetComponent<PlayerStatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        myHealthText.text = myPlayerStats.GetHealth().ToString() + "/" + myPlayerStats.myMaxHealth.GetValue().ToString();
        //myHealthText.text = myPlayer.GetComponent<PlayerController>().GetHealth().ToString();
        myEnemiesAliveText.text = EnemyManager.Instance?.myNumberOfEnemiesAlive.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    private float myScore;
    private int myUIHealth;
    private GameObject myPlayer;
    public TMP_Text myHealthText;
    public TMP_Text myScoreText;


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
    }

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        myUIHealth = (int)myPlayer.GetComponent<PlayerController>().GetHealth();
        myHealthText.text = myUIHealth.ToString();
        myScore = 0;
    }

    public void AddScore()
    {
        myScore += 1;
    }

    // Update is called once per frame
    void Update()
    {
        myHealthText.text = myPlayer.GetComponent<PlayerController>().GetHealth().ToString();
        myScoreText.text = myScore.ToString();
    }
}

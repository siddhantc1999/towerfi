using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoringSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI lives;
    public float CurrentScore;
    public float getCurrentScore
    {
        get { return CurrentScore; }
        set { CurrentScore = value; }
    }

    public int Currentlives;
    public int getLives
    {
        get { return Currentlives; }
        set { Currentlives = value; }
    }
    public static ScoringSystem Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        Currentlives = 3;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(float addscore)
    {
        CurrentScore += addscore;
        score.text = CurrentScore.ToString();
        GameManager.Instance.getCurrentCurrency = CurrentScore;
    }
    public void SubScore(float depscore)
    {
        CurrentScore -= depscore;
        score.text = CurrentScore.ToString();
        GameManager.Instance.getCurrentCurrency = CurrentScore;
    }
    public void Sublives()
    {
        Currentlives -= 1;
        lives.text = Currentlives.ToString();
        GameManager.Instance.getlives =Currentlives;
    }
}

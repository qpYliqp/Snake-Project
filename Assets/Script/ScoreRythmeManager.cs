using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRythmeManager : MonoBehaviour
{

    public static ScoreRythmeManager instance;


    [Header("Script")]
    [SerializeField] private float f_totalNote;
    public float f_neutral;
    public float f_good;
    public float f_great;
    public float f_perfect;
    public float f_miss;

    [Header("Score")]
    public int i_currentScore;
    [SerializeField] private int i_neutralNote = 20;
    [SerializeField] private int i_goodNote = 50;
    [SerializeField] private int i_greatNote = 100;
    [SerializeField] private int i_perfectNote = 150;
    public int i_currentMultiplier;
    [SerializeField] private int i_MultiplierTracker;
    [SerializeField] private int[] i_IndexTracker = new int[3];

    [Header("Affichage")]
    public Text txt_CurrentScore;
    public Text txt_Multiplier;
    public GameObject go_EndScreen;
    public Text txt_score, txt_neutral, txt_good, txt_great, txt_perfect, txt_miss, txt_rank, txt_percent;

    void Affichage()
    {
        //Affichage du score en fonction des variables
        txt_CurrentScore.text = i_currentScore.ToString();
        txt_Multiplier.text = "X" + i_currentMultiplier.ToString();
    }

    private void Awake()
    {
        instance = this;
        go_EndScreen.SetActive(false);
        f_totalNote = 17;
    }

    void Start()
    {
        //nombre de note réussies pour débloquer un coefficient
        i_IndexTracker[0] = 4;
        i_IndexTracker[1] = 12;
        i_IndexTracker[2] = 26;

        //Mise à zéro des scores
        i_currentScore = 0;
        i_currentMultiplier = 1;

        //Valeur des scores en fonction de la précision d'éxecution
        i_neutralNote = 20;
        i_goodNote = 50;
        i_greatNote = 100;
        i_perfectNote = 150;
    }

    void Score()
    {
       
    }

    public void NoteHit(string note)
    {


        
            //conditionnel pour savoir si on pour passer à un coefficient supérieur dans le calcul des scores
            if (i_currentMultiplier - 1 < i_IndexTracker.Length)
            {
                i_MultiplierTracker++;

                if (i_IndexTracker[i_currentMultiplier - 1] <= i_MultiplierTracker)
                {
                    i_MultiplierTracker = 0;
                    i_currentMultiplier += 1;
                }
            }
        if (note == "neutral")
        {
            i_currentScore += i_neutralNote * i_currentMultiplier;
            f_neutral++; f_totalNote--;

        }
        else if (note == "good")
        {
            i_currentScore += i_goodNote * i_currentMultiplier;
            f_good++; f_totalNote--;
        }
        else if (note == "great")
        {
            i_currentScore += i_greatNote * i_currentMultiplier;
            f_great++; f_totalNote--;
        }
        else if(note == "perfect")
        {
            i_currentScore += i_perfectNote * i_currentMultiplier;
            f_perfect++; f_totalNote--;
        }
    }

    public void NoteMissed()
    {
        //Remise à zéro des coefficients
        i_currentMultiplier = 1;
        i_MultiplierTracker = 0;
        f_miss++; f_totalNote--;
        Debug.Log("miss");
        Affichage();
       
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Affichage();

        if(f_totalNote <= 0)
        {
            SoundManager.instance.StopAMusic("redbone");
            txt_score.text = i_currentScore.ToString(); txt_neutral.text = f_neutral.ToString(); txt_good.text = f_good.ToString(); txt_great.text = f_great.ToString(); 
            txt_perfect.text = f_perfect.ToString(); txt_miss.text = f_miss.ToString();
            float test = (17 - f_miss) * 100 / 17;
            txt_percent.text = test.ToString(); 
            go_EndScreen.SetActive(true);

        }
    }
}

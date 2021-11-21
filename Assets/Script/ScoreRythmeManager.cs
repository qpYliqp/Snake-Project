using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRythmeManager : MonoBehaviour
{

    public static ScoreRythmeManager instance;



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

    void Affichage()
    {
        //Affichage du score en fonction des variables
        txt_CurrentScore.text = i_currentScore.ToString();
        txt_Multiplier.text = "X" + i_currentMultiplier.ToString();
    }

    private void Awake()
    {
        instance = this;
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

        }
        else if (note == "good")
        {
            i_currentScore += i_goodNote * i_currentMultiplier;
        }
        else if (note == "great")
        {
            i_currentScore += i_greatNote * i_currentMultiplier;
        }
        else if(note == "perfect")
        {
            i_currentScore += i_perfectNote * i_currentMultiplier;
        }
    }

    public void NoteMissed()
    {
        //Remise à zéro des coefficients
        i_currentMultiplier = 1;
        i_MultiplierTracker = 0;
        Affichage();
       
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Affichage();
    }
}

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
    public GameObject go_StartScreen; 
    public GameObject go_EndScreen;
    public GameObject go_Compteur;
    public Button btn_Musique1;
    public Button btn_Musique2;
    public Button btn_Musique3;
    [SerializeField] private int i_compteur;
    public Text txt_score, txt_neutral, txt_good, txt_great, txt_perfect, txt_miss, txt_rank, txt_percent, txt_Compteur;

    [Header("Scroller")]
    [SerializeField] private Scroller Beat1;
    [SerializeField] private Scroller Beat2;
    [SerializeField] private Scroller Beat3;
    [SerializeField] private GameObject go_Beat1;
    [SerializeField] private GameObject go_Beat2;

    [Header("Variable")]
    public bool startPlaying;
    public float f_tempo;
    [SerializeField] private string music;



    void Affichage()
    {
        //Affichage du score en fonction des variables
        txt_CurrentScore.text = i_currentScore.ToString();
        txt_Multiplier.text = "X" + i_currentMultiplier.ToString();
        txt_Compteur.text = i_compteur.ToString();

        if (f_totalNote <= 0)
        {
            SoundManager.instance.StopAMusic(music);
            txt_score.text = i_currentScore.ToString(); txt_neutral.text = f_neutral.ToString(); txt_good.text = f_good.ToString(); txt_great.text = f_great.ToString();
            txt_perfect.text = f_perfect.ToString(); txt_miss.text = f_miss.ToString();
            float test = (f_totalNote - f_miss) * 100 / f_totalNote;
            txt_percent.text = test.ToString();
            go_EndScreen.SetActive(true);
        }
    }

    public void Musique1()
    {
        f_totalNote = 17;
        f_tempo = 155;
        music = "redbone";

    }

    public void Musiqu2()
    {
        f_totalNote = 17;
        f_tempo = 124;
        music = "harvey";
    }

    public void Musique3()
    {

    }

    void LaunchMusic(Scroller beat)
    {
        beat.hasStarted = true;
    }

    private void Awake()
    {
        instance = this;
        go_StartScreen.SetActive(true);
        go_EndScreen.SetActive(false);
        i_compteur = 5;
        
    }

    void Start()
    {
        //nombre de note r�ussies pour d�bloquer un coefficient
        i_IndexTracker[0] = 4;
        i_IndexTracker[1] = 12;
        i_IndexTracker[2] = 26;

        //Mise � z�ro des scores
        i_currentScore = 0;
        i_currentMultiplier = 1;

        //Valeur des scores en fonction de la pr�cision d'�xecution
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


        
            //conditionnel pour savoir si on pour passer � un coefficient sup�rieur dans le calcul des scores
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
        //Remise � z�ro des coefficients
        i_currentMultiplier = 1;
        i_MultiplierTracker = 0;
        f_miss++; f_totalNote--;
        Debug.Log("miss");
        Affichage();
       
    }


    void Update()
    {
        Affichage();

        
    }

    IEnumerator WaitForLaunch()
    {
        go_Compteur.SetActive(true);
        while (i_compteur >= 0)
        {
            yield return new WaitForSeconds(1f);
            i_compteur--;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class RythmManager : MonoBehaviour
{

    public static RythmManager instance;


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
    public GameObject go_PlayingScreen;
    public GameObject go_Compteur;
    public Button btn_Musique1;
    public Button btn_Musique2;
    public Button btn_Musique3;
    public Button btn_Retry;
    public Button btn_Menu;
    [SerializeField] private int i_compteur;
    public Text txt_score, txt_neutral, txt_good, txt_great, txt_perfect, txt_miss, txt_rank, txt_percent, txt_compteur;

    [Header("Scroller")]
    [SerializeField] private Scroller Beat1;
    [SerializeField] private Scroller Beat2;
    [SerializeField] private GameObject go_Beat1;
    [SerializeField] private GameObject go_Beat2;

    [Header("Variable")]
    public bool startPlaying;
    public float f_tempo;
    [SerializeField] private bool b_canbeStopped;
    public int i_music; // To know which music is selected
    [SerializeField] private string str_music;



    void Affichage()
    {
        //Affichage du score en fonction des variables
        txt_CurrentScore.text = i_currentScore.ToString();
        txt_Multiplier.text = "X" + i_currentMultiplier.ToString();
        txt_compteur.text = i_compteur.ToString();


        
        if (f_totalNote <= 0 && b_canbeStopped)
        {
            SoundManager.instance.StopAMusic(str_music);
            txt_score.text = i_currentScore.ToString(); txt_neutral.text = f_neutral.ToString(); txt_good.text = f_good.ToString(); txt_great.text = f_great.ToString();
            txt_perfect.text = f_perfect.ToString(); txt_miss.text = f_miss.ToString();
            float test = (f_totalNote - f_miss) * 100 /f_totalNote;
            txt_percent.text = test.ToString();
            go_EndScreen.SetActive(true);
            btn_Retry.Select();


        }

        if (i_compteur <= 0)
        {
            switch (i_music) { 
                default: break;
                case 1: LaunchMusic(Beat1, go_Beat1); break;
                case 2: LaunchMusic(Beat2, go_Beat2); break;

            }
            
        }
    }
    /// </Liste de fonctions appelée quand on clique sur un boutton>
    /// 
    /// </début>
    public void Musique1()
    {
        f_totalNote = 17;
        f_tempo = 160 / 60;
        str_music = "redbone";
        StartCoroutine("WaitForLaunch");
        i_music = 1;

    }

    public void Musiqu2()
    {
        f_totalNote = 17;
        f_tempo = 124 / 60;
        str_music = "harvey";
        StartCoroutine("WaitForLaunch");
        i_music = 2;

    }

    public void Musique3()
    {

    }
    /// </Liste de fonctions appelée quand on clique sur un boutton>
    /// 
    /// </fin>
   
    //Ce qui permet de sélectionner le bon scroller
    void LaunchMusic(Scroller beat, GameObject go_beat)
    {
        go_Compteur.SetActive(false);
        i_compteur = 5;
        go_beat.SetActive(true);
        go_PlayingScreen.SetActive(true);
        beat.hasStarted = true;
        SoundManager.instance.PlayAMusic(str_music);

    }

    private void Awake()
    {
        instance = this;
        go_StartScreen.SetActive(true);
        go_EndScreen.SetActive(false);
        go_PlayingScreen.SetActive(false);
        go_Compteur.SetActive(false);
        go_Beat1.SetActive(false);
        go_Beat2.SetActive(false);
        i_compteur = 5;
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

    //calcul des notes
    public void NoteHit(string note)
    {
        if(b_canbeStopped == false)
        {
            b_canbeStopped = true;
        }


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
        else if (note == "perfect")
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


    void Update()
    {
        Affichage();

       
    }

    //permet de faire le décompte avant le démarrage du jeu
    IEnumerator WaitForLaunch()
    {
        go_StartScreen.SetActive(false);
        go_Compteur.SetActive(true);
        while (i_compteur > 0)
        {
            yield return new WaitForSeconds(1f);
            i_compteur--;


        }
    }
}

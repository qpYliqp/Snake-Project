using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnakeManager : MonoBehaviour
{
    [Header("Snake")]
    public GameObject SnakePrefab;
    [SerializeField]
    private GameObject Snake;

    [Header("El�ment de Gameplay")]
    public Transform FoodPrefab;
    [SerializeField]
    private GameObject FoodHolder;
    [SerializeField]
    private Transform SpawnZone;

    [Header("Modes de jeu")]
    public Gamemode[] gamemodes;
    [SerializeField]
    private Gamemode activeGamemode;

    [Header("Menu + Boutons")]
    public GameObject menu;
    public Button buttonStart;
    public Button buttonMenu;

    [Header("Textes pour Menu")]
    public Text GamemodeName;
    public Text GamemodeDesc;
    public Text GamemodeScore;
    public Text GamemodeHighscore;

    [Header("Score")]
    [SerializeField]
    private int score;
    [SerializeField]
    private int highscore;

    void Start()
    {
        score = 0;
        FoodHolder = GameObject.Find("FoodHolder");
        SpawnZone = GameObject.Find("SpawnZone").transform;

        SetGamemode(0);
        OpenMenu();
    }

    // Fonction qui g�re l'ouverture et l'initialisation du menu
    public void OpenMenu()
    {
        menu.SetActive(true);
        OverlayGamemode(activeGamemode);
        buttonStart.Select();
    }

    // Fonction qui supprime toutes les pommes existantes sur la map
    public void SupprAllApple()
    {
        for (int i = 0; i < FoodHolder.transform.childCount; i++)
        {
            Destroy(FoodHolder.transform.GetChild(i).gameObject);
        }
    }

    // Spawn d'une pomme
    public void AddApple()
    {
        Transform apple = Instantiate(this.FoodPrefab);

        float x = SpawnZone.position.x - SpawnZone.lossyScale.x / 2 + FoodPrefab.lossyScale.x
            + Random.Range(0, SpawnZone.lossyScale.x - FoodPrefab.lossyScale.x * 2);
        float y = SpawnZone.position.y - SpawnZone.lossyScale.y / 2 + FoodPrefab.lossyScale.y
            + Random.Range(0, SpawnZone.lossyScale.y - FoodPrefab.lossyScale.y * 2);

        apple.position = new Vector3(x, y, 0);
        apple.parent = FoodHolder.transform;
    }

    // consommation d'une pomme par le joueur
    // Param�tre : Transform de la pomme mang�e
    public void SnakeEatApple(Transform apple)
    {
        Destroy(apple.gameObject);
        AddApple();
        score += 10;
    }

    // Fonction appel�e lors du lancement d'un mode de jeu
    // Lance le jeu avec ce qu'il faut
    public void StartGame()
    {
        menu.SetActive(false);
        Snake = Instantiate(SnakePrefab);
        score = 0;
        highscore = PlayerPrefs.GetInt("Snake" + activeGamemode.name + "Highscore");

        AddApple();

        if (isMultipleApplesGM())
            for (int i = 1; i < 5; i++)
                AddApple();

    }

    // Fonction appel�e lors de la d�faite du joueur
    // Nettoie la sc�ne de jeu, enregistre le score si besoin, ouvre le menu
    public void Defeat()
    {
        Destroy(Snake);
        SupprAllApple();
        if (score > highscore)
            PlayerPrefs.SetInt("Snake" + activeGamemode.name + "Highscore", score);
        OpenMenu();
    }

    // Fonction qui permet de choisir le mode de jeu
    // Param�tre : indice du mode de jeu
    public void SetGamemode(int gamemode)
    {
        activeGamemode = gamemodes[gamemode];
        buttonStart.Select();
        score = 0;
        highscore = PlayerPrefs.GetInt("Snake" + activeGamemode.name + "Highscore");
        OverlayGamemode(activeGamemode);
    }

    // Fonction qui met � jour les informations sur le mode de jeu selectionn�
    // Param�tre : indice du mode de jeu
    public void OverlayGamemode(int gamemode)
    {
        OverlayGamemode(gamemodes[gamemode]);
    }

    // Fonction qui met � jour les informations sur le mode de jeu selectionn�
    // Param�tre : mode de jeu
    public void OverlayGamemode(Gamemode game)
    {
        GamemodeName.text = game.name;
        GamemodeDesc.text = game._description;
        if (score > 0)
            GamemodeScore.text = "Score = " + score;
        else
            GamemodeScore.text = "";
        score = 0;
        GamemodeHighscore.text = "Highscore = " + PlayerPrefs.GetInt("Snake" + game.name + "Highscore").ToString();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public string GetGamemodePrefabName()
    {
        return activeGamemode.name;
    }

    public bool isMultipleApplesGM()
    {
        return GetGamemodePrefabName() == "MultipleApples";
    }

    public bool isHarmlessSnakeGM()
    {
        return GetGamemodePrefabName() == "HarmlessSnake";
    }

    public bool isHarmlessWallsGM()
    {
        return GetGamemodePrefabName() == "HarmlessWalls";
    }
}

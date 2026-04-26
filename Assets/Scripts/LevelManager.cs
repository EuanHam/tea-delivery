using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private BobaDriver player;
    [SerializeField] private NewBobaShop bobaShop;
    [SerializeField] private GameObject highlightPrefab;
    [SerializeField] private UIManager ui;

    [SerializeField] private WinScreenManager winScreen;

    [SerializeField] private PauseScreenManager pauseScreenManager;

    private int[] balanceCondition;
    private float[] timeCondition;

    public float time;
    private string levelName;
    private int winBalance;
    private bool levelEnded = false;
    private GameObject highlight;
    void Awake()
    {
        balanceCondition = new int[] {500, 1000, 2000, 2100}; // Balance needed to win the level
        timeCondition = new float[] {120f, 100f, 80f, 60f}; //Time player has to complete the level
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
        switch (levelName)
        {
            case "Level0Tutorial":
                winBalance = 1;
                time = timeCondition[0];
                break;
            case "Level1":
                winBalance = balanceCondition[1];
                time = timeCondition[1];
                break;
            case "Level2":
                winBalance = balanceCondition[2];
                time = timeCondition[2];
                break;
            case "Level3":
                winBalance = balanceCondition[3];
                time = timeCondition[3];
                break;
            default:
                //TODO FIX DEFAULT WIN TIME AND BALANCE COND
                winBalance = balanceCondition[0];
                time = timeCondition[0];
                break;
        }

        highlight = Instantiate(highlightPrefab , Vector3.zero, Quaternion.Euler(90f, 0f, 0f), transform);
        highlight.transform.localScale = Vector3.one * 3f ;
    }

    // Update is called once per frame
    void Update()
    {
        levelName = SceneManager.GetActiveScene().name;
        if (player.load == null)
        {
            highlight.transform.position = bobaShop.transform.position + Vector3.up * 3.5f + Vector3.forward * 2.5f;
        } else
        {
            highlight.transform.position = player.load.customer.transform.position + Vector3.up * 3f;
        }
        
        // check wincon
        if (levelName == "Level0Tutorial" && player.balance >= winBalance && !levelEnded) {
            levelEnded = true;
            StartCoroutine(EndLevelSequence());
        } 
        // else if (ui.instructionActive())
        // {
        //     Time.timeScale = 0f;
        // } 
        // else {
        //     Time.timeScale = 1f;
            if (time <= 0 && !levelEnded) {
                levelEnded = true;
                StartCoroutine(EndLevelSequence());
            } 
        // }
        if (!pauseScreenManager.isPaused) {
            time -= Time.deltaTime;
        }
        float newY = highlight.transform.position.y + Mathf.Cos(Time.time * 2f) * 0.75f;
        highlight.transform.position = new Vector3(highlight.transform.position.x, newY, highlight.transform.position.z);
    }

    private System.Collections.IEnumerator ReturnToLevelSelectionAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("LevelSelection");
    }

    private System.Collections.IEnumerator EndLevelSequence()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);
        ui.endUI.SetActive(false);
        int stars = CalculateStars(player.balance);
        SaveHighScore(levelName, player.balance);
        ui.endUI.SetActive(true);
        int highScore = GetHighScore(levelName);

        winScreen.Show(player.balance, winBalance, player.ordersCompleted, player.specialOrdersCompleted, player.npcsHit, player.vehicleCollisions, stars, highScore);
    }

    private int CalculateStars(int balance)
    {
        float ratio = (float)balance / winBalance;
        if (ratio >= 1.0f) return 3;
        if (ratio >= 0.7f) return 2;
        if (ratio >= 0.4f) return 1;
        return 0;
    }

    void SaveHighScore(string levelName, int score)
    {
        string key = "HighScore_" + levelName;
        int currHigh = PlayerPrefs.GetInt(key, 0);
        if (score > currHigh)
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save();
            Debug.Log("New high score for " + levelName + ": " + score);
        }
    }

    int GetHighScore(string levelName)
    {
        string key = "HighScore_" + levelName;
        return PlayerPrefs.GetInt(key, 0);
    }


}

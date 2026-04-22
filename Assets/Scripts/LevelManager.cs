using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private BobaDriver player;

    [SerializeField] private UIManager ui;

    [SerializeField] private WinScreenManager winScreen;
    // Balance needed to win the level
    [SerializeField] private int[] balanceCondition = new int[] {500, 1000, 2000};

    //Time player has to complete the level
    [SerializeField] private float[] timeCondition = new float[] {120f, 120f, 120f};

    public float time;
    private string levelName;
    private int winBalance;
    private bool levelEnded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
        switch (levelName)
        {
            case "Level0Tutorial":
                winBalance = balanceCondition[0];
                time = timeCondition[0];

                break;
            case "Level1":
                winBalance = balanceCondition[1];
                time = timeCondition[1];
                break;
            default:
                //TODO FIX DEFAULT WIN TIME AND BALANCE COND
                winBalance = balanceCondition[0];
                time = timeCondition[0];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0) {
            levelEnded = true;
            StartCoroutine(EndLevelSequence());
        } else if (!ui.instructionActive())
        {
            time -= Time.deltaTime;
        }
    }

    private System.Collections.IEnumerator ReturnToLevelSelectionAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("LevelSelection");
    }

    private System.Collections.IEnumerator EndLevelSequence()
    {
    yield return new WaitForSeconds(2f);
    ui.endUI.SetActive(false);
    int stars = CalculateStars(player.balance);
    winScreen.Show(player.balance, winBalance, player.ordersCompleted, player.npcsHit, stars);
    }

    private int CalculateStars(int balance)
    {
        float ratio = (float)balance / winBalance;
        if (ratio >= 1.0f) return 3;
        if (ratio >= 0.7f) return 2;
        if (ratio >= 0.4f) return 1;
        return 0;
    }
}

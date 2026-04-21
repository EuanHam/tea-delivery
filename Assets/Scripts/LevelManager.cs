using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private BobaDriver player;

    [SerializeField] private UIManager ui;
    // Balance needed to win the level
    [SerializeField] private int[] balanceCondition = new int[] {500, 1000, 2000};

    //Time player has to complete the level
    [SerializeField] private float[] timeCondition = new float[] {120f, 120f, 120f};
    public float time;
    private string levelName;
    private int winBalance;

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
            StartCoroutine(ReturnToLevelSelectionAfterDelay(5f));
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
}

using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private BobaDriver player;
    [SerializeField] private NewBobaShop bobaShop;
    [SerializeField] private GameObject highlightPrefab;
    [SerializeField] private UIManager ui;
    // Balance needed to win the level
    [SerializeField] private int[] balanceCondition = new int[] {500, 1000, 2000};

    //Time player has to complete the level
    [SerializeField] private float[] timeCondition = new float[] {120f, 120f, 120f};
    public float time;
    private string levelName;
    private int winBalance;
    private GameObject highlight;

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

        highlight = Instantiate(highlightPrefab, Vector3.zero, Quaternion.Euler(90f, 0f, 0f), transform);
        highlight.transform.localScale = Vector3.one * 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.load == null)
        {
            highlight.transform.position = bobaShop.transform.position + Vector3.up * 3f;
        } else
        {
            highlight.transform.position = player.load.customer.transform.position + Vector3.up * 3f;
        }
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

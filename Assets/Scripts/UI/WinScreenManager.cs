using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreenPanel;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI numOrdersText;
    [SerializeField] private TextMeshProUGUI ordersBalanceText;
    [SerializeField] private TextMeshProUGUI numSpecialOrdersText;
    [SerializeField] private TextMeshProUGUI specialOrdersBalanceText;
    [SerializeField] private TextMeshProUGUI numberOfVehicleCollisionsText;
    [SerializeField] private TextMeshProUGUI vehicleCollisionsText;
    [SerializeField] private TextMeshProUGUI numNPCsHitText;
    [SerializeField] private TextMeshProUGUI npcsHitBalanceText;
    [SerializeField] private TextMeshProUGUI finalBalanceText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI star1BalanceText;
    [SerializeField] private TextMeshProUGUI star2BalanceText;
    [SerializeField] private TextMeshProUGUI star3BalanceText;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite starFilled;
    [SerializeField] private Sprite starEmpty;
    [SerializeField] private GameObject lowResOverlay;

    [SerializeField] private TextMeshProUGUI ordersLabelText;
    [SerializeField] private TextMeshProUGUI npcsLabelText;

    [SerializeField] private float delayBetweenStars = 0.6f;
    [SerializeField] private float countUpDuration = 1.0f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip moneySound;
    [SerializeField] private AudioClip countUp;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip dingSound;
    [SerializeField] private AudioClip tireSqueak;

    [SerializeField] private WinScreenRobot robot;

    void Start()
    {
        winScreenPanel.SetActive(false);
    }

    public void Show(int finalBalance, int targetBalance, int numOrders, int specialOrdersCompleted, int numNPCsHit, int vehicleCollisions, int starCount, int highScore)
    {
        winScreenPanel.SetActive(true);
        lowResOverlay.SetActive(false);
        Time.timeScale = 0f;

        robot.DriveIn();

        star1BalanceText.text = $"${(int)(targetBalance * 0.4)}";
        star2BalanceText.text = $"${(int)(targetBalance * 0.7)}";
        star3BalanceText.text = $"${targetBalance}";

        highScoreText.gameObject.SetActive(false);
        numOrdersText.gameObject.SetActive(false);
        ordersBalanceText.gameObject.SetActive(false);
        numSpecialOrdersText.gameObject.SetActive(false);
        specialOrdersBalanceText.gameObject.SetActive(false);
        numberOfVehicleCollisionsText.gameObject.SetActive(false);
        vehicleCollisionsText.gameObject.SetActive(false);
        numNPCsHitText.gameObject.SetActive(false);
        npcsHitBalanceText.gameObject.SetActive(false);
        finalBalanceText.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        ordersLabelText.gameObject.SetActive(false);
        npcsLabelText.gameObject.SetActive(false);

        for (int i = 0; i < stars.Length; i++) {
            stars[i].sprite = starEmpty;
        }

        StartCoroutine(AnimateSequence(finalBalance, numOrders, specialOrdersCompleted, numNPCsHit, vehicleCollisions, starCount, highScore));
    }

    private IEnumerator AnimateSequence(int finalBalance, int numOrders, int specialOrdersCompleted, int numNPCsHit, int vehicleCollisions, int starCount, int highScore)
    {
        musicSource.PlayOneShot(victorySound);
        yield return new WaitForSecondsRealtime(0.8f);

        // Star 1
        if (starCount >= 1)
        {
            stars[0].sprite = starFilled;
        }

        ordersLabelText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        highScoreText.text = $"High Score: ${highScore}";

        numOrdersText.gameObject.SetActive(true);
        ordersBalanceText.gameObject.SetActive(true);
        numOrdersText.text = $"{numOrders}x";
        ordersBalanceText.text = $"${numOrders * 100}";

        numSpecialOrdersText.gameObject.SetActive(true);
        numSpecialOrdersText.text = $"{specialOrdersCompleted}x";
        specialOrdersBalanceText.gameObject.SetActive(true);
        specialOrdersBalanceText.text = $"${specialOrdersCompleted * 200}";

        audioSource.PlayOneShot(dingSound);
        yield return StartCoroutine(StarAnimation(stars[0]));
        yield return new WaitForSecondsRealtime(delayBetweenStars);

        // Star 2
        if (starCount >= 2)
        {
            stars[1].sprite = starFilled;
        }

        npcsLabelText.gameObject.SetActive(true);
        numNPCsHitText.gameObject.SetActive(true);
        npcsHitBalanceText.gameObject.SetActive(true);
        numNPCsHitText.text = $"{numNPCsHit}x";
        npcsHitBalanceText.text = $"-${numNPCsHit * 10}";

        numberOfVehicleCollisionsText.gameObject.SetActive(true);
        numberOfVehicleCollisionsText.text = $"{vehicleCollisions}x";
        vehicleCollisionsText.gameObject.SetActive(true);
        vehicleCollisionsText.text = $"-${vehicleCollisions * 100}";

        audioSource.PlayOneShot(dingSound);
        yield return StartCoroutine(StarAnimation(stars[1]));
        yield return new WaitForSecondsRealtime(delayBetweenStars);

        // Star 3
        if (starCount >= 3)
        {
            stars[2].sprite = starFilled;
            audioSource.PlayOneShot(dingSound);
            yield return StartCoroutine(StarAnimation(stars[2]));
            yield return new WaitForSecondsRealtime(delayBetweenStars);
        }

        // Count of final balance
        finalBalanceText.gameObject.SetActive(true);
        audioSource.clip = countUp;
        audioSource.loop = true;
        audioSource.Play();
        yield return StartCoroutine(CountUpBalance(finalBalance));
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.PlayOneShot(moneySound);
        yield return StartCoroutine(TextAnimation(finalBalanceText));

        // Show result text at the end
        resultText.text = starCount == 0 ? "Better luck next time!" : "Nice work!";
        resultText.gameObject.SetActive(true);
    }

    private IEnumerator CountUpBalance(int finalBalance)
    {
        float elapsed = 0f;
        while (elapsed < countUpDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            int displayed = Mathf.RoundToInt(Mathf.Lerp(0, finalBalance, elapsed / countUpDuration));
            finalBalanceText.text = $"${displayed}";
            yield return null;
        }

        audioSource.PlayOneShot(moneySound);
        finalBalanceText.text = $"${finalBalance}";
    }

    public void OnRetryButton()
    {
        Debug.Log("Retry clicked");
        lowResOverlay.SetActive(true);
        StartCoroutine(ExitTransition(SceneManager.GetActiveScene().name));
    }

    public void OnLevelSelectButton()
    {
        lowResOverlay.SetActive(true);
        StartCoroutine(ExitTransition("LevelSelection"));
    }

    private IEnumerator StarAnimation(Image star)
    {
        float duration = 0.2f;
        Vector3 normalScale = Vector3.one;
        Vector3 bigScale = Vector3.one * 1.4f;

        // Scale up
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            star.transform.localScale = Vector3.Lerp(normalScale, bigScale, elapsed / duration);
            yield return null;
        }

        // Scale back down
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            star.transform.localScale = Vector3.Lerp(bigScale, normalScale, elapsed / duration);
            yield return null;
        }

        star.transform.localScale = normalScale;
    }

    private IEnumerator TextAnimation(TextMeshProUGUI text)
    {
        float duration = 0.1f;
        Vector3 normalScale = Vector3.one;
        Vector3 bigScale = Vector3.one * 1.1f;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            text.transform.localScale = Vector3.Lerp(normalScale, bigScale, elapsed / duration);
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            text.transform.localScale = Vector3.Lerp(bigScale, normalScale, elapsed / duration);
            yield return null;
        }

        text.transform.localScale = normalScale;
    }

    private IEnumerator ExitTransition(string sceneName)
    {
        robot.DriveOff();
        audioSource.PlayOneShot(tireSqueak);
        yield return new WaitForSecondsRealtime(0.8f);
        lowResOverlay.SetActive(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
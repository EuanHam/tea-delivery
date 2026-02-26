using UnityEngine;
using UnityEngine.UI;

public class BobaDeliveryManager : MonoBehaviour
{
    public static BobaDeliveryManager Instance;
    
    public Text hudText;
    public bool hasBoba = false;
    public bool gameWon = false;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    void Start()
    {
        UpdateHUD("Find the Boba Shop!");
    }
    
    public void UpdateHUD(string message)
    {
        if (hudText != null)
            hudText.text = message;
    }
    
    public void CollectBoba()
    {
        if (!gameWon)
        {
            hasBoba = true;
            UpdateHUD("Boba Acquired!\nDeliver The Boba");
        }
    }
    
    public void DeliverBoba()
    {
        if (!gameWon && hasBoba)
        {
            gameWon = true;
            UpdateHUD("YOU WIN!");
        }
    }
}
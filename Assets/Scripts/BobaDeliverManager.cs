using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class BobaDeliveryManager : MonoBehaviour
{
    public static BobaDeliveryManager Instance;
    
    public TMP_Text hudText;
    public bool hasBoba = false;
    public bool gameWon = false;

    public Boba currentBoba;
    public Order currentOrder;
    public float timeLeft;
    public bool timeRunning = false;

    public List<Transform> deliveryPoints;
    public List<Customer> customers = new List<Customer>();
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    void Start()
    {
        customers.Add(new Customer { name = "Alice", location = GameObject.Find("Alice").transform});
        customers.Add(new Customer { name = "Baxtor", location = GameObject.Find("Baxtor").transform});
        customers.Add(new Customer { name = "Chip", location = GameObject.Find("Chip").transform});
        GenerateOrder();
        UpdateHUD("Pick up your boba order from the shop!");
    }
    
    public void UpdateHUD(string message)
    {
        if (hudText != null)
            hudText.text = message;
    }

    public void GenerateOrder()
    {
        Customer randomCustomer = customers[Random.Range(0, customers.Count)];
        currentOrder = new Order
        {
            customer = randomCustomer,
            bobaType = (Boba)Random.Range(0, System.Enum.GetValues(typeof(Boba)).Length),
            deliveryLocation = randomCustomer.location
        };
        
        timeLeft = 60f; // 60 seconds 
        timeRunning = true;
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
            timeRunning = false;
            UpdateHUD("YOU WIN!");
        }
    }

    public void Update()
    {
        if (timeRunning)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 10)
                UpdateHUD("HURRY! Deliver the boba!");
            if (timeLeft <= 0)
            {
                timeRunning = false;
                UpdateHUD("Time's up! You are too slow");
            }
        }
    }
}
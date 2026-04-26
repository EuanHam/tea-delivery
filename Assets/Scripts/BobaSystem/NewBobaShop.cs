using UnityEngine;
using System.Collections.Generic;

public class NewBobaShop : MonoBehaviour
{
    private Queue<NewOrder> orders;
    [SerializeField] private NPCController npcManager;
    [SerializeField] private float nextOrder;
    [SerializeField] private float low, upper;
    [SerializeField] private AudioClip pickup;

    void Start()
    {
        orders = new Queue<NewOrder>();

    }

    void Update()
    {
        nextOrder -= Time.deltaTime;
        if (nextOrder <= 0)
        {
            orders.Enqueue(new NewOrder(npcManager.getRandomNPC(), low, upper));
            nextOrder = Random.Range(7f, 10f);
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player") 
        {   
            BobaDriver bd = c.gameObject.GetComponent<BobaDriver>();
            
            if (bd != null && bd.load == null && orders.Count != 0)
            {
                bd.load = orders.Dequeue();
                while (bd.load.customer  == null)
                {
                    bd.load = orders.Dequeue();
                }

                playPickup();
                bd.load.customer.GetComponent<npcAI>().stopAgent(true);
                bd.load.customer.GetComponent<Animator>().SetTrigger("customer");

            }
        }
        
    }

    public void playPickup()
    {
        if (pickup != null)
        {
            AudioSource.PlayClipAtPoint(pickup, transform.position);
        }
    }

}

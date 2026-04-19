using UnityEngine;
using System.Collections.Generic;

public class NewBobaShop : MonoBehaviour
{
    private Queue<NewOrder> orders;
    [SerializeField] private NPCController npcController;
    [SerializeField] private float nextOrder;
    [SerializeField] private float low, upper;

    void Start()
    {
        orders = new Queue<NewOrder>();

    }

    void Update()
    {
        nextOrder -= Time.deltaTime;
        if (nextOrder <= 0)
        {
            orders.Enqueue(new NewOrder(npcController.getRandomNPC(), low, upper));
            nextOrder = Random.Range(7f, 10f);
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player") 
        {
            BobaDriver bd = c.gameObject.GetComponent<BobaDriver>();
            
            if (bd != null && bd.load == null && orders.Count != 0)
            {
                bd.load = orders.Dequeue();
                bd.load.customer.GetComponent<npcAI>().stopAgent(true);
                bd.load.customer.GetComponent<Animator>().SetTrigger("customer");
            }
        }
        
    }

}

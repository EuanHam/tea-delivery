using UnityEngine;
using System.Collections;

public class NPCCollision : MonoBehaviour
{
    public PowerUpManager powerUpManager;
    [SerializeField] private Animator anim;
    [SerializeField] private UnityEngine.AI.NavMeshAgent nma;
    [SerializeField] private Collider rootCollider;
    [SerializeField] private AudioClip wilhelmScream, yay, cash_register, block;

    private Rigidbody[] rbs;

    void Awake()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player") 
        {

            bool invul = powerUpManager.isInvunerable();
            BobaDriver bd = c.gameObject.GetComponent<BobaDriver>();
            VehicleCollision vc = c.gameObject.GetComponent<VehicleCollision>();

            if (bd.load != null && bd.load.customer == this.gameObject)
            {
                Debug.Log("Delivered Boba!");

                bd.load = null;

                bd.ordersCompleted += 1;
                
                bd.balance += (powerUpManager.isDoubleMoney() ? 2 : 1) * 100;
                playCashRegister();
                StartCoroutine(startVictory());
            } else if (!invul && !vc.isStunned()) {
                bd.balance = Mathf.Max(0, bd.balance - 10);
                bd.npcsHit += 1;
                PlayWilhelmScream();
                EnableRagdoll(c);
                Destroy(gameObject, Random.Range(5f, 10f));
            } else if(invul)
            {
                PlayBlock();
            }
        }
        
    }

    private void DisableRagdoll()
    {
        anim.enabled = true;
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
        }
    }

    private void EnableRagdoll(Collision c)
    {
        rootCollider.enabled = false;

        nma.isStopped = true;
        nma.enabled = false;
        
        anim.enabled = false;
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
            rb.AddForce(-c.impulse.normalized * 300.0f, ForceMode.Impulse);
        }
    }
    
    private IEnumerator startVictory()
    {
        PlayYay();

        anim.SetTrigger("victory");

        yield return new WaitForSeconds(10f);

        nma.isStopped = false;
    }
    public void PlayWilhelmScream()
    {
        if (wilhelmScream != null)
        {
            AudioSource.PlayClipAtPoint(wilhelmScream, transform.position);
        }
    }

    public void PlayYay()
    {
        if (yay != null)
        {
            AudioSource.PlayClipAtPoint(yay, transform.position);
        }
    }
    public void playCashRegister()
    {
        if (cash_register != null)
        {
            AudioSource.PlayClipAtPoint(cash_register, transform.position);
        }
    }

    public void PlayBlock()
    {
        if (block != null)
        {
            AudioSource.PlayClipAtPoint(block, transform.position);
        }
    }

}

using UnityEngine;
using System.Collections;

public class CollisionControl : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private UnityEngine.AI.NavMeshAgent nma;
    [SerializeField] private Collider rootCollider;
    [SerializeField] private AudioClip wilhelmScream, yay;
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
            BobaDriver bd = c.gameObject.GetComponent<BobaDriver>();
            if (bd.load != null && bd.load.customer == this.gameObject)
            {
                Debug.Log("Delivered Boba!");

                bd.load = null;
                bd.balance += 100;
                bd.ordersCompleted += 1;
                
                StartCoroutine(startVictory());
            } else {
                bd.balance = Mathf.Max(0, bd.balance - 10);
                bd.npcsHit += 1;
                PlayWilhelmScream();
                EnableRagdoll(c);
                Destroy(gameObject, Random.Range(5f, 10f));


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
}

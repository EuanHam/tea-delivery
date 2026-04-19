using UnityEngine;
using System.Collections;

public class CollisionControl : MonoBehaviour
{

    private Rigidbody[] rbs;
    public Animator anim;
    public UnityEngine.AI.NavMeshAgent nma;
    public Collider rootCollider;
    private float timer = 0;
    private bool start;
    
    [Header("Audio")]
    [SerializeField] private AudioClip wilhelmScream, yay;

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
                
                StartCoroutine(startVictory());
            } else {
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
    
    private void PlayWilhelmScream()
    {
        if (wilhelmScream != null)
        {
            AudioSource.PlayClipAtPoint(wilhelmScream, transform.position);
        }
    }

    private void PlayYay()
    {
        if (yay != null)
        {
            AudioSource.PlayClipAtPoint(yay, transform.position);
        }
    }

    private IEnumerator startVictory()
    {
        PlayYay();

        anim.SetTrigger("victory");

        yield return new WaitForSeconds(10f);

        nma.isStopped = false;
    }
}

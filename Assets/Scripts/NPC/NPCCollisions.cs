using UnityEngine;
using System.Collections;
public class NPCCollisions : MonoBehaviour
{
public PowerUpManager powerUpManager;
    [SerializeField] private Animator anim;
    [SerializeField] private UnityEngine.AI.NavMeshAgent nma;
    [SerializeField] private Collider rootCollider;
    [SerializeField] private AudioClip wilhelmScream, yay;
    [SerializeField] private BobaDriver player;

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
            if (player.load != null && player.load.customer == this.gameObject || invul)
            {

                if (invul) return;

                Debug.Log("Delivered Boba!");

                player.load = null;
                player.balance += (powerUpManager.isDoubleMoney() ? 2 : 1) * 100;
                
                StartCoroutine(startVictory());
            } else {
                player.balance = Mathf.Max(0, player.balance - 10);
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

using UnityEngine;
using System.Collections;

public class NPCCollision : MonoBehaviour
{
    public PowerUpManager powerUpManager;
    [SerializeField] private Animator anim;
    [SerializeField] private UnityEngine.AI.NavMeshAgent nma;
    [SerializeField] private Collider rootCollider;
    [SerializeField] private AudioClip wilhelmScream, yay, cash_register;
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

                if (powerUpManager.isDoubleMoney()) {
                    bd.specialOrdersCompleted += 1;
                } else {
                    bd.ordersCompleted += 1;
                }

                bd.balance += (powerUpManager.isDoubleMoney() ? 2 : 1) * 100;
                playCashRegister();
                StartCoroutine(startVictory());
            } else if (!invul && !vc.isStunned()) {
                string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                int penalty = 10; // default

                switch (sceneName)
                {
                    case "Level0Tutorial":
                        penalty = 10;
                        break;
                    case "Level1":
                        penalty = 10;
                        break;
                    case "Level2":
                        penalty = 20;
                        break;
                    case "Level3":
                        penalty = 30;
                        break;
                }

                bd.balance -= penalty;
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
    public void playCashRegister()
    {
        if (cash_register != null)
        {
            AudioSource.PlayClipAtPoint(cash_register, transform.position);
        }
    }

}

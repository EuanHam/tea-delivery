using UnityEngine;

public class CollisionControl : MonoBehaviour
{

    private Rigidbody[] rbs;
    public Animator anim;
    public UnityEngine.AI.NavMeshAgent nma;
    public Collider rootCollider;
    private float timer = 0;
    private bool start;

    void Awake()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player") 
        {
            EnableRagdoll(c);
            Destroy(gameObject, Random.Range(5f, 10f));
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
}

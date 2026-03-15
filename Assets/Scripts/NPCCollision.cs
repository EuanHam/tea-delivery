using UnityEngine;

public class CollisionControl : MonoBehaviour
{

    private Rigidbody[] rbs;
    public Animator anim;
    public Collider rootCollider;

    void Awake()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player") EnableRagdoll(c);
        
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
        Debug.Log("Enabled Ragdoll");
        rootCollider.enabled = false;
        anim.enabled = false;
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
            rb.AddForce(-c.impulse.normalized * 300.0f, ForceMode.Impulse);
        }
    }
}

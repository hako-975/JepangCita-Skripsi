using UnityEngine;

public class TableSenseiAction : MonoBehaviour
{
    Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            animator = other.gameObject.GetComponent<Animator>();
            Debug.Log(other.gameObject);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsSitting", true);
            other.gameObject.GetComponent<Transform>().position = new Vector3(-1.5f, 0.18f, -6.55f);
            other.gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 90f, 0f);

        }
    }
}

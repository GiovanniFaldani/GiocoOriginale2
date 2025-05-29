using UnityEngine;

public class Negozio : MonoBehaviour
{
    Animator animator;

    public void OpenMerchantPanel(bool newState) {
        if (animator == null) animator = GetComponent<Animator>();
        animator.SetBool("CanOpen", newState);
    }
}
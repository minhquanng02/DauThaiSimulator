using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        PlayAnimation();
    }
    public void PlayAnimation()
    {
        animator.Play("Loading");
    }
}

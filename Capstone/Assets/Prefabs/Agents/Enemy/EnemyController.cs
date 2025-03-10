using UnityEngine;

public class EnemyController : Agent
{
    private bool canHook = true;
    private float minTime = 1f;
    private float maxTime = 3f;

    protected override void Awake()
    {
        base.Awake();
        targetTransform = GameObject.FindWithTag("Player").transform;
        Invoke("ThrowHook", Random.Range(minTime, maxTime));
    }

    private void ThrowHook()
    {
        if (canHook)
        {
            PlayAnimation("Right_Hook");
            canHook = false;
            Invoke("ResetHook", 1f);
        }
        Invoke("ThrowHook", Random.Range(minTime, maxTime));
    }

    private void ResetHook()
    {
        canHook = true;
    }

    protected override void OnDeath()
    {
        GameManager.Instance.HandleGameOver(true);
    }
}

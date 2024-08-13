using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeadEvent;

    public int health { get; private set; }
    [SerializeField] private int _maxHealth;

    private Agent _agent;
    public void Initialize(Agent agent)
    {
        _agent = agent;
        ResetHealth();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10);
        }
    }
    public void ResetHealth()
    {
        health = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        OnHitEvent?.Invoke();
        if (health <= 0)
            OnDeadEvent?.Invoke();
    }
    public float GetNormalizedHealth()
    {
        return health/(float)_maxHealth;
    }
}

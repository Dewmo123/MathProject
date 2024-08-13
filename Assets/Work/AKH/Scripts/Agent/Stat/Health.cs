using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnHitEvent;
    public UnityEvent OnChangeEvent;
    public UnityEvent OnRestoreEvent;
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
            ChangeHp(-10);
        }
    }
    public void ResetHealth()
    {
        health = _maxHealth;
    }
    public void ChangeHp(int damage)
    {
        health = Mathf.Clamp(health + damage,0,_maxHealth);
        OnChangeEvent?.Invoke();
        if (damage < 0)
            OnHitEvent?.Invoke();
        else if(damage > 0)
            OnRestoreEvent?.Invoke();
        if (health <= 0)
            OnDeadEvent?.Invoke();
    }
    public float GetNormalizedHealth()
    {
        return health / (float)_maxHealth;
    }
}

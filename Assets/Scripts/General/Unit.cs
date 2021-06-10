using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    [Header("Unit Parameters")]
    [SerializeField] protected int maxHealth;

    [Header("Unit References")]
    [SerializeField] protected MeshRenderer changedPlayerMaterial;
    [SerializeField] protected HealthBar healthBar;

    protected Player _player;
    protected float _bodyRadius;
    protected int _currentHealth;
    protected bool _isLife;

    #region OnEnable OnDisable
    protected void OnEnable()
    {
        healthBar.SetMaxHealt(maxHealth);
        Health = maxHealth;

        _isLife = true;
    }

    protected void OnDisable() { }
    #endregion

    #region Properties
    public float BodyRadius => _bodyRadius;

    public Player Player
    {
        get => _player;
        set
        {
            _player = value;
            changedPlayerMaterial.material = value.material;
        }
    }

    public Vector3 Position => transform.position;

    public int Health
    {
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Min(maxHealth, value);
            healthBar.SetHealth(_currentHealth);
        }
    }

    public Transform Transform => transform;
    #endregion

    #region Health
    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health < 0) Death();
    }

    public void Death()
    {
        if (!_isLife) return;

        _isLife = false;
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
    #endregion
}

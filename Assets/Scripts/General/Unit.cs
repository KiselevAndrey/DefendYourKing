using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    [Header("Unit Parameters")]
    [SerializeField] protected int maxHealth;

    [Header("Unit References")]
    [SerializeField] private MeshRenderer changedPlayerMaterial;
    [SerializeField] private HealthBar healthBar;

    protected Player _player;
    protected float _bodyRadius;
    private int _currentHealth;
    private bool _isLife;

    #region OnEnable OnDisable
    protected void OnEnable()
    {
        healthBar.SetMaxHealt(maxHealth);
        Health = maxHealth;

        _isLife = true;
    }
    #endregion

    #region Properties
    public int Health
    {
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Min(maxHealth, value);
            healthBar.SetHealth(_currentHealth);
        }
    }

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

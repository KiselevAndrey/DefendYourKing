using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit, ISelectableUnit
{
    [Header("Unit Parameters")]
    [SerializeField] protected int maxHealth;
    [SerializeField, Tooltip("X - min, Y - max")] private Vector2Int rewardForDeath;

    [Header("Unit References")]
    [SerializeField] protected MeshRenderer changedPlayerMaterial;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected Animator animator;

    protected IPlayer _player;
    protected float _bodyRadius;
    protected bool _isLife;
    protected bool _selected;

    private int _currentHealth;

    #region OnEnable
    protected void OnEnable()
    {
        healthBar.gameObject.SetActive(true);
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

    public IPlayer Player
    {
        get => _player;
        set
        {
            _player = value;
            changedPlayerMaterial.material = value.Material;
        }
    }

    public Vector3 Position => transform.position;

    public bool NeedHidePrevios => true;

    public Transform Transform => transform;

    public int RewardForDeath => Random.Range(rewardForDeath.x, rewardForDeath.y + 1);
    #endregion

    #region Health
    public void TakeDamage(int damage, IUnit attackedUnit)
    {
        Health -= damage;

        if (Health < 1)
        {
            Health = 0;

            if (_isLife)
                attackedUnit.Player.AddRuby(RewardForDeath);

            animator.SetBool("Death", true);
        }
    }

    public void Death()
    {
        if (!_isLife) return;

        _isLife = false;
        healthBar.gameObject.SetActive(false);

        Deselect();
    }

    public void Destroy()
    {

    }
    #endregion

    #region Select
    public void Select()
    {
        _selected = true;
        _player.SelectUnit(this);
    }

    public void Deselect()
    {
        if (_selected)
        {
            _player.DeselectUnit(this);
            _selected = false;
        }
    }
    #endregion
}

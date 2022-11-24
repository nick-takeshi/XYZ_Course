using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Hero : Creature
{
    [SerializeField] private Collider2D[] _interactionResult = new Collider2D[1];
    [SerializeField] private ParticleSystem _particle;

    [SerializeField] private LayerMask _interactionLayer;
    

    [SerializeField] private AnimatorController _armed;
    [SerializeField] private AnimatorController _disarmed;
    [SerializeField] private Cooldown _throwCooldown;
    [SerializeField] private Cooldown _meleeCooldown;

    [SerializeField] private CheckCircleOverlap _interactionCheck;
    private bool _allowDoubleJump;
    private bool _getDoubleJump;
    private bool _isDodge;
    private Rigidbody2D rigid;
    private CapsuleCollider2D coll;


    public GameSession _session;

    private int CoinCount => _session.Data.Inventory.Count("Coin");
    private int SwordCount => _session.Data.Inventory.Count("Sword");



    protected override void Awake()
    {
        base.Awake();
        _sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        
    }
    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _session.Data.Inventory.OnChanged += OnInventoryChanged;
        UpdateHeroWeapon();
    }

    private void OnInventoryChanged(string id, int value)
    {
        if (id == "Sword") UpdateHeroWeapon();
        
    }

    private void OnDestroy()
    {
        _session.Data.Inventory.OnChanged -= OnInventoryChanged;

    }
    protected override void Update()
    {
        base.Update();

        if (rigid.velocity.x > 0 | rigid.velocity.y > 0)
        {
            coll.size = new Vector2(0.66f, 0.8f);
            _animator.SetBool("isDodge", false);
        }
    }
    protected override float CalculateYVelocity()
    {
        var isJump = _direction.y > 0;
        var Yvelocity = _rigidbody.velocity.y;

        if (_getDoubleJump)
        {
            if (_isGrounded) _allowDoubleJump = true;
        }

        return base.CalculateYVelocity();
    }
    protected override float CalculateJumpVelocity(float Yvelocity)
    {

        if (_isGrounded)
        {
            _sounds.Play("Jump");

            _JumpParticles.Spawn();
            _allowDoubleJump = false;
            return _jumpSpeed;
        }

        return base.CalculateJumpVelocity(Yvelocity);
    }

    public void AddInInventory(string id, int value)
    {
        _session.Data.Inventory.Add(id, value);

        if (id == "Sword")
        {
            _sounds.Play("Sword");
        }
    }

    public override void TakeDamage()
    {
        base.TakeDamage();


    }
    public void Interact()
    {
        _interactionCheck.Check();
    }
    public void GetPower()
    {
        _getDoubleJump = true;
        Invoke("LosePower", 5);
    }
    private void LosePower()
    {
        _getDoubleJump = false;
    }
    public void SpawnFootDust()
    {
        _particles.Spawn("Run");
    }
    public void SpawnLandingDust()
    {
        _particles.Spawn("Landing");
    }
    public void SpawnAttackPartickle()
    {
        _particles.Spawn("Attack");
    }

    
    public override void Attack()
    {
        if (SwordCount <= 0) return;
        if (_meleeCooldown.IsReady)
        {
            base.Attack();
            _meleeCooldown.Reset();
        }
        
    }
    public void UpdateHeroWeapon()
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;
        Debug.Log(_animator.runtimeAnimatorController);
    }

    public void OnHealthChanged(int currentHealth)
    {
        _session.Data.Hp.Value = currentHealth;
    }
    public void OnDoThrow()
    {
        _particles.Spawn("Throw");
    }

    public void Throw()
    {
        if (_throwCooldown.IsReady & _session.Data.Inventory.Count("Sword") > 1)
        {
            _session.Data.Inventory.Remove("Sword", 1);
            _animator.SetTrigger("Throw");
            _throwCooldown.Reset();
            _sounds.Play("Range");
        }
    }

    public void Dodge()
    {
        coll.size = new Vector2(0.7f, 0.25f);
        _animator.SetBool("isDodge", true);
    }

    public void Heal()
    {
        if (_session.Data.Inventory.Count("HealPotion") > 0)
        {
            _session.Data.Inventory.Remove("HealPotion", 1);

            var healthComponent = GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.HealHP(5);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>()._session.Data.Hp.Value += 5;
                _sounds.Play("Heal");
            }
        }
        
        
    }

}

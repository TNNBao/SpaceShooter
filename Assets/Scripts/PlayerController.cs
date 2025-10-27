using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private InputSystem_Actions inputSystem_Actions;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 playerDirection;
    public float boost = 1f;
    private bool boosting = false;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float boostMultiplier = 4f;
    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegen;
    [SerializeField] private GameObject destroyEffect;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        inputSystem_Actions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        energy = maxEnergy;
    }

    private void Start()
    {  
        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
    }

    private void OnEnable()
    {
        // Bật Action Map
        inputSystem_Actions.Player.Enable();

        // Gắn sự kiện khi nhấn và thả phím Move
        inputSystem_Actions.Player.Move.performed += OnMove;
        inputSystem_Actions.Player.Move.canceled += OnMoveCanceled;

        // Gắn sự kiện khi nhấn và thả phím Boost
        inputSystem_Actions.Player.Boost.started += OnBoostStarted;
        inputSystem_Actions.Player.Boost.canceled += OnBoostCanceled;
    }

    private void OnDisable()
    {
        // Tháo sự kiện Move
        inputSystem_Actions.Player.Move.performed -= OnMove;
        inputSystem_Actions.Player.Move.canceled -= OnMoveCanceled;

        // Tháo sự kiện Boost
        inputSystem_Actions.Player.Boost.started -= OnBoostStarted;
        inputSystem_Actions.Player.Boost.canceled -= OnBoostCanceled;

        inputSystem_Actions.Player.Disable();
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            animator.SetFloat("moveX", playerDirection.x);
            animator.SetFloat("moveY", playerDirection.y);

            if (boost > 1f)
            {
                animator.SetBool("boosting", true);
            }
            else
            {
                animator.SetBool("boosting", false);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + playerDirection * moveSpeed * Time.fixedDeltaTime);

        if (boosting)
        {
            if (energy > 0.2f) energy -= 0.2f;
            else
            {
                boost = 1f;
                boosting = false;
            }
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += energyRegen;
            }
        }
        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        playerDirection = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        playerDirection = Vector2.zero;
    }

    private void OnBoostStarted(InputAction.CallbackContext context)
    {
        boost = boostMultiplier;
        boosting = true;
    }

    private void OnBoostCanceled(InputAction.CallbackContext context)
    {
        boost = 1f;
        boosting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        boost = 0f;
        gameObject.SetActive(false);
        Instantiate(destroyEffect, transform.position, transform.rotation);
        GameManager.Instance.GameOver();
    }
}

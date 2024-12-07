using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] JoyStick aimStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float turnSpeed = 20f;
    [SerializeField] float animTurnSpeed = 20f;

    [Header("Inventory")]
    [SerializeField] InventoryComponent inventoryComponent;

    [Header("Health")]
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] PlayerHealthBar healthBar;

    [Header("Death Screen")]
    [SerializeField] CanvasGroup deathScreenCanvasGroup; // 死亡屏幕 UI 的 CanvasGroup
    [SerializeField] TextMeshProUGUI deathMessageText; // 死亡信息文本
    [SerializeField] float fadeDuration = 2f; // 淡入淡出持续时间

    Animator animator;
    Camera mainCamera;
    CameraController cameraController;

    Vector2 moveInput;
    Vector2 aimInput;
    private bool isDead = false; // 是否死亡状态

    [SerializeField] AudioSource damageAudio;
    [SerializeField] AudioSource deathAudio;
    [SerializeField] AudioSource monsterEatAudio;
    // Start is called before the first frame update


    void Start()
    {
        moveStick.OnStickValueUpdated += moveStickUpdated;
        aimStick.OnStickValueUpdated += aimStickUpdated;
        aimStick.OnStickTabed += StartSwitchWeapon;

        mainCamera = Camera.main;
        cameraController = FindAnyObjectByType<CameraController>();
        animator = GetComponent<Animator>();

        healthComponent.onHealthChanged += HealthChanged;
        healthComponent.onHealthEmpty += OnDeath; // 监听死亡事件
        healthComponent.BroadcastHealthValueImmediately();

        // 初始化死亡屏幕
        if (deathScreenCanvasGroup != null)
        {
            deathScreenCanvasGroup.alpha = 0; // 屏幕开始时完全透明
        }
        if (deathMessageText != null)
        {
            deathMessageText.enabled = false; // 文本开始时隐藏
        }

        damageAudio = GetComponent<AudioSource>();
        deathAudio = GetComponent<AudioSource>();
        monsterEatAudio = GetComponent<AudioSource>();
    }

    private void HealthChanged(float health, float delta, float maxHealth)
    {
        healthBar.UpdateHealth(health, delta, maxHealth);
    }

    private void OnDeath()
    {
        if (isDead) return;
        isDead = true;

        // 停止角色控制器和动作
        characterController.enabled = false;
        moveStick.OnStickValueUpdated -= moveStickUpdated;
        aimStick.OnStickValueUpdated -= moveStickUpdated;

        // 设置死亡动画触发器
        animator.SetTrigger("Die");


        deathAudio.Play();

        // 开始显示死亡屏幕
        StartCoroutine(ShowDeathScreenAndReload());
    }

    private IEnumerator ShowDeathScreenAndReload()
    {
        // 添加延迟，等待 1 秒后开始黑屏（可调整）
        yield return new WaitForSeconds(1.5f);

        // 屏幕淡入黑色
        if (deathScreenCanvasGroup != null)
        {
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                deathScreenCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration); // 线性插值改变透明度
                yield return null;
            }
        }

        // 显示 "You Die" 文本
        if (deathMessageText != null)
        {
            deathMessageText.enabled = true;
            deathMessageText.text = "You Die";
            monsterEatAudio.Play();
        }

        // 等待 2 秒后重新加载场景
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void AttackPoint()
    {
        inventoryComponent.GetActiveWeapon().Attack();
    }

    void StartSwitchWeapon()
    {
        animator.SetTrigger("SwitchWeapon");
    }

    public void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();
    }

    private void moveStickUpdated(Vector2 inputValue)
    {
        if (isDead) return;
        moveInput = inputValue;
    }

    private void aimStickUpdated(Vector2 inputValue)
    {
        if (isDead) return;
        aimInput = inputValue;

        if (aimInput.magnitude > 0)
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    Vector3 StickInputToWorldDir(Vector2 inputVal)
    {
        Vector3 rightDir = mainCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return rightDir * inputVal.x + upDir * inputVal.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        PerformMoveAndAim();
        UpdateCamera();
    }

    private void PerformMoveAndAim()
    {
        Vector3 moveDir = StickInputToWorldDir(moveInput);
        characterController.Move(moveDir * Time.deltaTime * moveSpeed);
        UpdateAim(moveDir);

        float forward = Vector3.Dot(moveDir, transform.forward);
        float right = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("ForwardSpeed", forward);
        animator.SetFloat("RightSpeed", right);
    }

    private void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 aimDir = currentMoveDir;

        if (aimInput.magnitude != 0)
        {
            aimDir = StickInputToWorldDir(aimInput);
        }

        RotateTowards(aimDir);
    }

    private void UpdateCamera()
    {
        if (moveInput.magnitude != 0 && aimInput.magnitude == 0 && cameraController != null)
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }

    private void RotateTowards(Vector3 aimDir)
    {
        float currentTurnSpeed = 0;
        if (aimDir.magnitude != 0)
        {
            Quaternion prevRot = transform.rotation;
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), turnLerpAlpha);

            Quaternion currentRot = transform.rotation;

            float Dir = Vector3.Dot(aimDir, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(prevRot, currentRot) * Dir;
            currentTurnSpeed = rotationDelta / Time.deltaTime;
        }

        animator.SetFloat("TurningSpeed", currentTurnSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBall1"))
        {
            float damageAmount = -10f;
            healthComponent.ChangeHealth(damageAmount, other.gameObject);
            damageAudio.Play();

        }
        if (other.CompareTag("EnemyBall2"))
        {
            float damageAmount = -20f;
            healthComponent.ChangeHealth(damageAmount, other.gameObject);
            damageAudio.Play();
        }
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime; // Время, в течение которого игрок может находиться в воздухе перед прыжком
    private float coyoteCounter; // Время, прошедшее с момента, когда игрок оттолкнулся от края платформы

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps; // Дополнительные прыжки, которые может совершить игрок
    private int jumpCounter; // Количество доступных дополнительных прыжков

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX; // Горизонтальная сила прыжка от стены
    [SerializeField] private float wallJumpY; // Вертикальная сила прыжка от стены

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer; // Слой, представляющий землю
    [SerializeField] private LayerMask wallLayer; // Слой, представляющий стены

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound; // Звук прыжка

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        // Получаем ссылки на компоненты Rigidbody2D и Animator объекта
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // Получаем ввод горизонтального движения от игрока

        // Поворот игрока при движении влево-вправо
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Устанавливаем параметры аниматора
        anim.SetBool("Run", horizontalInput != 0); // Анимация бега
        anim.SetBool("Grounded", isGrounded()); // Анимация нахождения на земле

        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        // Регулировка высоты прыжка
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; // Сброс счетчика coyote при нахождении на земле
                jumpCounter = extraJumps; // Сброс счетчика прыжков до значения extraJumps
            }
            else
                coyoteCounter -= Time.deltaTime; // Начало уменьшения счетчика coyote, если не на земле
        }
    }

    private void Jump()
    {
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return;
        // Если счетчик coyote меньше или равен 0, игрок не находится на стене и нет доступных дополнительных прыжков, ничего не делаем

        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
            WallJump();
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                // Если не на земле и счетчик coyote больше 0, совершаем обычный прыжок
                if (coyoteCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                }
                else
                {
                    if (jumpCounter > 0) // Если у нас есть дополнительные прыжки, то прыгаем и уменьшаем счетчик прыжков
                    {
                        // Выполняем анимацию двойного прыжка
                        // anim.SetTrigger("DoubleJump");
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            // Сбрасываем счетчик coyote до 0, чтобы избежать двойных прыжков
            coyoteCounter = 0;
        }
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }


}
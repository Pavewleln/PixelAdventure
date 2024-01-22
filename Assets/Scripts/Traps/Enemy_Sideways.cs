using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance; // Расстояние, на которое будет двигаться враг влево и вправо
    [SerializeField] private float speed; // Скорость движения врага
    [SerializeField] private float damage; // Урон, который враг нанесет игроку
    private bool movingLeft; // Флаг, определяющий направление движения врага
    private float leftEdge; // Крайняя левая позиция, до которой будет двигаться враг
    private float rightEdge; // Крайняя правая позиция, до которой будет двигаться враг

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance; // Вычисляем крайнюю левую позицию
        rightEdge = transform.position.x + movementDistance; // Вычисляем крайнюю правую позицию
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge) // Если враг не достиг крайней левой позиции
            {
                // Двигаем врага влево
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false; // Меняем направление на право
            }
        }
        else
        {
            if (transform.position.x < rightEdge) // Если враг не достиг крайней правой позиции
            {
                // Двигаем врага вправо
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true; // Меняем направление на лево
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // Если враг столкнулся с игроком
        {
            collision.GetComponent<Health>().TakeDamage(damage); // Наносим урон игроку
        }
    }
}
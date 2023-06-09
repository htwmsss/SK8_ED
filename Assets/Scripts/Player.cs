using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 10f;           // Velocidad del personaje
    public float jumpForce = 700f;      // Fuerza del salto
    public LayerMask whatIsGround;      // M�scara de la capa para detectar el suelo
    public Transform groundCheck;       // Posici�n del objeto para detectar el suelo
    public Animator animator;           // Referencia al componente Animator
    private bool grounded = false;      // Booleano para determinar si el personaje est� en el suelo
    private float groundRadius = 0.2f;  // Radio para detectar el suelo
 
    private void OnCollisionEnter(Collision collision)
    {
        // Si el jugador colisiona con algo, se reinicia el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


// Update is called once per frame
void Update()
    {
        // Mover el personaje hacia la izquierda o la derecha
        float move = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed, GetComponent<Rigidbody2D>().velocity.y);

        // Detectar si el personaje est� en el suelo
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        animator.SetBool("Grounded", grounded);

        // Si el personaje est� en el suelo y presiona el bot�n de saltar, saltar
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("Jump");
        }

        // Si el personaje est� saltando y presiona el bot�n de realizar truco, hacer truco
        if (!grounded && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Trick");
        }

        // Mover el �rea de colisi�n del personaje alrededor de su posici�n
        Collider2D hit = Physics2D.OverlapBox(transform.position, new Vector2(0.1f, 0.1f), 0, LayerMask.GetMask("Ground"));

        // Si el personaje est� en contacto con el suelo, entonces est� "grounded"
        if (hit != null)
        {
            Debug.Log("Grounded");
        }
        else
        {
            Debug.Log("Not grounded");
        }

        // Mover el personaje hacia la izquierda o la derecha con las teclas de flecha
        if (move == 0)
        {
            animator.SetBool("avanzar", false);
        }
        else
        {
            animator.SetBool("avanzar", true);
        }


    }
}
public class DoubleJump : MonoBehaviour
{
    public float jumpForce = 10f;
    public int maxJumps = 2;
    private int jumpsLeft;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = maxJumps;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            rb.velocity = Vector2.up * jumpForce; // Aplicar la fuerza de salto
            jumpsLeft--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpsLeft = maxJumps;
        }
    }


}








using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;           // Velocidad del personaje
    public float jumpForce = 700f;      // Fuerza del salto
    public LayerMask whatIsGround;      // Máscara de la capa para detectar el suelo
    public Transform groundCheck;       // Posición del objeto para detectar el suelo
    public Animator animator;           // Referencia al componente Animator
    private bool grounded = false;      // Booleano para determinar si el personaje está en el suelo
    private float groundRadius = 0.2f;  // Radio para detectar el suelo

    // Update is called once per frame
    void Update()
    {
        // Mover el personaje hacia la izquierda o la derecha
        float move = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed, GetComponent<Rigidbody2D>().velocity.y);

        // Detectar si el personaje está en el suelo
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        animator.SetBool("Grounded", grounded);

        // Si el personaje está en el suelo y presiona el botón de saltar, saltar
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("Jump");
        }

        // Si el personaje está saltando y presiona el botón de realizar truco, hacer truco
        if (!grounded && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Trick");
        }

        // Mover el área de colisión del personaje alrededor de su posición
        Collider2D hit = Physics2D.OverlapBox(transform.position, new Vector2(0.1f, 0.1f), 0, LayerMask.GetMask("Ground"));

        // Si el personaje está en contacto con el suelo, entonces está "grounded"
        if (hit != null)
        {
            Debug.Log("Grounded");
        }
        else
        {
            Debug.Log("Not grounded");
        }

        // Mover el personaje hacia la izquierda o la derecha con las teclas de flecha
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}









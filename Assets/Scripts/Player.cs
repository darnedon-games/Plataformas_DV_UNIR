using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputH;

    [Header("Sistema de movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private Transform pies;
    [SerializeField] private float distanciaDeteccionSuelo;
    [SerializeField] private LayerMask queEsSaltable;
    
    [Header("Sistema de combate")]
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float danhoAtaque;
    [SerializeField] private LayerMask queEsDanhable;

    [SerializeField] private TextMeshProUGUI textoGemas;
    private int contadorGemas = 0;

    private Image barraVidaPlayer;

    [SerializeField] private GameObject infoBoss;

    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        barraVidaPlayer = GameObject.Find("Canvas/InfoPlayer/Vidas/BarraVida").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();

        Saltar();

        LanzarAtaque();
    }

    private void LanzarAtaque()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
    }

    // Se ejecuta desde Evento de Animaci�n
    private void Ataque()
    {
        // Lanzar trigger instant�neo
        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanhable);
        foreach (Collider2D item in collidersTocados)
        {
            SistemaVidas sistemaVidasEnemigos = item.gameObject.GetComponent<SistemaVidas>();
            sistemaVidasEnemigos.RecibirDanho(danhoAtaque);
        }
    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstoyEnSuelo())
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
    }

    private bool EstoyEnSuelo()
    {
        Debug.DrawRay(pies.position, Vector3.down, Color.red, 0.3f);
        return Physics2D.Raycast(pies.position, Vector3.down, distanciaDeteccionSuelo, queEsSaltable);
    }

    private void Movimiento()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(inputH * velocidadMovimiento, rb.linearVelocity.y);

        if (inputH != 0) // Si hay movimiento
        {
            anim.SetBool("running", true);
            if (inputH > 0) // Se mueve hacia la derecha
            {
                transform.eulerAngles = Vector3.zero;
            }
            else // Se mueve hacia la izquierda
            {
                transform.eulerAngles = new Vector3(0,180,0);
            }
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Gema"))
        {
            contadorGemas += 1;
            textoGemas.text = contadorGemas + "/10";
            Destroy(elOtro.gameObject);
        }
        else if (elOtro.gameObject.CompareTag("BossZone"))
        {
            infoBoss.SetActive(true);
        }
        else if (elOtro.gameObject.CompareTag("FinMapa"))
        {
            // Jugador se cae al vac�o
            barraVidaPlayer.fillAmount = 0;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("BossZone"))
        {
            infoBoss.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }
}

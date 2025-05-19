using UnityEngine;
using UnityEngine.Pool;

public class BolaFuego : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float impulsoDisparo;
    [SerializeField] private float danhoAtaque;

    public ObjectPool<BolaFuego> PoolBolaFuego { get; set; }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * impulsoDisparo, ForceMode2D.Impulse);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    //transform.forward --> MI EJE Z (AZUL)
    //    //transform.up --> MI EJE Y (VERDE)
    //    //transform.right --> MI EJE X (ROJO)
    //    //rb.AddForce(transform.right * impulsoDisparo, ForceMode2D.Impulse);
        
    //    rb.AddForce(transform.right * impulsoDisparo, ForceMode2D.Impulse);
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("PlayerHitBox"))
        {
            SistemaVidas sistemaVidasPlayer = elOtro.gameObject.GetComponent<SistemaVidas>();
            sistemaVidasPlayer.RecibirDanho(danhoAtaque);
            PoolBolaFuego.Release(this);
        }

        else if (elOtro.gameObject.CompareTag("Colisiones"))
        {
            PoolBolaFuego.Release(this);
        }
    }
}

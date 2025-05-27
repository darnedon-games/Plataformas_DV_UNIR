using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;
    public float Vidas { get => vidas;} // Se encapsula para poder acceder desde Script/Clase Boss

    [SerializeField] private Image barraVidaPlayer;
    [SerializeField] private Image barraVidaBoss;

    private SpriteRenderer spriteRenderer;
    private Color colorParpadeo = Color.red;
    private float duracionParpadeo = 0.1f;
    private int numParpadeos = 3;
    private Color colorOriginal;

    [SerializeField] Boss boss;

    private GameObject gameOverCanvas;
    private GameObject winCanvas;

    private AudioSource music;

    private Animator animBoss;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOriginal = spriteRenderer.color;

        if (boss != null) { 
            animBoss = boss.GetComponent<Animator>();
        }

        gameOverCanvas = GameObject.Find("CanvasGameOver");
        winCanvas = GameObject.Find("CanvasWin");

        music = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    public void RecibirDanho(float danhoRecibido)
    {
        IniciarParpadeo();

        vidas -= danhoRecibido;

        // Reducimos también la barra de vida del Player de la UI
        if (this.gameObject.CompareTag("PlayerHitBox")){
            barraVidaPlayer.fillAmount = (float)(barraVidaPlayer.fillAmount - (danhoRecibido * 0.01));
        }

        // Reducimos también la barra de vida del Boss de la UI
        if (this.gameObject.CompareTag("Boss"))
        {
            barraVidaBoss.fillAmount = (float)(barraVidaBoss.fillAmount - (danhoRecibido * 0.005));
        }

        if (vidas <= 0) {
            if (this.gameObject.CompareTag("Boss"))
            {
                StartCoroutine(DestruirBossConDelay()); // Para que de tiempo a la anim de muerte del Boss
            }
            else if (this.gameObject.CompareTag("PlayerHitBox"))
            {
                gameOverCanvas.transform.Find("Panel").gameObject.SetActive(true);
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator DestruirBossConDelay()
    {
        animBoss.SetTrigger("death");
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        winCanvas.transform.Find("Panel").gameObject.SetActive(true); // Se muestra pantalla de victoria
    }

    private void IniciarParpadeo()
    {
        StartCoroutine(Parpadeo());
    }

    IEnumerator Parpadeo()
    {
        for (int i = 0; i < numParpadeos; i++)
        {
            spriteRenderer.color = colorParpadeo;
            yield return new WaitForSeconds(duracionParpadeo);
            spriteRenderer.color = colorOriginal;
            yield return new WaitForSeconds(duracionParpadeo);
        }
    }
}

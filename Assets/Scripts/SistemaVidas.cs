using Unity.VisualScripting;
using UnityEngine;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;

    public void RecibirDanho(float danhoRecibido)
    {
        vidas -= danhoRecibido;
        if (vidas <= 0) {
            Destroy(this.gameObject);
        }
    }
}

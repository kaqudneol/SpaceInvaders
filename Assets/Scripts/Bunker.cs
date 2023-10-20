using UnityEngine;

public class Bunker : MonoBehaviour
{
    [SerializeField]public int hp = 0;
    [SerializeField]public int initialhp = 0;
    private Material material;
    private Color initialColor; // Almacena el color inicial

    private void Start()
    {
        // Asumiendo que tienes un material configurado en tu sprite
        material = GetComponent<Renderer>().material;
        initialColor = material.color; // Guarda el color inicial
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") || other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            
            hp--;
            Destruction();
            if (hp <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Destruction()
    {
        // Calcula el factor de oscurecimiento en base a la vida actual
        float proportionalFactor = (float)hp / initialhp; // Cambia 3 al valor máximo de vida

        // Aplica el color oscurecido al material, ahora se diferencia mas
        // Color newColor = initialColor * proportionalFactor;
        Color newColor = new Color(initialColor.r * proportionalFactor, initialColor.g * proportionalFactor, initialColor.b * proportionalFactor, initialColor.a);
        material.color = newColor;
    }
}

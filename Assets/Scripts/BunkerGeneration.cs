using UnityEngine;

public class BunkerGeneration : MonoBehaviour
{
    [SerializeField]
    public int bunkerHp=5;
    // Método para modificar la variable en todos los objetos hijos

    public void Start()
    {
        ModifyChildVariable();
    }

    public void ModifyChildVariable()
    {
        Bunker[] children = GetComponentsInChildren<Bunker>();
        foreach (Bunker child in children)
        {
            child.hp = bunkerHp; // Cambia el valor según tus necesidades
            child.initialhp=bunkerHp;
        }
    }
}

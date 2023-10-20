
using UnityEngine;

public class Invader : MonoBehaviour
{
    public string enemyType;
    public int ppKill = 10;
    public UpdateScore pointCounter; // Debes asignar esto desde el Inspector.

    public Sprite[] animationSprites;                       //Number of sprites (frames)
    public float animationTime = 1.0f;                      //Time between animations

    public System.Action killed;

    private SpriteRenderer _spriteRenderer;                 //Sprite Render
    private int _animationFrame;                            //Current animation frame
     
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();   // This will search for the component (sprite)
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime); //It calls a method, and then repeats it
        pointCounter = FindObjectOfType<UpdateScore>();
    }

    private void AnimateSprite(){
        _animationFrame++;

        if(_animationFrame >= this.animationSprites.Length)
        {
            _animationFrame= 0;
        }

        _spriteRenderer.sprite = this.animationSprites[_animationFrame]; 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {        
            enemyType = this.gameObject.name.Replace("(Clone)", "").Trim(); 

            switch (enemyType)
            {
                case "Enemigo1":
                    pointCounter.IncreaseScore(ppKill);
                    break;
                case "Enemigo2":
                    pointCounter.IncreaseScore(ppKill*2);
                    break;
                case "Enemigo3":
                    pointCounter.IncreaseScore(ppKill*5);
                    break;
            }
            this.killed.Invoke();
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Bunker"))
        {
            this.killed.Invoke();
            Destroy(gameObject);
        }
    }

}

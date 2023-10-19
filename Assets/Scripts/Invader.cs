using UnityEngine;

public class Invader : MonoBehaviour
{
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser") || other.gameObject.layer == LayerMask.NameToLayer("Bunker"))
        {        
            this.killed.Invoke();
            Destroy(gameObject);
            pointCounter.IncreaseScore(ppKill);

        }
    }

}

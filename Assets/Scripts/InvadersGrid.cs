using UnityEngine;
using UnityEngine.SceneManagement;
public class InvadersGrid : MonoBehaviour
{
    
    public Invader[] prefabs;             // Array for the enemies
    public int rows = 5;                    // Number of rows
    public int columns = 11;                // Number of columns

    public  AnimationCurve speed;   // X Y , % and speed
    public Projectile missilePrefav;
    public int amountKilled { get; private set;}  // Public getter private setter
    public int totalInvaders => this.rows *this.columns; // Total amout, row x column
    public int amountAlive => this.totalInvaders - this.amountKilled;              //Amount alive ==  total invaders - amountKilled  
    public float percentKilled => (float)this.amountKilled/(float)this.totalInvaders;

    public float missileAttackRate = 1.0f;

    private Vector3 _direction= Vector2.right;
    private void Awake(){
        for (int row = 0; row < this.rows; row++)
        {

            float width = 2.0f * (this.columns-1);
            float height = 2.0f * (this.rows -1);
            Vector2 centering = new Vector2(-width/2 ,  -height/2);         //Grid center

            Vector3 rowPosition = new Vector3(centering.x, centering.y+(row * 2.0f), 0.0f);

            for (int col=0; col < this.columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform );
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col*2.0f;
                invader.transform.localPosition= position;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack),this.missileAttackRate, this.missileAttackRate);
    }


    private void Update()
    {                                           // Increases speed when enemy killed
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        // To check the position

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach(Transform invader in this.transform)//We loop for each invader ( has this touched the edge?)
        {
            if(!invader.gameObject.activeInHierarchy)
            {   
                continue;
            }

            if((_direction == Vector3.right) && (invader.position.x >= (rightEdge.x - 1.0f )))
            {
                AdvancedRow();
            }
            else if((_direction == Vector3.left) && (invader.position.x <= (leftEdge.x + 1.0f)))
            {
                AdvancedRow();
            }
        }
    }


    private void AdvancedRow()
    {
        _direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 1.0f;                             //Distancia que se desplaza ahcia abajo
        this.transform.position = position;

    }

    private void MissileAttack()
    {
        //Spawn missile , low the more there are alive smaller chance
        foreach(Transform invader in this.transform)    
        {
            if(!invader.gameObject.activeInHierarchy)
            {   
                continue;
            }
            if(Random.value < (1.0f/(float)this.amountAlive))
            {
                Instantiate(this.missilePrefav, invader.position, Quaternion.identity);
                break;
            }
        }
    }    

    public void InvaderKilled()
    {
        this.amountKilled++;

        if(this.amountKilled >= this.totalInvaders)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
}

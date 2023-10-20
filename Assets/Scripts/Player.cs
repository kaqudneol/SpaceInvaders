
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Projectile laserPrefab;
    public float speed = 5.0f;

    private bool _laserActive;


    //Cambiar las variables para cuando la pantalla se aumente
    Vector3 rightEdge = new Vector3(14.0f, 0.0f, 0.0f);
    Vector3 leftEdge = new Vector3(-14.0f, 0.0f, 0.0f);
    private void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(this.transform.position.x> leftEdge.x)
            {
                this.transform.position += Vector3.left *this.speed *Time.deltaTime;
            }
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if(this.transform.position.x<rightEdge.x)           
            {
                this.transform.position += Vector3.right *this.speed *Time.deltaTime;
            }
        }    


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }


    private void Shoot()
    {   
        if(!_laserActive)
        {
            Projectile projectile =Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }

    private void LaserDestroyed()
    {
        _laserActive = false;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Invader")||other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

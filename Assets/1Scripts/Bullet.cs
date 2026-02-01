using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    private void OnCollisionEnter(Collision collision)
    {
        if(!isMelee && collision.gameObject.tag=="Floor")
        {
            Destroy(gameObject, 3);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Wall") 
        {
            Destroy(gameObject);
        }
    }
}

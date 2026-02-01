using UnityEngine;

public class Missile : MonoBehaviour
{
    
    void Update()
    {
        transform.Rotate(Vector3.right * 90 * Time.deltaTime); 
    }
}
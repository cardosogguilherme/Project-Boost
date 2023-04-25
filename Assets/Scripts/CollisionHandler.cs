using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag) 
        {
            case "Friendly":
                break;
            case "Fuel":
                break;
            case "Finish":
                break;
            default: 
                break;
        }
    }
}

using UnityEngine;

public class EndpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.TriggerEndpoint();
        }
    }
}

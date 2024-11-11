using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    private BaseResource _resource;

    public void Instantiate(BaseResource resource)
    {
        _resource = resource;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInChildren<Player>())
        {
            _resource.Collect();
            Destroy(gameObject);
        }
    }
}

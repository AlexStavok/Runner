using UnityEngine;

public class EnviromentHider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<EnviromentObject>(out var enviromentObject))
        {
            enviromentObject.BackToPool();
        }
    }
}

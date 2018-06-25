using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    
    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }

}

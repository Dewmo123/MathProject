using UnityEngine;
using UnityEngine.Events;

public class Health : Stat
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeValue(-10);
        }
    }
}

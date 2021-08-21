using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaBoom : MonoBehaviour, IDropable
{
    public void GivePower()
    {
        Destroy(this.gameObject);
    }

    public IEnumerator PlaySound()
    {
        throw new System.NotImplementedException();
    }
}

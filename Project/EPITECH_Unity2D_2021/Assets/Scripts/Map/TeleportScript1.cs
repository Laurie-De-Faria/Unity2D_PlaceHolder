using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript1 : MonoBehaviour
{
    public Transform Portal;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelEvent.Changelevel = true;
            other.transform.position = Portal.position;
        }
    }
}
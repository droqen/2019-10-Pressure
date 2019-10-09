using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour
{

    public bool powered_up { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ReactionAvatar>() != null)
        {
            powered_up = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void Setup()
    {
        powered_up = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Power_Up()
    {
        powered_up = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }

}

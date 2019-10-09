using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAvatar : MonoBehaviour
{

    public float speed = 5f;

    float stunned = 0;

    Rigidbody2D body {  get { return GetComponent<Rigidbody2D>(); } }

    private void Update()
    {
        //body.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed;
        if (stunned > 0)
        {
            stunned -= Time.deltaTime;
            if (stunned <= 0)
            {
                GetComponent<navdi3.jump.Jumper>().enabled = true;
                GetComponent<navdi3.jump.JumperExamplePlayer>().enabled = true;
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = Random.value < .5f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SmokeBullet>())
        {
            if (Random.value < .5f)
                GetComponent<navdi3.jump.Jumper>().enabled = false;
            else
                GetComponent<navdi3.jump.JumperExamplePlayer>().enabled = false;
            stunned = 1;
        }
    }
}

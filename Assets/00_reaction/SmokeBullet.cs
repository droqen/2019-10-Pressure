using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBullet : MonoBehaviour
{
    public navdi3.SpriteLot sprites;

    public void Setup(float speed, Transform target, float angleOffset)
    {
        var toTarget = target.position - this.transform.position;
        toTarget = Quaternion.Euler(0, 0, angleOffset) * toTarget;
        GetComponent<Rigidbody2D>().velocity = toTarget.normalized * speed;
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(20,30)];
    }

    private void Update()
    {
        var r = Random.Range(0, 200);
        if (r >= 20 && r < 30)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[r];
            GetComponent<Rigidbody2D>().velocity += (Vector2)(Quaternion.Euler(0, 0, Random.value * 360) * Vector2.right);
        }
        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x + 0.1f < 0 ||
            viewportPosition.y + 0.1f < 0 ||
            viewportPosition.x - 0.1f > 1 ||
            viewportPosition.y - 0.1f > 1 ){
            Object.Destroy(this.gameObject);
        }
    }
}

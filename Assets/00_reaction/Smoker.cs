using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoker : MonoBehaviour
{
    public float[] bullet_speeds = {   5, 10, 15 };
    public float[] bullet_angles = { -10,  0, 10 };
    public float bullet_delay = 3;
    public float screen_width = 160;

    float toNextShot = 0;

    reaction_xxi xxi;

    public void Setup(reaction_xxi xxi)
    {
        this.xxi = xxi;
        this.Update();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * (
            (Mathf.Sin(Time.time * 0.24f)+1) * (screen_width*0.5f) - this.transform.position.x );

        toNextShot -= Time.deltaTime;
        if (toNextShot <= 0)
        {
            toNextShot = bullet_delay;

            // spawn a bunch of bullets.
            foreach (var speed in bullet_speeds)
                foreach (var angle in bullet_angles)
                    xxi.banks["gross_smoke"].Spawn<SmokeBullet>(xxi.GetEntLot("bads"), this.transform.position)
                        .Setup(speed, xxi.GetEntLot("players").transform.GetChild(0), angle);
        }
    }
}

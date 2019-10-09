using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using navdi3;

public class reaction_xxi : navdi3.xxi.BaseTilemapXXI
{
    float current_timer;
    public TMPro.TMP_Text timer_text;
    public float number_of_machines = 2;
    public float number_of_machines_difficulty_delta = .25f;
    public float machine_timer = 10;
    public float machine_timer_difficulty_delta = -.25f;
    public int wave = 0;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bad"), LayerMask.NameToLayer("Bad"));

        InitializeManualTT();
        var bounds = new twinrect(-1, 0, 20, 10 - 1);
        var innerbounds = new twinrect(bounds.min+twin.one, bounds.max-twin.one);
        bounds.DoEach(cell =>
        {
            var EntPos = tilemap.layoutGrid.GetCellCenterWorld(cell);
            if (innerbounds.Contains(cell))
            {
                int storey = cell.y / 3 % 2 + 1;
                if (cell.y % 3 == 0)
                {
                    if (cell.x % 4 != (storey))
                        Sett(cell, 10);
                }
                if (cell.y % 3 == 1)
                {
                    if (cell.x % 4 == (storey + 2) % 4)
                    {
                        if (GetEntLot("machines").IsEmpty())
                            banks["avatar"].Spawn(GetEntLot("players"), EntPos);

                        banks["machine"].Spawn<Machine>(GetEntLot("machines"), EntPos).Setup();
                    }
                }
            }
            else
            {
                Sett(cell, 10);
            }
        });

        banks["gross_smoker"].Spawn<Smoker>(GetEntLot("bads"), new Vector3(84,120)).Setup(this);
    }

    private void Update()
    {
        bool complete = true;
        foreach (Transform mt in GetEntLot("machines").transform)
        {
            if (mt.GetComponent<Machine>().powered_up)
            {
                complete = false; break;
            }
        }

        if (complete)
        {
            var machines = GetEntLot("machines").transform.GetComponentsInChildren<Machine>();
            navdi3.Util.shufl(ref machines);
            for(int i = 0; i < number_of_machines; i++)
            {
                machines[i].Power_Up();
            }
            current_timer = machine_timer;

            // gets harder each time
            machine_timer += machine_timer_difficulty_delta;
            number_of_machines += number_of_machines_difficulty_delta;
            wave++;
        } else
        {
            current_timer -= Time.deltaTime;
            if (current_timer <= 0)
            {
                // you lose!
                GetEntLot("players").transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (GetEntLot("players").transform.GetChild(0).gameObject.activeSelf)
        {
            timer_text.text = "wave " + wave + "\n" + Mathf.CeilToInt(current_timer).ToString();
        } else
        {
            timer_text.text = "game over\ncleared " + wave + " waves";
        }

    }

    public override int[] GetSolidTileIds()
    {
        return new int[] { 10, };
    }
    public override int[] GetSpawnTileIds()
    {
        return new int[0];
    }
    public override void SpawnTileId(int TileId, Vector3Int TilePos)
    {
        throw new System.NotImplementedException();
    }
}

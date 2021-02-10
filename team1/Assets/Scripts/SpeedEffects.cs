using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class SpeedEffects : MonoBehaviour
{

    private Player player;
    private float defaultFOV;
    public ParticleSystem speedLines;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        defaultFOV = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        // Camera.main.fieldOfView = Mathf.Max(defaultFOV, 0.25f * Mathf.Sqrt(player.GetSpeed()) + defaultFOV);
        var emission = speedLines.emission;
        emission.rateOverTime = Mathf.Min(80, Mathf.Sqrt(player.GetSpeed() - 300));
    }
}

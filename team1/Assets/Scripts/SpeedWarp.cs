using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class SpeedWarp : MonoBehaviour
{
    public Player player;

    private PostProcessVolume volume;
    private LensDistortion distortion;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out distortion);
    }

    // Update is called once per frame
    void Update()
    {
        distortion.intensity.value = Mathf.Max(-60, -1.5f * Mathf.Sqrt(player.GetSpeed()));
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class PostProcessing : MonoBehaviour
{
    private PostProcessVolume volume;
    private LensDistortion distortion;
    private Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out distortion);
        volume.profile.TryGetSettings(out vignette);
    }

    public void SpeedEffects(float playerSpeed)
    {
        distortion.intensity.value = Mathf.Max(-60, -1.5f * Mathf.Sqrt(playerSpeed));
    }

    public void TimeWarpEffects(bool on)
    {
        vignette.intensity.value = on ? 0.5f : 0;
    }
}

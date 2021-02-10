using UnityEngine;


public class TimeShift : Gear
{
    public float slowdownGage = 0.05f;
    public float timeEffectLength = 2f;
    private float original_time;
    private float currentShiftTime;
    bool shiftTimeActive;

    // Update is called once per frame
    void Update()
    {
        if (shiftTimeActive)
        {
            currentShiftTime += Time.deltaTime;
            if (currentShiftTime > timeEffectLength)
            {
                restoreTime();
            }
        }
        currentCooldown -= Time.deltaTime;
    }

    protected override void Activate()
    {
        if (currentCooldown <= 0)
        {
            Time.timeScale = slowdownGage;
            original_time = Time.fixedDeltaTime;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            shiftTimeActive = true;
            currentCooldown = cooldown;
            currentShiftTime = 0f;
        }

    }

    // restore time back to normal 
    private void restoreTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = original_time;
        shiftTimeActive = false;
    }
}

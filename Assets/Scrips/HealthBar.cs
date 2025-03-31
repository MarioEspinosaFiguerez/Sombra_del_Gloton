using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float hungryDecayAmount = 1.0f;
    public float hungryDecayRate = 1.0f;
    public Life playerLife;

    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        LifeTimer();
    }

    private void LifeTimer()
    {
        if (playerLife is not null && playerLife.currentLife > 0)
        {
            timer += Time.deltaTime;

            if (timer > hungryDecayRate)
            {
                playerLife.ChangeCurrentLife(-hungryDecayAmount);
                timer = 0f;
            }
        }
    }
}
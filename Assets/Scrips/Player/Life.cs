using UnityEngine;

public class Life : MonoBehaviour
{
    public float maxLife;
    public float currentLife;

    private void Start()
    {
        maxLife = maxLife != 0 ? maxLife : 100.0f;
        currentLife = maxLife;
    }

    public void ChangeCurrentLife(float damage)
    {
        currentLife += damage;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);

        if (currentLife <= 0)
        {
            // Llamar Die
        }
    }
}
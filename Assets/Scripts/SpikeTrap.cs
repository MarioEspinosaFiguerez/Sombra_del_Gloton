using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float damageSpike = 1.0f;

    [Header("Knockback")]
    public float knockbackXForce = 5.0f;
    public float knockbackYForce = 5.0f;
    public float knockbackDuration = 0.5f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Life playerLife))
        {
            playerLife.ChangeCurrentLife(-damageSpike);

            if (collider.TryGetComponent(out CharacterController playerCharacterController) && collider.TryGetComponent(out PlayerController playerScript))
            {
                StartCoroutine(ApplyKnockback(playerCharacterController, playerScript.goRight));
            }
        }
    }

    private IEnumerator ApplyKnockback(CharacterController characterController, bool facingRight)
    {
        float elapsedTimeCoroutineStarted = 0;
        Vector3 knockback = new Vector3(facingRight ? -knockbackXForce : knockbackXForce, knockbackYForce, 0);


        while (elapsedTimeCoroutineStarted < knockbackDuration)
        {
            // Progress = 0 -> Start of Coroutine / Progress = 1 -> End of Coroutine
            float progress = elapsedTimeCoroutineStarted / knockbackDuration;
            Vector3 currentKnockback = Vector3.Lerp(knockback, Vector3.zero, progress);

            characterController.Move(currentKnockback * Time.deltaTime);
            currentKnockback.y = Physics.gravity.y * Time.deltaTime;

            elapsedTimeCoroutineStarted += Time.deltaTime;
            yield return null;
        }

        // The Knockback stopped - To be sure the player stop getting force to continue moving
        characterController.Move(Vector3.zero);
    }
}
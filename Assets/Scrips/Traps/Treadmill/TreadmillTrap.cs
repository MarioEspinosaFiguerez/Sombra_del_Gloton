using System.Collections;
using UnityEngine;

public class TreadmillTrap : MonoBehaviour
{
    public enum DirectionTreadmill { Right, Left }

    [Header("Treadmill")]
    public DirectionTreadmill direction;
    public float treadmillMultiplier = 1.5f;
    public float treadmillDivider = 3.0f;
    public float treadmillForce = 3.0f;

    private float baseSpeed;
    private float targetSpeed;
    private bool isOnTreadmillCoroutine;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out PlayerController playerController) && !isOnTreadmillCoroutine)
        {
            baseSpeed = playerController.speed;
            StartCoroutine(ApplyTreadmillEffect(playerController));
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out PlayerController playerController))
        {
            RestoreSpeed(playerController);
        }
    }

    private IEnumerator ApplyTreadmillEffect(PlayerController controller)
    {
        isOnTreadmillCoroutine = true;

        CharacterController charController = controller.GetCharacterController();
        bool sameDirection = IsSameDirection(controller);
        controller.speed = targetSpeed = sameDirection ? controller.speed * treadmillMultiplier : controller.speed / treadmillDivider;

        // Treadmill direction push
        float pushDirection = direction is DirectionTreadmill.Right ? 1 : -1;

        while (isOnTreadmillCoroutine && charController.isGrounded)
        {
            // Checking if changes direction while on Treadmill, if so recalculate the speeds with baseSpeed instead of current speed
            sameDirection = IsSameDirection(controller);
            controller.speed = targetSpeed = sameDirection ? baseSpeed * treadmillMultiplier : baseSpeed / treadmillDivider;
            
            Vector3 push = new Vector3(pushDirection * treadmillForce * Time.deltaTime, 0, 0);
            charController.Move(push);

            yield return null;
        }
    }

    private void RestoreSpeed(PlayerController controller)
    {
        isOnTreadmillCoroutine = false;
        controller.speed = baseSpeed;
    }

    private bool IsSameDirection(PlayerController controller) => controller.goRight && direction is DirectionTreadmill.Right || !controller.goRight && direction is DirectionTreadmill.Left;
    
}
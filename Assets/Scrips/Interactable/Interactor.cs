using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
   [SerializeField] private Transform interactionPoint;
   [SerializeField] private float interactionPointRadius = 0.5f;
   [SerializeField] private LayerMask interactableMask;
    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;
    private IInteractable interactable;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
    void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if(numFound>0){
            interactable = colliders[0].GetComponent<IInteractable>();

            // if(interactable != null && Input.GetKeyDown(KeyCode.E)){
            //     interactable.Interact(this);
            // }
            if(interactable != null)
            {
                if(!interactionPromptUI.IsDisplayed){
                    interactionPromptUI.SetUp(interactable.InteractionPrompt);
                }
                if(Input.GetKeyDown(KeyCode.E)){
                    interactable.Interact(this);
                }
            }
        }
        else{
            if(interactable != null){
            interactable = null;
            }
            if(interactionPromptUI.IsDisplayed){
                interactionPromptUI.Close();
            }
            
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}

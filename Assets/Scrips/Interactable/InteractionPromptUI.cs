using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
  private Camera mainCam;
  [SerializeField] private GameObject uiPanel;
  [SerializeField] private TextMeshProUGUI promptText;
  public bool IsDisplayed = false;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
      mainCam = Camera.main;
      uiPanel.SetActive(false);
  }

  /// <summary>
  /// LateUpdate is called every frame, if the Behaviour is enabled.
  /// It is called after all Update functions have been called.
  /// </summary>
  void LateUpdate()
  {
      var rotation = mainCam.transform.rotation;
      transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
  }

  public void SetUp(string prompt)
  {
    promptText.text = prompt;
    uiPanel.SetActive(true);
    IsDisplayed = true;
  }

  public void Close(){
    uiPanel.SetActive(false);
    IsDisplayed = false;
  }

}

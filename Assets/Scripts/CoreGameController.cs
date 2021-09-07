using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoreGameController : MonoBehaviour
{
    public GameConfig activeGameConfig;
    [SerializeField] LightSaber lightSabreLeft;
    [SerializeField] LightSaber lightSabreRight;
    [SerializeField] TextMeshProUGUI TMPCollision;
    public LightSaber activeLightSaber;
    bool simulateSabers = false;
    bool simulateGhosts = false;

    private void Start()
    {
        // Both sabers get their initial rotations set by designer
        lightSabreLeft.Initialize(activeGameConfig.initialRotationSabreLeft);
        lightSabreRight.Initialize(activeGameConfig.initialRotationSabreRight);
        SwordsNotCollided();
    }

    public void SetGhostSabres()
    {
        lightSabreLeft.ghost.rb.MoveRotation(lightSabreLeft.rb.rotation);
        lightSabreRight.ghost.rb.MoveRotation(lightSabreRight.rb.rotation);
    }

    public void SimulateGhosts()
    {
        simulateGhosts = true;
    }

    public void SimulateOnClick(bool isSimulatedByPlayer)
    {
        lightSabreLeft.rb.isKinematic = false;
        lightSabreRight.rb.isKinematic = false;
        simulateSabers = true;
        activeLightSaber = null;
        SetGhostSabres();
        lightSabreLeft.SetSimulatedByPlayer(true);
        lightSabreRight.SetSimulatedByPlayer(true);
    }

    public void SetKinematics(bool isKinematic)
    {
        lightSabreLeft.rb.isKinematic = isKinematic;
        lightSabreRight.rb.isKinematic = isKinematic;
        lightSabreLeft.ghost.rb.isKinematic = isKinematic;
        lightSabreRight.ghost.rb.isKinematic = isKinematic;
    }

    public void SwordsCollided(bool isSimulatedByPlayer)
    {

        TMPCollision.text = "Swords Collided";
        
        simulateSabers = false;
        simulateGhosts = false;

        if (isSimulatedByPlayer)
        {
            Debug.LogError("COLLIDED BY PLAYER");
            StartCoroutine(SetSwordsToStartingState());
        }
        else
        {
            Debug.LogError("COLLIDED SLIDER");
            activeLightSaber = null;
        }
        SetKinematics(true);
    }

    public void SwordsNotCollided()
    {
        TMPCollision.text = "Swords Not Collided";
        simulateSabers = false;
        simulateGhosts = false;
        activeLightSaber = null;
        Debug.LogError("NO COLLISION");
    }

    IEnumerator SetSwordsToStartingState()
    {
        yield return new WaitForSeconds(1);
        lightSabreLeft.SetSabreToStartingState();
        lightSabreRight.SetSabreToStartingState();
    }

    private void FixedUpdate()
    {
        if (activeLightSaber != null)
        {
            if (simulateGhosts)
            {
                activeLightSaber.ghost.Simulate();
            }
        }
        else
        {
            if (simulateSabers)
            {
                lightSabreLeft.Simulate();
                lightSabreRight.Simulate();
                SetGhostSabres();
            }

        }
    }

}

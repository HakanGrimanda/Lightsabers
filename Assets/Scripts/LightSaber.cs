using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSaber : MonoBehaviour
{
    [SerializeField] Vector3 rotationSpeed;
    public LightSaber ghost;
    CoreGameController coreGame;
    Vector3 startingPosition;
    Vector3 startingRotation;
    [SerializeField] Slider slider;
    bool isSimulatedByPlayer;

    public Rigidbody rb;

    public void SetSabreToStartingState()
    {
        rb.isKinematic = true;
        rb.position = startingPosition;
        rb.rotation=Quaternion.Euler(startingRotation);
    }

    public void SetSimulatedByPlayer(bool isSimulatedByPlayer=false)
    {
        this.isSimulatedByPlayer = isSimulatedByPlayer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        coreGame.SwordsCollided(isSimulatedByPlayer);
    }

    public void Initialize(Vector3 initialRotation)
    {
        coreGame = GameObject.Find("GameController").GetComponent<CoreGameController>();

        rb = GetComponent<Rigidbody>();
        rb.MoveRotation(Quaternion.Euler(initialRotation));

        if (ghost != null)
        {
            ghost.Initialize(initialRotation);
        }

        startingPosition = rb.position;
        startingRotation = rb.rotation.eulerAngles;
    }

    public void Simulate()
    {
        Vector3 v=Vector3.zero;
        v.x += rotationSpeed.x * Time.fixedDeltaTime;
        
        rb.MoveRotation(rb.rotation * Quaternion.Euler(v));
    }

    public void SetRotationWithSlider()
    {
        Vector3 v = Vector3.zero;
        v.z = slider.value - transform.rotation.eulerAngles.z;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(v));
        coreGame.SetGhostSabres();
        coreGame.activeLightSaber = this;
        coreGame.SimulateGhosts();
        SetSimulatedByPlayer();
        coreGame.SetKinematics(false);
    }
}

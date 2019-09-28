using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLifecycle : MonoBehaviour
{
    public float Age = 0.0f;
    private EnergyState energyState;
    private MovementCapability movementCapability;
    private EatOnCollide eater;
    private EdibleState edible;
    private MovementBasedOnMass massMove;
    private FishAI ai;
    public bool Dead;
    public float AlivePecentage;
    public Color AliveColor;
    public Color DeadColor;
    public GameObject ColorTarget;
    private MeshRenderer mesh;

    public void Start()
    {
        Age = 0.0f;
        energyState = GetComponent<EnergyState>();
        movementCapability = GetComponent<MovementCapability>();
        eater = GetComponent<EatOnCollide>();
        edible = GetComponent<EdibleState>();
        massMove = GetComponent<MovementBasedOnMass>();
        ai = GetComponent<FishAI>();
        if (ColorTarget != null)
        {
            mesh = ColorTarget.GetComponent<MeshRenderer>();
        }
    }

    public void Update()
    {
        if (Dead)
        {
            UpdateStateDead();
        }
        else
        {
            UpdateStateAlive();
        }
    }

    private void UpdateStateDead()
    {
        energyState.Energy -= 2.0f * Time.deltaTime;
        movementCapability.MovementX = .0f;
        movementCapability.MovementY = .0f;
        var scale = transform.localScale;
        scale.y = -Mathf.Abs(scale.y);
        transform.localScale = scale;
        transform.localPosition += new Vector3(.0f, .3f * Time.deltaTime, .0f);
    }

    private void UpdateStateAlive()
    {
        energyState.Energy -= .5f * Time.deltaTime;
        Age += Time.deltaTime;
        AlivePecentage = energyState.Energy*.1f - Age * .02f;

        if (AlivePecentage <= .0f)
        {
            Dead = true;
            AlivePecentage = .0f;
            if (eater != null)
            {
                eater.FoodClass = "nothing";
                eater.enabled = false;
            }
            if (edible != null) edible.enabled = false;
            if (massMove != null) massMove.enabled = false;
            if (ai != null) ai.enabled = false;
        }

        if (mesh != null)
        {
            mesh.material.SetColor("_BaseColor", Color.Lerp(
                DeadColor, 
                AliveColor, Mathf.Clamp01(AlivePecentage)));
        }
    }
}

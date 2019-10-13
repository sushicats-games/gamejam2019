using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    private float commandTimeout = .0f;
    private MovementCapability movementCapability;
    private EdibleState edible;
    private EatOnCollide eater;
    private PlayerController playerController;
    private float commandX = .0f;
    private float commandY = .0f;
    private float nextCommandX = .0f;
    private float nextCommandY = .0f;
    private float commandAlertness = .0f;
    private float phase = .0f;
    private float previousXScale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        movementCapability = GetComponent<MovementCapability>();
        edible = GetComponent<EdibleState>();
        eater = GetComponent<EatOnCollide>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController != null && playerController.IsEnabled())
        {
            return; // disable AI
        }
        
        commandTimeout -= Time.deltaTime;
        if (commandTimeout <= .0)
        {
            NextCommand();
        }
        var nextAmount = Mathf.Clamp(commandAlertness * 2.0f, .9f, 2.0f) * Time.deltaTime;
        nextAmount = Mathf.Clamp01(nextAmount);
        commandX = Mathf.Lerp(commandX, nextCommandX, nextAmount);
        commandY = Mathf.Lerp(commandY, nextCommandY, nextAmount);

        phase += Time.deltaTime * (movementCapability.MovementX + movementCapability.MovementY) * 0.2f;
        Vector3 position = transform.localPosition;
        Vector2 moveDir = new Vector2(commandX - position.x, commandY - position.y);
        moveDir.Normalize();
        var chill = 1.0f - commandAlertness;
        if (chill > .0f)
        {
            moveDir.x += Mathf.Sin(phase) * chill * .6f;
            moveDir.y += Mathf.Cos(phase) * chill * .6f;
        }
        var speedModifier = Mathf.Lerp(1.0f, .7f, chill) * Mathf.Min(Mathf.Abs(position.x - commandX), Mathf.Abs(position.y - commandY),1.0f);
        moveDir.Normalize();

        transform.Translate(
            new Vector3(
                moveDir.x * movementCapability.MovementX, 
                moveDir.y * movementCapability.MovementY) 
             * Time.deltaTime * speedModifier);

        ModifyScaleBasedOnMovement(moveDir.x);
    }

    internal void Disable()
    {
        this.enabled = false;
    }

    private void NextCommand()
    {
        playerController = GetComponent<PlayerController>();

        EdibleState food = null;
        EatOnCollide predator = null;
        float foodDistance = 1.0e9f, predatorDistance = 1.0e9f;
        Vector3 position = transform.localPosition;
        foreach (var otherEdible in FindObjectsOfType<EdibleState>())
        {
            if (otherEdible.FoodClass == eater.FoodClass)
            {
                // found food :)
                var distance = Vector3.Distance(position, otherEdible.transform.localPosition);
                if (distance < foodDistance)
                {
                    food = otherEdible;
                    foodDistance = distance;
                }
            }
        }
        if (edible != null)
        {
            foreach (var otherEater in FindObjectsOfType<EatOnCollide>())
            {
                if (otherEater.FoodClass == edible.FoodClass)
                {
                    // found predator :(
                    var distance = Vector3.Distance(position, otherEater.transform.localPosition);
                    if (distance < predatorDistance)
                    {
                        predator = otherEater;
                        predatorDistance = distance;
                    }
                }
            }
        }
        if (predatorDistance < foodDistance)
        {
            nextCommandX = position.x + (position.x - predator.transform.localPosition.x) * 10.0f;
            nextCommandY = position.y + (position.y - predator.transform.localPosition.y) * 10.0f;
            commandTimeout = predatorDistance * .5f;
        }
        else if (food != null)
        {
            nextCommandX = position.x + (food.transform.localPosition.x - position.x) * 4.0f;
            nextCommandY = position.y + (food.transform.localPosition.y - position.y) * 4.0f;
            commandTimeout = foodDistance * .5f;
        }
        else
        {
            float size = 16.0f;
            nextCommandX = position.x + UnityEngine.Random.Range(-size, size);
            nextCommandY = position.y + UnityEngine.Random.Range(-size, size);
            commandTimeout = UnityEngine.Random.Range(1.0f, 2.0f);
        }
        commandAlertness = Mathf.Clamp01(3.0f-Mathf.Max(predatorDistance / 5.0f, foodDistance / 5.0f));
        commandTimeout *= UnityEngine.Random.Range(.1f, .6f);
        commandTimeout = Mathf.Clamp(commandTimeout, .1f, 4.0f);
    }

    private void ModifyScaleBasedOnMovement(float xMovement)
    {
        Vector3 scale = transform.localScale;

        if (xMovement < -0.5f)
            scale.x = -Mathf.Abs(scale.x);
        else if (xMovement > +0.5f)
            scale.x = Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(previousXScale);

        previousXScale = scale.x;

        transform.localScale = scale;
    }
}

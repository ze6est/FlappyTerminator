using System;
using UnityEngine;

[RequireComponent(typeof(TerminatorMover))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(TerminatorCollisionHandler))]
public class Terminator : MonoBehaviour
{
    private TerminatorMover _terminatorMover;
    private ScoreCounter _scoreCounter;
    private TerminatorCollisionHandler _handler;

    public event Action GameOver;

    private void Awake()
    {
        _terminatorMover = GetComponent<TerminatorMover>();
        _scoreCounter = GetComponent<ScoreCounter>();
        _handler = GetComponent<TerminatorCollisionHandler>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += OnCollisionDetected;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= OnCollisionDetected;
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _terminatorMover.Reset();
    }

    private void OnCollisionDetected(IInteractable interactable)
    {
        ProcessCollision(interactable);
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Death)
            GameOver?.Invoke();
    }
}
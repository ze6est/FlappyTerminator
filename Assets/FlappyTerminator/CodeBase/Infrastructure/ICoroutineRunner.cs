using System.Collections;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
using System.Collections;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.AssetManagement
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}
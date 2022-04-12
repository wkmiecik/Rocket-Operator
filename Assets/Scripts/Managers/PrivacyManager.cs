using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System.Collections.Generic;

public class PrivacyManager : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
    }
}
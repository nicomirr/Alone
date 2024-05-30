using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpotCount : MonoBehaviour
{
    public static HideSpotCount Instance;
    [SerializeField] bool _hasHideSpot;

    private void Awake()
    {
        Instance = this;
    }
    public bool HasHideSpot { get => _hasHideSpot; set => _hasHideSpot = value; }
}

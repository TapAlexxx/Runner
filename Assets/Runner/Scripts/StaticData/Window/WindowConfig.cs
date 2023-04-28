using System;
using Scripts.Infrastructure.Services.Window;
using UnityEngine;

namespace Scripts.StaticData.Window
{
  [Serializable]
  public class WindowConfig
  {
    public WindowTypeId WindowTypeId;
    public GameObject Prefab;
  }
}
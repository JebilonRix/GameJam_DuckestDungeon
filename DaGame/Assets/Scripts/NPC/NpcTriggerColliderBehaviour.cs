using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTriggerColliderBehaviour : MonoBehaviour
{
   [Header("Dependencies")] 
   [SerializeField] private NpcController _parentController;

   public NpcController ParentController => _parentController;


}

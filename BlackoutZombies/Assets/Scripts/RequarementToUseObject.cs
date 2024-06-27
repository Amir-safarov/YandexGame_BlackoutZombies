using UnityEngine;

public class RequarementToUseObject : MonoBehaviour
{
    [SerializeField, Range(0,400)] private int _requirementCount;

    public int GetRequirementDeadZombiesCount()
    { return _requirementCount;}
}

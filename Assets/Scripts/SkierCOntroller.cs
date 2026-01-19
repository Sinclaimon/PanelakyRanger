using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class SkierController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform skierMesh;
    [SerializeField] private BasicCamera basicCamera;

    [Header("Tuning")]
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float steerStrength = 6f;
    [SerializeField] private float brakeStrength = 12f;

    private Rigidbody rb;
    private Vector2 steerInput;
    private float brakeInput;

    private void Awake()
    {
          rb = GetComponent<Rigidbody>(); 
    }
}

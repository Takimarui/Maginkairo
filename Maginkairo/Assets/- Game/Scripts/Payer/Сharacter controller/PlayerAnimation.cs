using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveMagnitude = _playerController.MoveVector.magnitude;
        _animator.SetFloat("CustomMoveMagnitude", moveMagnitude);
    }
}

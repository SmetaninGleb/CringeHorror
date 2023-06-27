using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _minRotationSpeed = 0.001f;
    [SerializeField] private float _maxRotationSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _cameraRotMin = -90;
    [SerializeField] private float _cameraRotMax = 90;

    private CharacterController _controller;
    private Player _player;
    private bool _isMoving = false;

    public bool IsMoving => _isMoving;

    public void SetMouseSensitivity(float mouseSens)
    {
        _rotationSpeed = Mathf.Lerp(_minRotationSpeed, _maxRotationSpeed, mouseSens);
        PlayerPrefs.SetFloat("MouseSens", _rotationSpeed);
        PlayerPrefs.Save();
    }

    public float GetMouseSens()
    {
        return _rotationSpeed / (_maxRotationSpeed - _minRotationSpeed);
    }

    void Start()
    {
        _player = GetComponent<Player>();
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        if (PlayerPrefs.HasKey("MouseSens"))
        {
            _rotationSpeed = PlayerPrefs.GetFloat("MouseSens");
        }
        else
        {
            PlayerPrefs.SetFloat("MouseSens", _rotationSpeed);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        if (_player.IsAttacked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            return;
        }

        Vector3 move = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        if (move == Vector3.zero)
        {
            _isMoving = false;
        }
        else
        {
            _isMoving = true;
        }
        _controller.Move(move * _speed * Time.deltaTime);
        float rotationHor = _rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3(0f, rotationHor, 0f);
        transform.Rotate(rotation);

        float cameraRot = _cameraTransform.eulerAngles.x - _rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");
        cameraRot = Mathf.Clamp(cameraRot, _cameraRotMin, _cameraRotMax + 360);
        Vector3 cameraRotEuler = new Vector3(cameraRot, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _cameraTransform.eulerAngles = cameraRotEuler;
    }
}

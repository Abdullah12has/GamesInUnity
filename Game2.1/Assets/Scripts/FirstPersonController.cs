using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private float sensitivity = 100f;

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float jumpPower = 1f;

    private float rotation = 0f;

    private float gravity = -9.81f;

    [SerializeField]
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    private Transform detector;
    [SerializeField]
    private LayerMask groundLayer;

    //weapons
    private bool shootButtonPressed = false;
    private float forcePower = -10f;

    private Weapon weapon;
    [SerializeField] Transform firepoint;
    private List<Weapon> weapons = new List<Weapon>();
    private int selectedWeaponIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        weapons.Add(new CannonGun(firepoint));
        weapons.Add(ForceGun.PullGun(camera.transform));
        weapons.Add(ForceGun.PushGun(camera.transform));
        weapons.Add(new CreationTool(camera.transform));

        SelectWeapon(selectedWeaponIndex);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation of a player
        float x = Input.GetAxis(InputStrings.MouseX) * sensitivity * Time.deltaTime;
        float y = Input.GetAxis(InputStrings.MouseY) * sensitivity * Time.deltaTime;

        controller.transform.Rotate(Vector3.up * x);

        //It doesn't work becouse you can rotate all the way around
        //camera.transform.Rotate(Vector3.left * y);

        rotation -= y;
        rotation = Mathf.Clamp(rotation, -88f, 90f);

        camera.transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);

        //Movement
        float moveX = Input.GetAxis(InputStrings.HorizontalAxis);
        float moveZ = Input.GetAxis(InputStrings.VerticalAxis);

        Vector3 moveVector = controller.transform.forward * moveZ;
        moveVector += controller.transform.right * moveX;

        moveVector *= speed * Time.deltaTime;

        //gravity
        velocity.y += gravity * Time.deltaTime * Time.deltaTime;

        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = 0;
        }

        //jump
        if (Input.GetButtonDown(InputStrings.JumpButton) && IsGrounded())
        {
            velocity.y = jumpPower;
        }

        controller.Move(moveVector + velocity);

        //shooting
        if (Input.GetButtonDown(InputStrings.ShootButton))
        {
            shootButtonPressed = true;
        }

        if (Input.GetButtonUp(InputStrings.ShootButton))
        {
            shootButtonPressed = false;
        }


        //switch weapons

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SwitchWeapons();
        }
    }

    private void FixedUpdate()
    {
        if (shootButtonPressed)
        {
            Shoot();
            //shootButtonPressed = false;
        }
    }


    private void SwitchWeapons()
    {
        selectedWeaponIndex = (selectedWeaponIndex + 1) % weapons.Count;

        //if (selectedWeaponIndex < weapons.Count - 1)
        //{
        //    selectedWeaponIndex++;
        //}
        //else
        //{
        //    selectedWeaponIndex = 0;
        //}

        SelectWeapon(selectedWeaponIndex);
    }

    private void SelectWeapon(int index)
    {
        weapon = weapons[index];
        UIManager.sharedInstance.SetWeaponLabel(weapon.Name);
    }

    
    private void Shoot()
    {
        weapon.Shoot();
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(detector.position, 0.3f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(detector.position, 0.3f);
    }
}

public struct InputStrings
{
    public static string MouseX = "Mouse X";
    public static string MouseY = "Mouse Y";
    public static string HorizontalAxis = "Horizontal";
    public static string VerticalAxis = "Vertical";
    public static string JumpButton = "Jump";
    public static string ShootButton = "Fire1";
}

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeController : MonoBehaviour
{
    public float RotationTime = 0.5f;
    public float minSwipeDistY = 5;
    public float minSwipeDistX = 5;

    private CubeLogic _cubeLogic;
    private Transform _dados;
    private List<Transform> _axis = new List<Transform>();
    private List<Transform> _cubes = new List<Transform>();

    private RaycastHit _hit;
    private Transform _selectedAxis;
    private Vector2 _swipeStartPosition = Vector2.zero;
    private bool _rotationLocked = false;
    private bool _solve = false;

    Transform SelectedAxis
    {
        get
        {
            return _selectedAxis;
        }
        set
        {
            if (_selectedAxis == null)
            {
                _selectedAxis = value;
                _selectedAxis.MaterialColorFade(1);
            }
            else if (_selectedAxis != value)
            {
                _selectedAxis.MaterialColorFade(0);
                _selectedAxis = value;
                _selectedAxis.MaterialColorFade(1);
            }
            else
            {
                _selectedAxis.MaterialColorFade(0);
                _selectedAxis = null;
            }
        }
    }

    void Start()
    {
        _dados = transform.Find("Dados");
        _axis = transform.Find("Axis").GetTransformChilders();
        _cubes = _dados.GetTransformChilders();
        _cubeLogic = new CubeLogic();
        Randomize();
    }

    void Update()
    {
        if (Input.touchCount > 0 && !_solve)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Ended:

                    Vector2 currentSwipe = Input.GetTouch(0).position - _swipeStartPosition;
                    currentSwipe.Normalize();

                    if (!_rotationLocked && SelectedAxis != null)
                    {
                        // UP SWIPE
                        if (currentSwipe.y > 0 && (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f))
                        {
                            if (SelectedAxis.name == "TopAxis" || SelectedAxis.name == "BottomAxis")
                                StartCoroutine(RotateAxisCO(SelectedAxis, Vector3.forward * -180));
                            else
                                StartCoroutine(RotateAxisCO(SelectedAxis, Vector3.right * -180));

                            _cubeLogic.CheckRotation(SelectedAxis.name);

                        }
                        // DOWN SWIPE
                        else if (currentSwipe.y < 0 && (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f))
                        {
                            if (SelectedAxis.name == "TopAxis" || SelectedAxis.name == "BottomAxis")
                                StartCoroutine(RotateAxisCO(SelectedAxis, Vector3.forward * 180));
                            else
                                StartCoroutine(RotateAxisCO(SelectedAxis, Vector3.right * 180));

                            _cubeLogic.CheckRotation(SelectedAxis.name);
                        }
                        // LEFT SWIPE
                        else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                        {
                            StartCoroutine(RotateAxisCO(SelectedAxis, Vector3.up * 90));
                            _cubeLogic.CheckRotation(SelectedAxis.name, true, true);
                        }
                        // RIGHT SWIPE
                        else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                        {
                            StartCoroutine(RotateAxisCO(SelectedAxis, Vector3.up * -90));
                            _cubeLogic.CheckRotation(SelectedAxis.name, true);
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    if (SelectedAxis == null && !_rotationLocked)
                    {
                        transform.Rotate(Input.GetTouch(0).deltaPosition.y * 0.3f, Input.GetTouch(0).deltaPosition.x * 0.3f, 0, Space.World);

                    }
                    break;

                case TouchPhase.Began:
                    _swipeStartPosition = Input.GetTouch(0).position;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out _hit, Mathf.Infinity))
                        SelectedAxis = _hit.transform;
                    break;
            }
        }
    }

    IEnumerator RotateAxisCO(Transform selectedAxis, Vector3 angle)
    {
        _rotationLocked = true;

        Quaternion currentRotation = selectedAxis.rotation;
        Bounds axisBound = (selectedAxis.GetComponent(typeof(Collider)) as Collider).bounds;
        _cubes.ForEach(x => x.parent = axisBound.Contains(x.position) ? selectedAxis : _dados);
        var rotation = selectedAxis.rotation * Quaternion.Euler(angle);
        float t = 0;
        while (t < RotationTime)
        {
            selectedAxis.rotation = Quaternion.Lerp(currentRotation, rotation, t / RotationTime);
            t += Time.deltaTime;
            yield return null;
        }
        selectedAxis.rotation = rotation;
        _cubes.ForEach(x => x.parent = _dados);
        selectedAxis.rotation = currentRotation;
        _rotationLocked = false;
        _solve = _cubeLogic.CheckSolve();
    }

    void RotateAxis(Transform selectedAxis, Vector3 angle)
    {
        Quaternion currentRotation = selectedAxis.rotation;
        Bounds axisBound = (selectedAxis.GetComponent(typeof(Collider)) as Collider).bounds;
        _cubes.ForEach(x => x.parent = axisBound.Contains(x.position) ? selectedAxis : _dados);
        selectedAxis.rotation = selectedAxis.rotation * Quaternion.Euler(angle);
        _cubes.ForEach(x => x.parent = _dados);
        selectedAxis.rotation = currentRotation;
    }

    void Randomize()
    {
        for (int i = 0; i < 1000; i++)
        {
            Transform selectedAxis = _axis[Random.Range(0, 4)];
            Utils.Rotation rotation = (Utils.Rotation)Random.Range(0, 4);

            switch (rotation)
            {
                case Utils.Rotation.Up:
                    Debug.Log(selectedAxis.name + " UP");
                    if (selectedAxis.name == "TopAxis" || selectedAxis.name == "BottomAxis")
                        RotateAxis(selectedAxis, Vector3.forward * -180);
                    else
                        RotateAxis(selectedAxis, Vector3.right * -180);

                    _cubeLogic.CheckRotation(selectedAxis.name);
                    break;
                case Utils.Rotation.Down:
                    Debug.Log(selectedAxis.name + " DOWN");
                    if (selectedAxis.name == "TopAxis" || selectedAxis.name == "BottomAxis")
                        RotateAxis(selectedAxis, Vector3.forward * 180);
                    else
                        RotateAxis(selectedAxis, Vector3.right * 180);

                    _cubeLogic.CheckRotation(selectedAxis.name);
                    break;

                case Utils.Rotation.Left:
                    Debug.Log(selectedAxis.name + " RIGHT");
                    RotateAxis(selectedAxis, Vector3.up * 90);
                    _cubeLogic.CheckRotation(selectedAxis.name, true, true);
                    break;

                case Utils.Rotation.Right:
                    Debug.Log(selectedAxis.name + " LEFT");
                    RotateAxis(selectedAxis, Vector3.up * -90);
                    _cubeLogic.CheckRotation(selectedAxis.name, true);
                    break;
            }
        }
        _cubeLogic.CubeLogicDebug();        
    }
}
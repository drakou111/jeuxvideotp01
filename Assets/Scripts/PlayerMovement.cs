using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerMovement : MonoBehaviour
{
  private float hInput;
  private float vInput;
  [SerializeField] float speed;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    vInput = Input.GetAxis("Vertical");
    hInput = Input.GetAxis("Horizontal");

    transform.Translate(Vector3.forward * Time.deltaTime * speed * vInput);
    transform.Translate(Vector3.right * Time.deltaTime * speed * hInput);

  }
}

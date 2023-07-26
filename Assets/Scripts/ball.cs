using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ball : MonoBehaviour
{
    public int coin;
    public TMP_Text coinn;
    public int score;
    public TMP_Text scoree;
    public GameObject finish_panel;
    public static ball instance { get; set; }

    // Start is called before the first frame update


    public float jumpForce = 5f;
    private bool isJumping = false;

    private Rigidbody rb;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        coinn.text = coin.ToString();
        scoree.text = score.ToString();
        if (Input.GetMouseButtonDown(0) && !isJumping)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    public void OnJumpButtonClicked()
    {
        if (!isJumping)
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            finish_panel.SetActive(true);
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "score")
        {
            score++;
        }

    }
    public void retry()
    {
        SceneManager.LoadScene("SampleScene");
    }
        
    
}

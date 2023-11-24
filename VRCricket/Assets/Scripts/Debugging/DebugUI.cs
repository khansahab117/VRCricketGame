using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugUI : MonoBehaviour
{


    /// <summary>
    /// 
    /// GAME OBJECTS 
    /// 
    /// </summary>/
    public BatBallCollision batBallCollision;
    public CannonManagerDebug cannonManager;
    public GameObject cricketBall;
    public GameObject cricketBat;





    /// <summary>
    /// 
    /// CONTROLLER VIBRATION AND BALL VELOCITY 
    /// 
    /// </summary>/
    public Slider intensitySlider;
    public Slider durationSlider;
    public Slider velocityMultiplierSlider;

    public TextMeshProUGUI intensityLabel;
    public TextMeshProUGUI durationLabel;
    public TextMeshProUGUI velocityMultiplierLabel;

    public TMP_InputField TI_intensitySlider;
    public TMP_InputField TI_durationSlider;
    public TMP_InputField TI_velocityMultiplierSlider;

    /// <summary>
    /// 
    /// CRICKET BALL RB PROPERTIES  
    /// 
    /// </summary>/
    public Slider massSlider_Ball;
    public Slider linearDragSlider_Ball;
    public Slider angularDragSlider_Ball;

    public TextMeshProUGUI massLabel_Ball;
    public TextMeshProUGUI linearDragLabel_Ball;
    public TextMeshProUGUI angularDragLabel_Ball;

    private Rigidbody rbBall; // Cached reference to the Rigidbody of cricketBall

    public TMP_InputField TI_massSlider_Ball;
    public TMP_InputField TI_linearDragSlider_Ball;
    public TMP_InputField TI_angularDragSlider_Ball;

    /// <summary>
    /// 
    /// CRICKET BAT RB PROPERTIES  
    /// 
    /// </summary>/
    public Slider massSlider_Bat;
    public Slider linearDragSlider_Bat;
    public Slider angularDragSlider_Bat;

    public TextMeshProUGUI massLabel_Bat;
    public TextMeshProUGUI linearDragLabel_Bat;
    public TextMeshProUGUI angularDragLabel_Bat;

    private Rigidbody rbBat; // Cached reference to the Rigidbody of cricketBat

    public TMP_InputField TI_massSlider_Bat;
    public TMP_InputField TI_linearDragSlider_Bat;
    public TMP_InputField TI_angularDragSlider_Bat;

    void Start()
    {
        // Set initial values on sliders
        intensitySlider.value = batBallCollision.vibrationIntensity;
        durationSlider.value = batBallCollision.vibrationDuration;
        velocityMultiplierSlider.value = cannonManager.velocityMultiplier;

        // Cache the reference to the Rigidbody of cricketBall
        rbBall = cricketBall.GetComponent<Rigidbody>();
        if (rbBall != null)
        {
            // Set initial values for Rigidbody sliders of cricketBall
            massSlider_Ball.value = rbBall.mass;
            linearDragSlider_Ball.value = rbBall.drag;
            angularDragSlider_Ball.value = rbBall.angularDrag;

        }

        // Cache the reference to the Rigidbody of cricketBat
        rbBat = cricketBat.GetComponent<Rigidbody>();
        if (rbBat != null)
        {
            // Set initial values for Rigidbody sliders of cricketBat
            massSlider_Bat.value = rbBat.mass;
            linearDragSlider_Bat.value = rbBat.drag;
            angularDragSlider_Bat.value = rbBat.angularDrag;
        }

        // Add input field listeners
        TI_intensitySlider.onValueChanged.AddListener(delegate { UpdateSliderValue(TI_intensitySlider.text, intensitySlider); });
        TI_durationSlider.onValueChanged.AddListener(delegate { UpdateSliderValue(TI_durationSlider.text, durationSlider); });
        TI_velocityMultiplierSlider.onValueChanged.AddListener(delegate { UpdateSliderValue(TI_velocityMultiplierSlider.text, velocityMultiplierSlider); });

        TI_massSlider_Ball.onValueChanged.AddListener(delegate { UpdateSliderValue(TI_massSlider_Ball.text, massSlider_Ball); });
        TI_linearDragSlider_Ball.onValueChanged.AddListener(delegate { UpdateSliderValue(TI_linearDragSlider_Ball.text, linearDragSlider_Ball); });
        TI_angularDragSlider_Ball.onValueChanged.AddListener(delegate { UpdateSliderValue(TI_angularDragSlider_Ball.text, angularDragSlider_Ball); });

        TI_massSlider_Bat.onValueChanged.AddListener(delegate { UpdateSliderValue(TI_massSlider_Bat.text, massSlider_Bat); });
        TI_linearDragSlider_Bat.onValueChanged.AddListener(delegate { UpdateSliderValue(TI_linearDragSlider_Bat.text, linearDragSlider_Bat); });
        TI_angularDragSlider_Bat.onValueChanged.AddListener(delegate { UpdateSliderValue(TI_angularDragSlider_Bat.text, angularDragSlider_Bat); });

        // Update labels with initial values
        UpdateLabels();
    }

    void Update()
    {
        // Update values in the referenced script
        batBallCollision.vibrationIntensity = intensitySlider.value;
        batBallCollision.vibrationDuration = durationSlider.value;
        cannonManager.velocityMultiplier = velocityMultiplierSlider.value;

        // Update values for Rigidbody sliders of cricketBall
        if (rbBall != null)
        {
            rbBall.mass = massSlider_Ball.value;
            rbBall.drag = linearDragSlider_Ball.value;
            rbBall.angularDrag = angularDragSlider_Ball.value;
        }

        // Update values for Rigidbody sliders of cricketBat
        if (rbBat != null)
        {
            rbBat.mass = massSlider_Bat.value;
            rbBat.drag = linearDragSlider_Bat.value;
            rbBat.angularDrag = angularDragSlider_Bat.value;
        }

        // Update labels with current values
        UpdateLabels();
    }

    void UpdateLabels()
    {
        // Update TextMeshPro labels with slider values
        intensityLabel.text = $"Intensity: {intensitySlider.value:F2}";
        durationLabel.text = $"Duration: {durationSlider.value:F2}";
        velocityMultiplierLabel.text = $"Velocity Multiplier: {velocityMultiplierSlider.value:F2}";

        // Update Rigidbody property labels of cricketBall
        if (rbBall != null)
        {
            massLabel_Ball.text = $"Mass: {rbBall.mass:F2}";
            linearDragLabel_Ball.text = $"Linear Drag: {rbBall.drag:F2}";
            angularDragLabel_Ball.text = $"Angular Drag: {rbBall.angularDrag:F2}";
        }

        // Update Rigidbody property labels of cricketBat
        if (rbBat != null)
        {
            massLabel_Bat.text = $"Mass: {rbBat.mass:F2}";
            linearDragLabel_Bat.text = $"Linear Drag: {rbBat.drag:F2}";
            angularDragLabel_Bat.text = $"Angular Drag: {rbBat.angularDrag:F2}";
        }
    }

    private void UpdateSliderValue(string inputText, Slider sliderToUpdate)
    {
        float sliderValue;

        // Parse the input text into a floating-point number
        if (float.TryParse(inputText, out sliderValue))
        {
            // Update the slider value
            sliderToUpdate.value = sliderValue;
        }
    }
}

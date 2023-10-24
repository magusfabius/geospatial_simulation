using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class HumanoidAgent : Agent
{
    public Transform Target;
    private Rigidbody rBody;
    private const float Speed = 5f;  // Adjust as needed
    private Humanoid humanoid;

    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        humanoid = new Humanoid("John", 30, "6 feet", "70 kg", "Male", "Caucasian", "Engineer");
    }

    public override void OnEpisodeBegin()
    {
        // Reset agent and target positions
        this.transform.position = new Vector3(0, 1, 0);
        Target.position = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Target.position);
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rBody.velocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Convert actions to movement
        Vector3 movement = new Vector3(actions.ContinuousActions[0], 0, actions.ContinuousActions[1]);
        rBody.AddForce(movement * Speed);

        // Rewards and penalties
        if (Vector3.Distance(transform.position, Target.position) < 1.5f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        else if (transform.position.y < 0 || Vector3.Distance(transform.position, Target.position) > 25f)
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        actionsOut.ContinuousActions.Array[0] = Input.GetAxis("Horizontal");
        actionsOut.ContinuousActions.Array[1] = Input.GetAxis("Vertical");
    }

    public class Humanoid
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Gender { get; set; }
        public string Ethnicity { get; set; }
        public string Occupation { get; set; }
        public string Meaning { get; set; }
        public string CurrentThought { get; set; }

        public Humanoid(string name, int age, string height, string weight, string gender, string ethnicity, string occupation, string meaning = "", string currentThought = "")
        {
            Name = name;
            Age = age;
            Height = height;
            Weight = weight;
            Gender = gender;
            Ethnicity = ethnicity;
            Occupation = occupation;
            Meaning = meaning;
            CurrentThought = currentThought;
        }

        public string Q(string query)
        {
            switch (query)
            {
                case "name": return Name;
                case "age": return Age.ToString();
                case "height": return Height;
                case "weight": return Weight;
                case "gender": return Gender;
                case "ethnicity": return Ethnicity;
                case "occupation": return Occupation;
                case "meaning": return Meaning;
                case "current_thought": return CurrentThought;
                default: return "I don't understand that question.";
            }
        }

        public void SetCurrentThought(string thought)
        {
            CurrentThought = thought;
        }
    }
}

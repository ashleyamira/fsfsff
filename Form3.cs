using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project
{
    public partial class Form3 : Form
    {
        Random random = new Random();  // To generate random sabotage actions
        Button btnSabotage;            // The sabotage button
        bool sabotageAvailable = true; // Flag to check if sabotage is available
        Timer sabotageCooldownTimer;   // Timer for cooldown period

        // Example list of opponent ships (just for simulation)
        List<Ship> opponentShips;

        public Form3()
        {
            InitializeComponent();

            // Initialize the opponent's ships
            opponentShips = new List<Ship>
        {
            new Ship("Ship1"),
            new Ship("Ship2"),
            new Ship("Ship3")
        };

            // Create the Sabotage button
            btnSabotage = new Button();
            btnSabotage.Text = "Sabotage";
            btnSabotage.Size = new Size(100, 40);
            btnSabotage.Location = new Point(350, 350); // Adjust location as needed
            btnSabotage.Click += BtnSabotage_Click;

            // Add the button to the form
            this.Controls.Add(btnSabotage);

            // Set up a timer for the cooldown after using sabotage
            sabotageCooldownTimer = new Timer();
            sabotageCooldownTimer.Interval = 10000; // 10 seconds cooldown
            sabotageCooldownTimer.Tick += SabotageCooldownTimer_Tick;
        }

        // Event handler when the Sabotage button is clicked
        private void BtnSabotage_Click(object sender, EventArgs e)
        {
            if (sabotageAvailable)
            {
                // Trigger a random sabotage action
                ActivateSabotage();
                sabotageAvailable = false;  // Disable sabotage until cooldown is over
                sabotageCooldownTimer.Start(); // Start the cooldown timer
            }
            else
            {
                MessageBox.Show("Sabotage is on cooldown! Please wait.");
            }
        }

        // Timer tick event that re-enables the sabotage button after the cooldown
        private void SabotageCooldownTimer_Tick(object sender, EventArgs e)
        {
            sabotageAvailable = true;  // Enable the sabotage button again
            sabotageCooldownTimer.Stop();  // Stop the timer
        }

        // This method randomly selects a sabotage action to perform
        private void ActivateSabotage()
        {
            int sabotageAction = random.Next(1, 4); // Random number between 1 and 3

            switch (sabotageAction)
            {
                case 1:
                    DisableShip();
                    break;
                case 2:
                    SwapShips();
                    break;
                case 3:
                    ForceMiss();
                    break;
            }
        }

        // Example: Disables an opponent's ship temporarily
        private void DisableShip()
        {
            MessageBox.Show("You disabled an opponent's ship for one turn!");

            // Simulate disabling a random ship
            int shipIndex = random.Next(opponentShips.Count);
            opponentShips[shipIndex].IsDisabled = true;

            // Optionally, you can update the UI to show the disabled ship
            // For now, we just show the ship name in the message
            MessageBox.Show(opponentShips[shipIndex].Name + " is disabled for one turn.");
        }

        // Example: Swap two of the opponent's ships
        private void SwapShips()
        {
            MessageBox.Show("You swapped two of your opponent's ships!");

            // Simulate swapping two ships randomly
            int shipIndex1 = random.Next(opponentShips.Count);
            int shipIndex2 = random.Next(opponentShips.Count);

            // Swap the ships
            Ship temp = opponentShips[shipIndex1];
            opponentShips[shipIndex1] = opponentShips[shipIndex2];
            opponentShips[shipIndex2] = temp;

            // Optionally, update the UI to reflect the swapped ships
            MessageBox.Show("Swapped " + opponentShips[shipIndex1].Name + " with " + opponentShips[shipIndex2].Name);
        }

        // Example: Force the opponent to miss their next shot
        private void ForceMiss()
        {
            MessageBox.Show("You forced your opponent's next shot to miss!");

            // Logic for forcing a miss would go here.
            // For example, you might set a flag to indicate the next shot will miss.
            // For now, we're just simulating with a message.
        }

        // Ship class for representing the opponent's ships
        public class Ship
        {
            public string Name { get; set; }
            public bool IsDisabled { get; set; }

            public Ship(string name)
            {
                Name = name;
                IsDisabled = false;
            }

        }
    }
}

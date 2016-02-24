using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_Control_Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Elevator elwood = new Elevator();
            Console.Write("Which Floor? : ");
            string x = Console.ReadLine();
            int floor;
            bool isValid = int.TryParse(x, out floor);
            while (floor != -1)
            {
                elwood.gotoFloor(floor);
                Console.WriteLine(elwood);
                Console.WriteLine("press enter to goto next floor...");
                Console.ReadLine();
                elwood.arrivedAtFloor();
                Console.WriteLine(elwood);
                Console.Write("\nWhich Floor? (enter -1 to quit): ");
                x = Console.ReadLine();
                isValid = int.TryParse(x, out floor);
            }
        }

        public class Elevator
        {
            public string Name { get; set; }
            public int CurrentFloor { get; set; }
            private Nullable<int>  NextFloor, LastFloor;
            private int DoorState;
            private bool DoorClear, InService;

            //default contructor
            public Elevator ()
            {
                Name = "Elwood the Elevator";
                InService = true;
            }

            //constructor public
            public Elevator(string elevatorName, int currentFloor)
            {
                this.Name = elevatorName;
                this.CurrentFloor = currentFloor;
                this.NextFloor = null;
                this.LastFloor = null;
                this.DoorState = 0;
                this.DoorClear = false;
                this.InService = true;
            }

            //contructor
            private Elevator(string elevatorName, int currentFloor, Nullable<int> nextFloor,
                Nullable<int> lastFloor, int doorState, bool doorClear, bool inService)
            {
                this.Name = elevatorName;
                this.CurrentFloor = currentFloor;
                this.NextFloor = nextFloor;
                this.LastFloor = lastFloor;
                this.DoorState = doorState;
                this.DoorClear = doorClear;
                this.InService = inService;
            }

            // setting the door state
            private void setDoorState(string doorState)
            {
                switch (doorState)
                {
                    case "closed":
                        this.DoorState = 0;
                        break;
                    case "closing":
                        DoorState = 3;
                        break;
                    case "opened":
                        DoorState = 2;
                        break;
                    case "opening":
                        DoorState = 1;
                        break;
                    default:
                        break;
                }
            }

            //toString()
            public override string ToString()
            {
                string s = "";
                s = "Name: " + Name + "\nInService: " + InService + "\nCurrentFloor: " + CurrentFloor
                     + "\nNextFloor: " + NextFloor + "\nLastFloor: " + LastFloor
                     + "\nDoorState: " + this.showDoorState() + "\nDoorClear: " + DoorClear;
                return s;
            }

            //display door state
            public string showDoorState()
            {
                string s = "";
                if (this.DoorState == 0)
                { s = "DoorClosed"; }
                if (this.DoorState == 1)
                { s = "DoorOpening"; }
                if (this.DoorState == 2)
                { s = "DoorOpen"; }
                if (this.DoorState == 3)
                { s = "DoorClosing"; }
                return s;
            }

            public void gotoFloor(int f)
            {
                this.NextFloor = f;
            }

            public void arrivedAtFloor()
            {
                this.LastFloor = this.CurrentFloor;
                this.CurrentFloor = (int)this.NextFloor;
                this.NextFloor = null;
            }
        }

        class Buttons { }
        class Display { }
        class Scheduler { }
    }
}

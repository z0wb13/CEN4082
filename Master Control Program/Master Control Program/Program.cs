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
            Console.WriteLine("hello world");
            Elevator elwood = new Elevator();
            Console.WriteLine(elwood);
            Console.Write("Which FLoor:");
            string x = Console.ReadLine();
            int floor;
            bool isValid = int.TryParse(x, out floor);
            elwood.gotoFloor(floor);
            Console.WriteLine(elwood);
            x = Console.ReadLine();
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

            //constructor
            public Elevator(string elevatorName, int currentFloor,  string doorState, bool doorClear)
            {
                this.Name = elevatorName;
                this.CurrentFloor = currentFloor;
                this.NextFloor = null;
                this.LastFloor = null;
                this.DoorState = 0;

                switch (doorState)
                {
                    case "":
                        break;
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
                }
                this.DoorClear = doorClear;
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



        }
        class Buttons { }
        class Display { }
        class Scheduler { }







    }

    


}

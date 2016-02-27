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
            //elwood.openElevator();
            Console.WriteLine(elwood);
            elwood.setDoorState("opened");
            Console.Write("\nWhich Floor? : ");
            elwood.setDoorState("closed");
            string x = Console.ReadLine();
            int floor;
            bool isValid = int.TryParse(x, out floor);
            while (floor != -1)
            {
                elwood.gotoFloor(floor);
                Console.WriteLine(elwood.getElevatorState());
                //Console.WriteLine("\npress enter to goto next floor...");
                Console.ReadLine(); 
                Console.WriteLine(elwood);
                Console.Write("\nWhich Floor? (enter -1 to quit): ");
                x = Console.ReadLine();
                elwood.setDoorClear(true);
                elwood.closeElevator(); 
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
            private List<Passenger> Passengers = new List<Passenger>(); 
            
            //default contructor
            public Elevator ()
            {
                Name = "Elwood the Elevator";
                InService = true;
                this.NextFloor = null;
                this.LastFloor = null;
                this.DoorState = 0;
                this.DoorClear = false;
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

            public string getName()
            {
                return this.Name;
            }

            public void setName(string name)
            {
                this.Name = name;
            }

            public int getCurrentFloor()
            {
                return this.CurrentFloor;
            }

            public void setCurrentFloor(int currentFloor)
            {
                this.CurrentFloor = currentFloor;
            }

            public int getNextFloor()
            {
                return (int)this.NextFloor;
            }

            public void setNextFloor(int nextFloor)
            {
                this.NextFloor = nextFloor;
            }

            public int getLastFloor()
            {
                return (int)this.LastFloor;
            }

            public void setLastFloor(int lastFloor)
            {
                this.LastFloor = lastFloor;
            }

            public bool getDoorClear()
            {
                return this.DoorClear;
            }

            public void setDoorClear(bool doorClear)
            {
                this.DoorClear = doorClear;
            }

            public bool getInService()
            {
                return this.InService;
            }

            public int getDoorState()
            {
                return this.DoorState;
            }

            public void setDoorState(int doorState)
            {
                if (doorState >= 0 && doorState <= 4)
                {
                    this.DoorState = doorState;
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

            // setting the door state 
            public void setDoorState(string doorState)
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

            public async void gotoFloor(int nextFloor)
            {
                if (getDoorState() == 0)
                {
                    this.NextFloor = nextFloor;
                    await Task.Delay(ETA());
                    arrivedAtFloor();
                    openElevator();
                }
                
            }

            public void arrivedAtFloor()
            {
                Console.WriteLine("you have arrived");
                this.LastFloor = this.CurrentFloor;
                this.CurrentFloor = (int)this.NextFloor;
                this.NextFloor = null;
            }

            public int ETA()
            {
                int t = 0;
                t = (int)NextFloor - CurrentFloor;
                t *= 7000;
                if (t < 0)
                {
                    t *= -1;
                }
                return t;
            }

            //this tells you if the elevator is headed up or down. 
            public string getElevatorState()
            {
                string s = "Arrived_At_Floor";
                if (NextFloor != null)
                {
                    if ((CurrentFloor - (int)NextFloor) < 0)
                    { s = "Going_UP"; }
                    if ((CurrentFloor - (int)NextFloor) > 0)
                    { s = "Going_DOWN"; }
                    if ((CurrentFloor - (int)NextFloor) == 0)
                    { s = "you're already there..."; }
                }
                return s;
            }


            private async void openElevator()
            {
                if (DoorState == 0 || DoorClear == true)
                {
                    this.DoorState = 1;
                    Console.WriteLine("the door is opening. ");
                    await Task.Delay(3000);
                    this.DoorState = 2;
                    Console.WriteLine("the door is opened. ");
                }
            }

            public async void closeElevator()
            {
                if (DoorState == 2 && DoorClear == true)
                {
                    this.DoorState = 3;
                    Console.WriteLine("the door is closing. ");
                    await Task.Delay(3000);
                    this.DoorState = 0;
                    Console.WriteLine("the door is closed. ");
                }
            }
        }

        class Passenger
        {
            private string Name;
            private int CurrentFloor, NextFloor;
            private int TimePickUp, TimeDropOff, TimeWait;

            public Passenger(string name, int currentFloor, int nextFloor)
            {
                this.Name = name;
                this.CurrentFloor = currentFloor;
                this.NextFloor = nextFloor;
            }

            //methods get/set
            public string getName()
            {
                return this.Name;
            }
            public int getCurrentFloor()
            {
                return this.CurrentFloor;
            }
            public int getNextFloor()
            {
                return this.NextFloor;
            }


        }

        class Scheduler 
        {
            List<Elevator> Elevator = new List<Elevator>();
            List<Passenger> Passengers = new List<Passenger>();
            List<Passenger> Requests = new List<Passenger>();





        }


        class Buttons { }
        class Display { }
        
    }
}

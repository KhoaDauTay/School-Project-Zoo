using System;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class that represents the move behavior of climbing and falling.
    /// </summary>
    [Serializable]
    public class ClimbBehavior : IMoveBehavior
    {
        /// <summary>
        /// The behavior's random.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The process stage that the animal is currently in.
        /// </summary>
        private ClimbProcess process;

        /// <summary>
        /// The maximum height that an animal can reach while climbing.
        /// </summary>
        private int maxHeight;

        /// <summary>
        /// Moves the animal.
        /// </summary>
        /// <param name="animal">The animal to move.</param>
        public void Move(Animal animal)
        {
            switch (this.process)
            {
                case ClimbProcess.Climbing:
                    // If the animal is climbing, ensure the vertical direction is set to up
                    animal.YDirection = VerticalDirection.Up;

                    // Move the animal vertically
                    MoveHelper.MoveVertically(animal, animal.MoveDistance);

                    // If the animal has hit or will hit the top of the cage
                    if (animal.YPosition - animal.MoveDistance <= this.maxHeight)
                    {
                        // Change the animal's vertical direction to down
                        animal.YDirection = VerticalDirection.Down;

                        // Switch the way the animal is facing
                        animal.XDirection = animal.XDirection == HorizontalDirection.Left ? HorizontalDirection.Right : HorizontalDirection.Left;

                        // And switch the animal to the next process
                        this.NextProcess(animal);
                    }

                    break;
                case ClimbProcess.Falling:
                    // Otherwise if the animal is floating, have it move diagonally
                    MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
                    MoveHelper.MoveVertically(animal, animal.MoveDistance * 2);

                    // If the animal has hit or will hit the bottom of the cage
                    if (animal.YPosition + animal.MoveDistance >= animal.YPositionMax)
                    {
                        // Switch to the next process
                        this.NextProcess(animal);
                    }

                    break;
                case ClimbProcess.Scurrying:
                    // Move the animal horizontally
                    MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

                    // If the animal has hit or will hit a vertical wall
                    if (animal.XPosition - animal.MoveDistance <= 0 || animal.XPosition + animal.MoveDistance >= animal.XPositionMax)
                    {
                        // Move the animal to the appropriate edge of the cage
                        if (animal.XPosition + animal.MoveDistance >= animal.XPositionMax)
                        {
                            animal.XPosition = animal.XPositionMax;
                        }
                        else
                        {
                            animal.XPosition = 0;
                        }

                        // Switch to the next process
                        this.NextProcess(animal);
                    }

                    break;
            }
        }

        /// <summary>
        /// Toggles between the two processes of the hovering behavior.
        /// </summary>
        /// <param name="animal">The animal to move.</param>
        private void NextProcess(Animal animal)
        {
            switch (this.process)
            {
                case ClimbProcess.Climbing:
                    this.process = ClimbProcess.Falling;

                    break;
                case ClimbProcess.Falling:
                    this.process = ClimbProcess.Scurrying;

                    break;
                case ClimbProcess.Scurrying:
                    // Set the maximum height to a random value between 15 and 85 percent of the height of the animal's maximum y position
                    int lowerMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.15));
                    int higherMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.85));

                    this.maxHeight = ClimbBehavior.random.Next(lowerMax, higherMax + 1);

                    this.process = ClimbProcess.Climbing;

                    break;
            }
        }
    }
}
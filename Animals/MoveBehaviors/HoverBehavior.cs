using System;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class that represents the move behavior of flying.
    /// </summary>
    [Serializable]
    public class HoverBehavior : IMoveBehavior
    {
        /// <summary>
        /// A random value involved in the animal's movement.
        /// </summary>
        private static Random moveRandom = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The process stage the hovering animal is currently in.
        /// </summary>
        private HoverProcess process;

        /// <summary>
        /// A counter indicating the number of steps remaining in the current process.
        /// </summary>
        private int stepCount = 0;

        /// <summary>
        /// The animal hovers.
        /// </summary>
        /// <param name="animal">The animal which is to hover.</param>
        public void Move(Animal animal)
        {
            if (this.stepCount <= 0)
            {
                this.NextProcess(animal);
            }

            this.stepCount--;

            int moveDistance;

            if (this.process == HoverProcess.Hovering)
            {
                moveDistance = animal.MoveDistance;

                animal.XDirection = HoverBehavior.moveRandom.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
                animal.YDirection = HoverBehavior.moveRandom.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;
            }
            else
            {
                moveDistance = animal.MoveDistance * 4;
            }

            MoveHelper.MoveHorizontally(animal, moveDistance);
            MoveHelper.MoveVertically(animal, moveDistance);
        }

        /// <summary>
        /// Changes the animals hover process. To be called upon completion of current process.
        /// </summary>
        /// <param name="animal">The animal whose hover process is to change.</param>
        private void NextProcess(Animal animal)
        {
            if (this.process == HoverProcess.Hovering)
            {
                this.process = HoverProcess.Zooming;

                this.stepCount = HoverBehavior.moveRandom.Next(5, 9);

                animal.XDirection = HoverBehavior.moveRandom.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
                animal.YDirection = HoverBehavior.moveRandom.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;
            }
            else
            {
                this.process = HoverProcess.Hovering;

                this.stepCount = HoverBehavior.moveRandom.Next(7, 11);
            }
        }
    }
}
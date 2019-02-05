namespace Animals
{
    /// <summary>
    /// A factory for creating movement behaviors.
    /// </summary>
    public static class MoveBehaviorFactory
    {
        /// <summary>
        /// Creates a movement behavior based on the passed-in type.
        /// </summary>
        /// <param name="type">The type of behavior to create.</param>
        /// <returns>The created behavior.</returns>
        public static IMoveBehavior CreateMoveBehavior(MoveBehaviorType type)
        {
            IMoveBehavior result = null;

            switch (type)
            {
                case MoveBehaviorType.Climb:
                    result = new ClimbBehavior();
                    break;
                case MoveBehaviorType.Fly:
                    result = new FlyBehavior();
                    break;
                case MoveBehaviorType.Hover:
                    result = new HoverBehavior();
                    break;
                case MoveBehaviorType.Pace:
                    result = new PaceBehavior();
                    break;
                case MoveBehaviorType.Swim:
                    result = new SwimBehavior();
                    break;
                case MoveBehaviorType.NoMove:
                    result = new NoMoveBehavior();
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
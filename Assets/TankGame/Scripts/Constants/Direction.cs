namespace TankGame {
    public enum Direction {
        Up = 0,
        Right,
        Down, 
        Left,
        None
    }
    
    public static class DirectionHelper {
        public static Direction LocalToWorld (Direction facingDirection, Direction localDirection) {
            // This is such a bad way to do this
            // Relies on the enums above to be in a clockwise order

            int d = (int) facingDirection + (int) localDirection;
            d = d % 4;

            var newDirection = (Direction) d;
            return newDirection;
        }
    }
}
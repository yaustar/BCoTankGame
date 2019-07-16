namespace TankGame {
    public interface IGun {
        float GetReloadTimeSecs();
        float GetSecSinceLastFired();
    }
}
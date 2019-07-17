namespace TankGame {
    public interface IGun {
        float GetGunReloadTimeSecs();
        float GetSecSinceLastFired();
    }
}
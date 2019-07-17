namespace TankGame {
    public interface IBomb {
        float GetBombReloadTimeSecs();
        float GetSecSinceLastBombed();
    }
}
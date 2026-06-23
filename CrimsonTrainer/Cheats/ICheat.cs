namespace CrimsonTrainer.Cheats
{
    /// <summary>
    /// Contract that every cheat must implement.
    /// </summary>
    public interface ICheat
    {
        /// <summary>Display name shown in the UI.</summary>
        string Name { get; }

        /// <summary>Whether this cheat is currently enabled.</summary>
        bool IsActive { get; }

        /// <summary>Flip the active state.</summary>
        void Toggle();

        /// <summary>
        /// Called every timer tick (~100 ms). Write the desired values to game memory.
        /// Must be a no-op when IsActive is false.
        /// </summary>
        void Apply();
    }
}

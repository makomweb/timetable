using System;

namespace Timetable
{
    public static class Ensure
    {
        public static void That(bool condition, string message)
        {
            if (!condition)
            {
                throw new ArgumentException($"Verification failed!\n{message}");
            }
        }
    }
}
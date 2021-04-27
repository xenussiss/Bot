using System;
using System.Threading;
using System.Text;

namespace TolokaBot
{
    class MethodsLib
    {
        public static void CapthcaAlert()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Beep(800, 100
                );
                Console.Beep(1000, 100
                    );
                Console.Beep(2000, 400
                    );
                Console.Beep(1000, 100
                    );
                Console.Beep(800, 100
                    );
                Console.Beep(1800, 200
                    );
                Console.Beep(1800, 200
                    );
                Console.Beep(1800, 200
                    );
                Console.Beep(1000, 200
                    );
                Console.Beep(1800, 300
                   );
                Thread.Sleep(200);
                Console.Beep(1600, 400
                   );
                Thread.Sleep(200);
            }
        }
    }
}

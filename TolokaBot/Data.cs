using System;
namespace TolokaBot
    
{
    class Data
    {
        Random random = new Random();
        public static string L1 { get; set; } = "Логин1";
        public static string L2 { get; set; } = "Логин2";
        public static string StartPassword { get; set; } = "Единый пароль для всех(для удобства)";
        

        //нереализован. функция случайного выбора логина
        public void LoginChoice(string RandomedLogin)
        {
            int r = random.Next(1, 2);
            
            if (r == 1)
            {
                var randomed = L1;

            }
            if (r == 2)
            {
                var randomed = L2;
            }

            
        }

        
    }
}

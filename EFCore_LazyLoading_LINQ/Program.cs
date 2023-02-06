/*
                                                      Задача

                            Необходимо подключить Lazy Loading и написать LINQ запросы

Критерии:
    ● Добавить в проект Lazy Loading
    ● Написать следующие LINQ запросы:
        1. Запрос, который объединяет 3 таблицы и обязательно включает LEFT JOIN
        2. Запрос, который возвращает разницу между CreatedDate/HiredDate и сегодня.
            Фильтрация должна быть выполнена на сервере.
        3. Запрос, который обновляет 2 сущности. Сделать в одной транзакции
        4. Запрос, который добавляет сущность Employee с Title и Project
        5. Запрос, который удаляет сущность Employee
        6. Запрос, который группирует сотрудников по ролям и возвращает название роли (Title)
            если оно не содержит 'a'

 */

namespace EFCore_LazyLoading_LINQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Starter.Run();

            Console.Write("\nPress any key to continue . . .");
            Console.ReadLine();
        }
    }
}
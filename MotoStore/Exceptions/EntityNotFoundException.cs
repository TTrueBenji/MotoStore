using System;

namespace MotoStore.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException(string name, string key)
            : base($"Данные '{name}' по id {key} не были найдены.")
        {
        }
        public EntityNotFoundException(string name)
            : base($"Данных '{name}' не обнаружено.")
        {
        }
    }
}
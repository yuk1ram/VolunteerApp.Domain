namespace VolunteerApp.Domain.Generics;

/// <summary>
/// Обобщённый контракт хранилища с сортировкой.
/// </summary>
/// <typeparam name="T">Тип элемента, поддерживающий сравнение.</typeparam>
public interface IRepository<T> where T : IComparable<T>
{
    /// <summary>Добавляет элемент в хранилище.</summary>
    void Add(T item);

    /// <summary>Возвращает элементы, упорядоченные по возрастанию.</summary>
    IReadOnlyList<T> GetSorted();

    /// <summary>Возвращает все элементы без сортировки.</summary>
    IReadOnlyList<T> GetAll();
}
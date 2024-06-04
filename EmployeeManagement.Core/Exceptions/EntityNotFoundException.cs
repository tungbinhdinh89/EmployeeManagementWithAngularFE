namespace EmployeeManagement.Core.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string? message) : base(message)
    {
    }

    public EntityNotFoundException(string type, int id) : base($"Entity of type {type}:{id} could not be found")
    {
    }
}
namespace BankingSystem.SharedKernel.Domain;

public abstract class Entity
{
    protected Entity()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public string Id { get; protected set; } = default!;

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? RemovedAt { get; private set; }
    public bool IsRemoved { get; private set; }

    protected abstract void Validate();

    public virtual bool CheckForNullProperties()
    {
        var properties = GetType().GetProperties();
        return properties.Any(prop => prop.GetValue(this) == null);
    }

    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;
        if (ReferenceEquals(compareTo, null)) return false;
        if (ReferenceEquals(this, compareTo)) return true;
        
        return string.Equals(Id, compareTo.Id, StringComparison.Ordinal);
    }

    public static bool operator ==(Entity? a, Entity? b)
    {
        return ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    }

    public static bool operator !=(Entity? a, Entity? b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + (Id?.GetHashCode() ?? 0);
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }

    public void MarkCreated()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkRemoved()
    {
        if (!IsRemoved)
        {
            IsRemoved = true;
            RemovedAt = DateTime.UtcNow;
        }
    }
}
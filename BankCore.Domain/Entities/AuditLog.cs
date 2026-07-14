namespace BankCore.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; private set; }
    public string CommandName { get; private set; } = string.Empty;
    public string RequestData { get; private set; } = string.Empty;
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }
    public DateTime ExecutedAt { get; private set; }

    private AuditLog() { }

    private AuditLog(string commandName, string requestData, bool isSuccess, string? errorMessage)
    {
        Id = Guid.NewGuid();
        CommandName = commandName;
        RequestData = requestData;
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        ExecutedAt = DateTime.UtcNow;
    }

    public static AuditLog CreateSuccess(string commandName, string requestData)
        => new(commandName, requestData, true, null);

    public static AuditLog CreateFailure(string commandName, string requestData, string errorMessage)
        => new(commandName, requestData, false, errorMessage);
}
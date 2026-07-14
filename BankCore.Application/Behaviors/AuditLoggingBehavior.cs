using System.Text.Json;
using BankCore.Application.Abstractions;
using BankCore.Domain.Entities;
using MediatR;

namespace BankCore.Application.Behaviors;

public class AuditLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AuditLoggingBehavior(IAuditLogRepository auditLogRepository, IUnitOfWork unitOfWork)
    {
        _auditLogRepository = auditLogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var commandName = typeof(TRequest).Name;

        if (!commandName.EndsWith("Command", StringComparison.Ordinal))
        {
            return await next();
        }

        var requestData = JsonSerializer.Serialize(request);

        try
        {
            var response = await next();

            var successLog = AuditLog.CreateSuccess(commandName, requestData);
            await _auditLogRepository.AddAsync(successLog);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return response;
        }
        catch (Exception ex)
        {
            _unitOfWork.DiscardChanges();

            var failureLog = AuditLog.CreateFailure(commandName, requestData, ex.Message);
            await _auditLogRepository.AddAsync(failureLog);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            throw;
        }
    }
}
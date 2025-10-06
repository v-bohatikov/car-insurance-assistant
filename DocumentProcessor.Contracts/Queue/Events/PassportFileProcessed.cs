using DocumentProcessor.Contracts.Models;
using SharedKernel.BaseTypes;

namespace DocumentProcessor.Contracts.Queue.Events;

public record PassportFileProcessed(
    Ulid UserId,
    Ulid DocumentId,
    PassportData PassportData)
    : EventBase(UserId);
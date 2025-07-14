using DocumentProcessor.Contracts.Models;

namespace DocumentProcessor.Contracts.Queue.Events;

public record PassportFileProcessed(
    long UserId,
    Guid FileId,
    PassportData PassportData);